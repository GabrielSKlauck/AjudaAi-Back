using Microsoft.AspNetCore.Mvc;
using Rest.Contracts.Repository;
using Rest.DTO;
using Rest.Entity;
using Rest.Repository;

namespace Rest.Controllers
{

    [ApiController]
    [Route("user")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        public static bool tokenValidado;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Add(UserDTO user)
        {
            await _userRepository.Add(user);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update(UserEntity user)
        {
            await _userRepository.Update(user);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _userRepository.Get());
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _userRepository.GetById(id));
        }

        [HttpGet("GetByEmail/{email}")]
        public async Task<IActionResult> GetByEmail(string email)
        {
            return Ok(await _userRepository.GetByEmail(email));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _userRepository.Delete(id);
            return Ok();
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(UserLoginDTO user)
        {
            try
            {
                return Ok(await _userRepository.Login(user));
            }
            catch (Exception e)
            {
                return Unauthorized("Usuario ou senha invalidos");
            }
        }

        [HttpPost("SendTokenToEmail/{email}")]       
        public async Task<IActionResult> SendTokenToEmail(string email)
        {
            try
            {
                await _userRepository.SendTokenToEmail(email);
                return Ok();
            }catch (Exception e)
            {
                return Unauthorized("Email invalido");
            }
        }

        [HttpPost("SendToken/{token}")]
        public async Task<IActionResult> CheckToken(string token)
        {
            int tokenNum = (int)Int64.Parse(token);
            await _userRepository.CheckToken(tokenNum);
            if (tokenValidado)
            {
                
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost("ChangePassword/{senha}")]       
        public async Task<IActionResult> ChangePassword(string senha)
        {
            await _userRepository.ChangePassword(senha);
            return Ok();
        }
    }
}