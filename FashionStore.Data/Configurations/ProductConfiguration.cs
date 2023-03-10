using FashionStore.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionStore.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.Name).IsRequired().HasMaxLength(255);
            builder.Property(x => x.Slug).IsRequired().HasMaxLength(255);
            builder.Property(x => x.Description).IsRequired();
            builder.Property(x => x.Status).IsRequired();
            builder.Property(x => x.Thumbnail).IsRequired();

            builder.Property(x => x.Stock).IsRequired();
            builder.Property(x => x.Price).IsRequired();
            builder.Property(x => x.PriceSale).IsRequired();

            builder.HasOne(x => x.Brand).WithMany(x => x.Products).HasForeignKey(x => x.BrandId);
            builder.HasOne(x => x.Category).WithMany(x => x.Products).HasForeignKey(x => x.CategoryId);
        }
    }
}
