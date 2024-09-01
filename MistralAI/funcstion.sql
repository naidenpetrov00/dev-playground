create or replace function match_handbook_docks (
  query_embedding vector(1024),
  match_threshold float,
  match_count int
)
returns table (
  id bigint,
  content text,
  similarity float
)
language sql stable
as $$
  select
    handbook_docks.id,
    handbook_docks.content,
    1 - (handbook_docks.embedding <=> query_embedding) as similarity
  from handbook_docks
  where 1 - (handbook_docks.embedding <=> query_embedding) > match_threshold
  order by (handbook_docks.embedding <=> query_embedding) asc
  limit match_count;
$$;