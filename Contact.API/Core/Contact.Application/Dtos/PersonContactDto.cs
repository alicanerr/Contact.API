namespace Contact.Application.Dtos
{
    public class PersonContactDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Company { get; set; }
        public Guid ContactInformationId { get; set; }
        public virtual Nullable<System.Guid> PersonId { get; set; }
        public string Phone { get; set; }
        public string Mail { get; set; }
        public string Location { get; set; }
        public string Information { get; set; }
    }

}
