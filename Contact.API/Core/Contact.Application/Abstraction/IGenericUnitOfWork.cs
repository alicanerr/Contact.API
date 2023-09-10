using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact.Application.Abstraction
{
    public interface IGenericUnitOfWork
    {
        Task CommitAsync();
        void Commit();
    }
}
