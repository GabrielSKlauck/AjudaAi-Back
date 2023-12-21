using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rest.Contracts.Repository;
using Rest.DTO;

namespace Rest.Controllers
{
    public class AdsController : ControllerBase
    {
        private readonly IAdsRepository _adsRepository;

        public AdsController(IAdsRepository adsRepository)
        {
            _adsRepository = adsRepository;
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Add(AdsDTO ads)
        {
            await _adsRepository.Add(ads);
            return Ok();
        }

        [HttpDelete]
        [Authorize(Roles ="admin")]
        public async Task<IActionResult> Delete(int id)
        {
            await _adsRepository.Delete(id);
            return Ok();
        }
    }
}
