using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact.Domain.Models
{
    public class ContactInformation
    {
        public Guid Id { get; set; }
        public Guid PersonId { get; set; }
        public string Phone { get; set; }
        public string Mail { get; set; }
        public string Location { get; set; }
        public string Information { get; set; }
    }
}
