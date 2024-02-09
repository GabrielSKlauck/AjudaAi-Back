using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rest.Contracts.Repository;
using Rest.DTO;
using Rest.Entity;
using Rest.Repository;

namespace Rest.Controllers
{
    [ApiController]
    [Route("Achievements")]
    public class AchievementsController : ControllerBase
    {
        private readonly IAchievementsRepository _achievementsRepository;

        public AchievementsController(IAchievementsRepository achievementsRepository)
        {
            _achievementsRepository = achievementsRepository;
        }

        [HttpPost]
        [Authorize(Roles = "master")]
        public async Task<IActionResult> Add(AchievementsDTO entity)
        {
            await _achievementsRepository.Add(entity);
            return Ok();
        }

        [HttpDelete]
        [Authorize(Roles = "master")]
        public async Task<IActionResult> Delete(int id)
        {
            await _achievementsRepository.Delete(id);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _achievementsRepository.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _achievementsRepository.GetById(id));
        }

        [HttpPut]
        [Authorize(Roles = "master")]
        public async Task<IActionResult> Update(AchievementsEntity entity)
        {
            await _achievementsRepository.Update(entity);
            return Ok();
        }

        [HttpPut("UpdateDescription")]
        [Authorize(Roles = "master")]
        public async Task<IActionResult> UpdateDescription(AchievementsEntity entity)
        {
            await _achievementsRepository.UpdateDescription(entity);
            return Ok();
        }
    }
}
