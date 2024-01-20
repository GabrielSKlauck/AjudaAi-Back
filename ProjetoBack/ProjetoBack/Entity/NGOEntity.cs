namespace Rest.Entity
{
    public class NGOEntity
    {
        public int Id { get; set; }
        public string NgoName { get; set; }

        public string Description { get; set; }

        public string Site { get; set; }

        public string HeadPerson { get; set; }

        public string Telephone { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Role { get; set; }
        
        public int CausesId { get; set; }

        public int CityId { get; set; }

        public int CityStateId { get; set; }
    }
}
