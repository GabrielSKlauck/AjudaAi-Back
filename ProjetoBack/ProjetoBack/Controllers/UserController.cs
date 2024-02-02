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

        [HttpPost]
        [Route("SendTokenToEmail")]
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

        [HttpPost]
        [Route("SendToken")]
        public async Task<IActionResult> CheckToken(int token)
        {
            await _userRepository.CheckToken(token);
            if (tokenValidado)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [Route("ChangePassword")]
        public async Task<IActionResult> ChangePassword(string senha)
        {
            await _userRepository.ChangePassword(senha);
            return Ok();
        }
    }
}