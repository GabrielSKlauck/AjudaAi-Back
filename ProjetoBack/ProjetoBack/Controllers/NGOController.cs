using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjetoBack.Entity;
using Rest.Contracts.Repository;
using Rest.DTO;
using Rest.Entity;

namespace Rest.Controllers
{
    [ApiController]
    [Route("ngo")]
    public class NGOController : ControllerBase
    {
        private readonly INGORepository _ngoRepository;
        public static bool tokenValidado;

        public NGOController(INGORepository ngoRepository)
        {
            _ngoRepository = ngoRepository;
        }

        [HttpPost]       
        public async Task<IActionResult> Add(NGODTO ngo)
        {
            await _ngoRepository.Add(ngo);
            return Ok();
        }

        
        [HttpPut]
        public async Task<IActionResult> Update(NGOEntity ngo)
        {
            await _ngoRepository.Update(ngo);
            return Ok();
        }

        [HttpPut("AtualizarPerfil")]
        public async Task<IActionResult> UpdatePerfil(NgoUpdateEntity ngo)
        {
            await _ngoRepository.UpdatePerfil(ngo);
            return Ok();
        }

        [HttpPut("Atualizarlogo")]
        public async Task<IActionResult> UpdateLogoPerfil(NgoLogoUpdateEntity ngo)
        {
            await _ngoRepository.UpdateLogo(ngo);
            return Ok();
        }

        [HttpGet]  
        public async Task<IActionResult> Get(){           
           return Ok(await _ngoRepository.Get());                   
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _ngoRepository.GetById(id));
        }

        [HttpGet("CauseId/{id}")]
        public async Task<IActionResult> GetByCausesId(int id)
        {
            return Ok(await _ngoRepository.GetByCausesId(id));
        }

        [HttpGet("GetByName/{NgoName}")]
        public async Task<IActionResult> GetByName(string NgoName)
        {
            return Ok(await _ngoRepository.GetByName(NgoName));
        }

        [HttpGet("GetByEmail/{email}")]
        public async Task<IActionResult> GetByEmail(string email)
        {
            return Ok(await _ngoRepository.GetByEmail(email));
        }

        
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _ngoRepository.Delete(id);
            return Ok();
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(NGOLoginDTO ngo)
        {
            try
            {
                return Ok(await _ngoRepository.Login(ngo));
            }catch (Exception e)
            {
                return Unauthorized("Usuario ou senha invalidos");
            }
        }

        [HttpPost("SendTokenToEmail/{email}")]
        public async Task<IActionResult> SendTokenToEmail(string email)
        {
            try
            {
                await _ngoRepository.SendTokenToEmail(email);
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
            await _ngoRepository.CheckToken(tokenNum);
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
            await _ngoRepository.ChangePassword(senha);
            return Ok();
        }
    }
}
