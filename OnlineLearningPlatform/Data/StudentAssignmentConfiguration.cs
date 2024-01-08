using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineLearningPlatform.Models;

namespace OnlineLearningPlatform.Data
{
    public class StudentAssignmentConfiguration : IEntityTypeConfiguration<StudentAssignment>
    {
        public void Configure(EntityTypeBuilder<StudentAssignment> builder)
        {
            builder.Property(b => b.Status).HasConversion(c => c.ToString(), c => Enum.Parse<Status>(c));
        }
    }
}
