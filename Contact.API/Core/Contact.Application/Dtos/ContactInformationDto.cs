using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact.Application.Dtos
{
    public class ContactInformationDto
    {
        [Required(ErrorMessage = "PersonId is required")]
        public virtual Nullable<System.Guid> PersonId { get; set; }
        public string Phone { get; set; }
        public string Mail { get; set; }
        public string Location { get; set; }
        public string Information { get; set; }
    }
}
