namespace ProjetoBack.Entity
{
    public class NgoUpdateEntity
    {
        public int Id { get; set; }

        public string NgoName { get; set; }

        public string Description { get; set; }

        public int CausesId { get; set; }

        public int CityId { get; set; }

        public int CityStateId { get; set; }
    }
}