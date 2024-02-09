using Microsoft.AspNetCore.Mvc;
using Rest.Contracts.Repository;
using Rest.Repository;

namespace Rest.Controllers
{
    [ApiController]
    [Route("City")]
    public class CityController : ControllerBase
    {
        private readonly ICityRepository _cityRepository;

        public CityController(ICityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _cityRepository.Get());

        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _cityRepository.GetByState(id));
        }

        [HttpGet("GetByCityId/{id}")]
        public async Task<IActionResult> GetByCityId(int id)
        {
            return Ok(await _cityRepository.GetById(id));
        }
    }
}
