using Microsoft.AspNetCore.Mvc;
using Rest.Contracts.Repository;
using Rest.Entity;
using Rest.Repository;

namespace Rest.Controllers
{
    [ApiController]
    [Route("master")]
    public class MasterController : ControllerBase
    {
        private readonly IMasterRepository _masterRepository;

        public MasterController(IMasterRepository masterRepository)
        {
            _masterRepository = masterRepository;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(MasterLoginDTO master)
        {
            try
            {
                return Ok(await _masterRepository.Login(master));
            }
            catch (Exception e)
            {
                return Unauthorized("Usuario ou senha invalidos");
            }
        }
    }
}