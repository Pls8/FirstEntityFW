using AssimplyConnection.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssimplyConnection.Config
{
    public class CategoryConfiguration : IEntityTypeConfiguration<CategoryClass>
    {                                                           // ^-- ctrl + . to add AssimplyConnection
        public void Configure(EntityTypeBuilder<CategoryClass> builder)
        {
            // this Fluent API Configuration
            // this config instead of DataAnnotation [Flag] [Key] [ForignKey]
            builder.ToTable("Categories");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasIndex(c => c.Name)
                .IsUnique();
        }
    }
}
