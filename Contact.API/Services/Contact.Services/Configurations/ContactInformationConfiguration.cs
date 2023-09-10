using Contact.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact.Services.Configurations
{
    public class ContactInformationConfiguration : IEntityTypeConfiguration<ContactInformation>
    {
        public void Configure(EntityTypeBuilder<ContactInformation> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
        }
    }
}
