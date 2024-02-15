using Rest.Entity;

namespace Rest.DTO
{
    public class MasterTokenDTO
    {
        public string Token { get; set; }
        public MasterEntity Master { get; set; }
    }
}