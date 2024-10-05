using Gatherly.Domain.Entities;
using Gatherly.Domain.Repositories;
using Gatherly.Domain.ValueObjects;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

namespace Gatherly.Persistence.Repositories;

public class CachedMemberRepository : IMemberRepository
{
    private readonly IMemberRepository _decorated;
    private readonly IDistributedCache distributedCache;
    private readonly ApplicationDbContext dbContext;

    public CachedMemberRepository(
        IMemberRepository decorated,
        IDistributedCache distributedCache,
        ApplicationDbContext dbContext
    )
    {
        _decorated = decorated;
        this.distributedCache = distributedCache;
        this.dbContext = dbContext;
    }

    public async Task<Member?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        string key = $"member-{id}";

        string cachedMember = await distributedCache.GetStringAsync(key, cancellationToken);

        Member? member;
        if (string.IsNullOrEmpty(cachedMember))
        {
            member = await _decorated.GetByIdAsync(id, cancellationToken);
            if (member == null)
            {
                return null;
            }

            await distributedCache.SetStringAsync(
                key,
                JsonConvert.SerializeObject(member),
                cancellationToken
            );

            return member;
        }

        member = JsonConvert.DeserializeObject<Member>(
            cachedMember,
            new JsonSerializerSettings
            {
                ConstructorHandling = ConstructorHandling.Default,
                ContractResolver = new PrivateResolver(),
            }
        );
        if (member != null)
        {
            dbContext.Set<Member>().Attach(member);
        }
        return member;
    }

    public Task<bool> IsEmailUniqueAsync(
        Email email,
        CancellationToken cancellationToken = default
    ) => _decorated.IsEmailUniqueAsync(email, cancellationToken);

    public void Add(Member member) => _decorated.Add(member);

    public void Update(Member member) => _decorated.Update(member);
}
