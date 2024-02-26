using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Rest.Contracts.Repository;
using Rest.DTO;
using Rest.Entity;
using Rest.Repository;
using System.Diagnostics.CodeAnalysis;

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

        [HttpPut("UpdateProfileImage")]
        public async Task<IActionResult> UpdateProfileImage(UserImageUpdateEntity user)
        {
            await _userRepository.UpdateProfileImage(user);
            return Ok();
        }

        [HttpPut("AtualizarConta")]
        public async Task<IActionResult> AtualizarContaUser(UserUpdateEntity user)
        {
            await _userRepository.ShortUpdate(user);
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

        [HttpGet("GetImageById/{id}")]
        public async Task<IActionResult> GetImageById(int id)
        {
            return Ok(await _userRepository.GetImageById(id));
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

        [HttpPost("AddMaster")]
        public async Task<IActionResult> AddMaster(UserDTO adm)
        {
            await _userRepository.AddMaster(adm);
            return Ok();
        }

        [HttpPut("UpdateMaster")]
        [Authorize(Roles ="admin")]
        public async Task<IActionResult> UpdateMaster(UserEntity adm)
        {
            await _userRepository.UpdateMaster(adm);
            return Ok();
        }

        [HttpDelete("DeleteMaster")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteMaster(int id)
        {
            await _userRepository.DeleteMaster(id);
            return Ok();
        }

        [HttpGet("GetAllAdmins")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetAllAdmins()
        {
            return Ok(await _userRepository.GetAllAdmins());
        }
    }
}