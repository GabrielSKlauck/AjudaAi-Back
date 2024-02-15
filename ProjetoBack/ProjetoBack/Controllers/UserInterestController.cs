using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rest.Contracts.Repository;
using Rest.DTO;
using Rest.Entity;
using Rest.Repository;
using System.Data;

namespace Rest.Controllers
{

    [ApiController]
    [Route("userInterest")]
    public class UserInterestController : ControllerBase
    {
        private readonly IUserInterestRepository _userInterestRepository;

        public UserInterestController(IUserInterestRepository userInterestRepository)
        {
            _userInterestRepository = userInterestRepository;
        }

        [HttpPost]
        [Authorize(Roles = "voluntario")]
        public async Task<IActionResult> Add(UserInterestDTO user)
        {
            await _userInterestRepository.Add(user);
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByUserId(int id)
        {
            return Ok(await _userInterestRepository.GetByUserId(id));
        }

        [HttpPut]
        [Authorize(Roles = "voluntario")]
        public async Task<IActionResult> Update(UserInterestEntity user)
        {
            await _userInterestRepository.Update(user);
            return Ok();
        }

        [HttpDelete]
        [Authorize(Roles = "voluntario")]
        public async Task<IActionResult> Delete(int id)
        {
            await _userInterestRepository.Delete(id);
            return Ok();    
        }
    }
}