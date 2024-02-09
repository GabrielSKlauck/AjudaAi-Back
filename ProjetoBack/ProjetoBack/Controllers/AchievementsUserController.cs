using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rest.Contracts.Repository;
using Rest.DTO;
using Rest.Entity;
using Rest.Repository;

namespace Rest.Controllers
{
    [ApiController]
    [Route("AchievementsUser")]
    public class AchievementsUserController : ControllerBase
    {
        private readonly IAchievementsUserRepository _achievementsUserRepository;

        public AchievementsUserController(IAchievementsUserRepository achievementsUserRepository)
        {
            _achievementsUserRepository = achievementsUserRepository;
        }

        [HttpPost]
        [Authorize(Roles = "master")]
        public async Task<IActionResult> Add(AchievementsUserDTO dto)
        {
            await _achievementsUserRepository.Add(dto);
            return Ok(dto);
        }

        [HttpDelete]
        [Authorize(Roles = "master")]
        public async Task<IActionResult> Delete(AchievementsUserDTO dto)
        {
            await _achievementsUserRepository.Delete(dto);
            return Ok();
        }

        [HttpGet("ConquistasCompletas{userId}")]
        public async Task<IActionResult> GetComplete(int userId)
        {
            return Ok(await _achievementsUserRepository.GetAchievementsCompletedByUserId(userId));
        }

        [HttpGet("ConquistasIncompletas{userId}")]
        public async Task<IActionResult> GetIncomplete(int userId)
        {
            return Ok(await _achievementsUserRepository.GetAchievementsIncompleteByUserId(userId));
        }
    }
}
