using Contact.Services.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact.Services
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ContactDbContext>
    {
        public ContactDbContext CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder<ContactDbContext> dbContextOptionsBuilder = new();
            dbContextOptionsBuilder.UseNpgsql(Configuration.ConnectionString);
            return new(dbContextOptionsBuilder.Options);
        }
    }
}
