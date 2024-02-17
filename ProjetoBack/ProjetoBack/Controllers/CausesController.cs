using Microsoft.AspNetCore.Mvc;
using Rest.Contracts.Repository;

namespace Rest.Controllers
{
    [ApiController]
    [Route("Causes")]
    public class CausesController : ControllerBase
    {
        private readonly ICausesRepository _causesRepository;

        public CausesController(ICausesRepository causesRepository)
        {
            _causesRepository = causesRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _causesRepository.Get());
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _causesRepository.GetById(id));
        }
    }
}
