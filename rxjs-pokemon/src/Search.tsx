import { useState, useMemo, memo } from "react";
import { useObservableState } from "observable-hooks";
import { usePokemon } from "./store";
import { BehaviorSubject, combineLatestWith, map, pipe } from "rxjs";

export const Search = memo(() => {
  const { pokemon$, selected$ } = usePokemon();
  const search$ = useMemo(() => new BehaviorSubject(""), []);
  const pokemon = useObservableState(pokemon$, []);

  const [filteredPokemon] = useObservableState(
    () =>
      pokemon$.pipe(
        combineLatestWith(search$),
        map(([pokemon, search]) =>
          pokemon.filter((p) =>
            p.name.toLowerCase().includes(search.toLowerCase())
          )
        )
      ),
    []
  );

  // const filteredPokemon = useMemo(() => {
  //   return pokemon.filter((p) =>
  //     p.name.toLowerCase().includes(search$.value.toLowerCase())
  //   );
  // }, [pokemon]);

  return (
    <div>
      <input
        type="text"
        value={search$.value}
        onChange={(e) => {
          search$.next(e.target.value);
        }}
      />
      <div>
        {filteredPokemon.map((p) => (
          <div key={p.name}>
            <input
              type="checkbox"
              checked={p.selected}
              onChange={() => {
                if (selected$.value.includes(p.id)) {
                  selected$.next(selected$.value.filter((id) => id !== p.id));
                } else {
                  selected$.next([...selected$.value, p.id]);
                }
              }}
            />
            <strong>{p.name}</strong> - {p.power}
          </div>
        ))}
      </div>
    </div>
  );
});
