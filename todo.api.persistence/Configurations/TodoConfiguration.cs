using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace todo.api.persistence.Configurations
{
    public class TodoConfiguration:IEntityTypeConfiguration<todo.api.persistence.ToDo>
    {
        public void Configure(EntityTypeBuilder<ToDo> builder)
        {
            builder.Property(s => s.Content).IsRequired();

            builder.Property(s => s.CreatedBy).IsRequired();

            builder.Property(s => s.CreatedOn)
                .IsRequired()
                .HasColumnType("Date")
                .HasDefaultValueSql("GETDATE()");

            builder.Property(s => s.ModifiedBy).IsRequired();

            builder.Property(s => s.ModifiedOn)
                .IsRequired()
                .HasColumnType("Date")
                .HasDefaultValueSql("GETDATE()");
        }
    }
}
