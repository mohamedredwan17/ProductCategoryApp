using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ProductCategoryApp.Domain.Products.Entities;

namespace ProductCategoryApp.Infrastructure.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .ValueGeneratedNever();

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(200);

            
            builder.OwnsOne(p => p.Price, priceBuilder =>
            {
                priceBuilder.Property(p => p.Value)
                    .HasColumnName("Price")
                    .HasPrecision(18, 2)
                    .IsRequired();

                priceBuilder.Property(p => p.Currency)
                    .HasColumnName("Currency")
                    .HasMaxLength(3)
                    .IsRequired();
            });

            
            builder.OwnsOne(p => p.CategoryId, categoryIdBuilder =>
            {
                categoryIdBuilder.Property(c => c.Value)
                    .HasColumnName("CategoryId")
                    .IsRequired();
            });

            builder.ToTable("Products");
        }
    }
}
