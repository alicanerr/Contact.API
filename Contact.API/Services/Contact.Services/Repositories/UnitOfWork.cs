using Contact.Application.Abstraction;
using Contact.Services.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact.Services.Repositories
{
    public class UnitOfWork : IGenericUnitOfWork
    {
        private readonly ContactDbContext _context;

        public UnitOfWork(ContactDbContext context)
        {
            _context = context;
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
