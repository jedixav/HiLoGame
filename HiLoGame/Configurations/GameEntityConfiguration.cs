using HiLoGame.Repositories.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HiLoGame.Configurations
{
    public class GameEntityConfiguration :IEntityTypeConfiguration<GameEntity>
    {
        public void Configure(EntityTypeBuilder<GameEntity> builder)
        {
            builder.Property(x => x.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();
        }
    }
}
