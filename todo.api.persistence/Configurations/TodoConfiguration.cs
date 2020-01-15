using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ToDo.Api.Persistence.Configurations
{
    public class ToDoConfiguration:IEntityTypeConfiguration<ToDo>
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
