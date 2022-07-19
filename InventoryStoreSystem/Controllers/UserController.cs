using InventoryApi.Model;
using InventoryApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryApi.Controllers
{
    [Route("api/User")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _UserRepository;
        public UserController(IUserRepository UserRepository)
        {
            _UserRepository = UserRepository;
        }
        [Route("Register")]
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Register([FromBody] User objRegister)
        {
            try
            {
                return Ok(_UserRepository.Register(objRegister));

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        [Route("Update")]
        [HttpPost]
        public IActionResult Update([FromBody] User user)
        {
            try
            {
                return Ok(_UserRepository.Update(user,User.Identity?.Name ?? ""));

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        [Route("ChangePassword")]
        [HttpPost]
        public IActionResult ChangePassword([FromBody] ChangePassword user)
        {
            try
            {
                user.Id = User.Identity?.Name ?? "";
                return Ok(_UserRepository.ChangePassword(user));

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        [Route("ReadById")]
        [HttpGet]
        public IActionResult ReadByUserId()
        {
            try
            {
                
                return Ok(_UserRepository.ReadByUserId(User.Identity?.Name ?? ""));

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

    }
}
