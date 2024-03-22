using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Route.Session3.DAL.Models;

namespace Route.Session3.DAL.Data.Configurations
{
    internal class DepartmentConfigurations : IEntityTypeConfiguration<Department>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Department> builder)
        {
            builder.Property(D => D.Id).UseIdentityColumn(10, 10);
            builder.Property(D => D.Name).IsRequired().HasColumnType("varchar").HasMaxLength(50);
            builder.Property(D => D.Code).IsRequired().HasColumnType("varchar").HasMaxLength(50);
        }
    }
}
