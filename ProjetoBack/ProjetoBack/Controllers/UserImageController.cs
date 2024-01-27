using Microsoft.AspNetCore.Mvc;
using Rest.Contracts.Repository;
using Rest.DTO;
using Rest.Entity;
using Rest.Repository;

namespace Rest.Controllers
{

    [ApiController]
    [Route("UserProfileImage")]
    public class UserImageController : ControllerBase
    {
        private readonly IUserImageRepository _userImageRepository;

        public UserImageController(IUserImageRepository userImageRepository)
        {
            _userImageRepository = userImageRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Add(UserImageDTO userImage)
        {
            await _userImageRepository.Add(userImage);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _userImageRepository.Delete(id);  
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _userImageRepository.GetByUserId(id));
        }

        [HttpPut]
        public async Task<IActionResult> Update(UserImageEntity userImage)
        {
            await _userImageRepository.Update(userImage);
            return Ok();
        }
    }
}