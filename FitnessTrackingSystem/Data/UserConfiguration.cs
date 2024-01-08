using FitnessTrackingSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OnlineLearningPlatform.Data
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(b => b.Gender).HasConversion(c => c.ToString(), c => Enum.Parse<Gender>(c));
        }
    }
}
