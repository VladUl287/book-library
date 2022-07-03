using DataAccess.Abstractions;

namespace DataAccess.Models;

public class Role : EntityWithId<Guid>
{
    public string Name { get; set; } = string.Empty;
}