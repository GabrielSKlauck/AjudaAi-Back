using System.Web.Http;

namespace Rest.Infrastructure
{
    public class Configuration
    {
        public static string JWTSecret { get; set; } = "EaiPessoalTudoBemAquiQuemFalaÉOEduGaleraEstamosAquiParaJogarONovoRedDead3";

        public static void Register(HttpConfiguration config)
        {
            config.EnableCors();
        }
    }
}
