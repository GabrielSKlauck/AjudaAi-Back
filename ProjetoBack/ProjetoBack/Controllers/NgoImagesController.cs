using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjetoBack.Contracts.Repository;
using ProjetoBack.DTO;
using ProjetoBack.Entity;
using System.Data;

namespace ProjetoBack.Controllers
{
    [ApiController]
    [Route("NgoImages")]
    public class NgoImagesController : ControllerBase
    {
        private readonly INgoImagesRepository _ngoImagesRepository;

        public NgoImagesController(INgoImagesRepository ngoImagesRepository)
        {
            _ngoImagesRepository = ngoImagesRepository;
        }

        [HttpPost]
        [Authorize(Roles = "ngo, admin")]
        public async Task<IActionResult> Add(NgoImagesDTO image)
        {
            await _ngoImagesRepository.Add(image);
            return Ok();
        }

        [HttpPut]
        [Authorize(Roles = "ngo, admin")]
        public async Task<IActionResult> Update(NgoImagesEntity image)
        {
            await _ngoImagesRepository.Update(image);
            return Ok();
        }

        [HttpGet("{ngoId}")]
        [Authorize(Roles = "ngo, admin")]
        public async Task<IActionResult> Get(int ngoId)
        {
            return Ok(await _ngoImagesRepository.Get(ngoId));
        }

        [HttpDelete]
        [Authorize(Roles = "ngo, admin")]
        public async Task<IActionResult> Delete(int id)
        {
            await _ngoImagesRepository.Delete(id);
            return Ok();
        }
    }
}
