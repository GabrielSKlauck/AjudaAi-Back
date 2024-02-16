using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        [Authorize(Roles = "admin")]
        [HttpPut]
        public async Task<IActionResult> Update(NGOEntity ngo)
        {
            await _ngoRepository.Update(ngo);
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

        [Authorize(Roles = "admin")]
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
    }
}
