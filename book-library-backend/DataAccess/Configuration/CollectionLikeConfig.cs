using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configuration;

public class CollectionLikeConfig : IEntityTypeConfiguration<CollectionLike>
{
    public void Configure(EntityTypeBuilder<CollectionLike> builder)
    {
        builder.HasKey(x => new { x.UserId, x.CollectionId });
    }
}