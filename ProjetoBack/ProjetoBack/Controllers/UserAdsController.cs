using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rest.Contracts.Repository;
using Rest.DTO;
using Rest.Entity;
using Rest.Repository;

namespace Rest.Controllers
{
    [ApiController]
    [Route("userAds")]
    public class UserAdsController : ControllerBase
    {
        private readonly IUserAdsRepository _userAdsRepository;

        public UserAdsController(IUserAdsRepository userAdsRepository)
        {
            _userAdsRepository = userAdsRepository;
        }

        [HttpPost]        
        public async Task<IActionResult> Add(UserAdsDTO user)
        {
            await _userAdsRepository.Add(user);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _userAdsRepository.Get());
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int adsId)
        {
            await _userAdsRepository.Delete(adsId);
            return Ok();
        }

        [HttpPost("userIdAdsId")]
        public async Task<IActionResult> GetByUserIdAdsId(UserAdsEntity userAds)
        {
            return Ok(await _userAdsRepository.GetByUserIdAdsId(userAds));
        }

    }
}