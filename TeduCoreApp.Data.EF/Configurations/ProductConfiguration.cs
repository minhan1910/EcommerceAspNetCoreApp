using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TeduCoreApp.Data.EF.Extensions;
using TeduCoreApp.Data.Entities;

namespace TeduCoreApp.Data.EF.Configurations
{
    public class ProductConfiguration : DbEntityConfiguration<Product>
    {
        public override void Configure(EntityTypeBuilder<Product> entity)
        {
            entity
                .Property(p => p.SeoAlias)
                .IsUnicode(false)
                .HasMaxLength(255);
        }
    }
}
