using CQRS.BankAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CQRS.BankAPI.Persistence.Configuration
{
    internal class CustomerConfig : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customers");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.FirtsName).HasColumnType("nvarchar").HasMaxLength(80).IsRequired();
            builder.Property(p => p.LastName).HasColumnType("nvarchar").HasMaxLength(200).IsRequired();
            builder.Property(p => p.BirthDate).IsRequired();
            builder.Property(p => p.PhoneNumber).HasColumnType("nvarchar").HasMaxLength(80).IsRequired();
            builder.Property(p => p.Email).HasColumnType("nvarchar").HasMaxLength(100);
            builder.Property(p => p.Address).HasColumnType("nvarchar").HasMaxLength(300).IsRequired();
            builder.Property(p => p.Age);
        }
    }
}
