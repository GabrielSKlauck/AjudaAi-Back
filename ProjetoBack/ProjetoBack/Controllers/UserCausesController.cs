﻿using Microsoft.AspNetCore.Mvc;
using ProjetoBack.Contracts.Repository;
using ProjetoBack.DTO;
using Rest.Repository;

namespace ProjetoBack.Controllers
{
    [ApiController]
    [Route("UserCauses")]
    public class UserCausesController : ControllerBase
    {
        private readonly IUserCausesRepository _userCausesRepository;

        public UserCausesController(IUserCausesRepository userCausesRepository)
        {
            _userCausesRepository = userCausesRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Add(UserCausesDTO user)
        {
            await _userCausesRepository.Add(user);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetUserCausesById(int id)
        {
            return Ok(await _userCausesRepository.GetUserCausesById(id));
        }

        [HttpPut]
        public async Task<IActionResult> Update(UserCausesDTO user)
        {
            await _userCausesRepository.Update(user);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(UserCausesDTO user)
        {
            await _userCausesRepository.Delete(user);
            return Ok();
        }
    }
}
