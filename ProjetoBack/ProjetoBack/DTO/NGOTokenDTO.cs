using Rest.Entity;

namespace Rest.DTO
{
    public class NGOTokenDTO
    {
        public string Token { get; set; }

        public NGOEntity User { get; set; }
    }
}
