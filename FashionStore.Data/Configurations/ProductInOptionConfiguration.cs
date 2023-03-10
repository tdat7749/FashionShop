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
    public class ProductInOptionConfiguration : IEntityTypeConfiguration<ProductInOption>
    {
        public void Configure(EntityTypeBuilder<ProductInOption> builder)
        {
            builder.ToTable("ProductInOptions");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();


            builder.HasOne(x => x.Product).WithMany(x => x.ProductInOptions).HasForeignKey(x => x.ProductId);
            builder.HasOne(x => x.Option).WithMany(x => x.ProductInOptions).HasForeignKey(x => x.OptionId);
        }
    }
}
