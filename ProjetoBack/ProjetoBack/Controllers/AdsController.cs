using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rest.Contracts.Repository;
using Rest.DTO;
using Rest.Entity;
using Rest.Repository;

namespace Rest.Controllers
{
    [ApiController]
    [Route("ads")]
    public class AdsController : ControllerBase
    {
        private readonly IAdsRepository _adsRepository;

        public AdsController(IAdsRepository adsRepository)
        {
            _adsRepository = adsRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _adsRepository.Get());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _adsRepository.GetById(id));
        }

        [HttpGet("CausesId/{id}")]
        public async Task<IActionResult> GetByCausesId(int id)
        {
            return Ok(await _adsRepository.GetByCausesId(id));
        }

        [HttpGet("NgoId/{id}")]
        public async Task<IActionResult> GetByNgoId(int id)
        {
            return Ok(await _adsRepository.GetByNgoId(id));
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Add(AdsDTO ads)
        {
            await _adsRepository.Add(ads);
            return Ok();
        }

        [HttpDelete]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int id)
        {
            await _adsRepository.Delete(id);
            return Ok();
        }

        [HttpPut]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Update(AdsEntity ads)
        {
            await _adsRepository.Update(ads);
            return Ok();
        }

        [HttpPost("Finalizar/{adsId}")]
        public async Task<IActionResult> Finalizar(int adsId)
        {
            await _adsRepository.Finalizar(adsId);
            return Ok();
        }

    }
}
