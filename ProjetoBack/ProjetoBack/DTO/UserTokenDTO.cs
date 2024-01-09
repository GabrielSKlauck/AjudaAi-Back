using Rest.Entity;

namespace Rest.DTO { 

public class UserTokenDTO
{
    public string Token { get; set; }

    public UserEntity User { get; set; }
}
}