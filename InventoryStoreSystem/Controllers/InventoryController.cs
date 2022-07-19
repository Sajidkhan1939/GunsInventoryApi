using InventoryApi.Model;
using InventoryApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryApi.Controllers
{
    [Route("api/Inventory")]
    [Authorize]
    public class InventoryController : ControllerBase
    {
        private readonly IinventoryRepository _InventoryRepository;
        private IWebHostEnvironment _Environment;
        public InventoryController(IinventoryRepository InventoryRepository, IWebHostEnvironment _environment)
        {
            this._InventoryRepository = InventoryRepository;
            _Environment = _environment;
        }
        [Route("Add")]
        [HttpPost]
        public ActionResult Add([FromBody]Guns objAdd)
        {
            try
            {
                return Ok(_InventoryRepository.Add(objAdd, User.Identity?.Name?? ""));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        [Route("ReadByGunsId/{Id}")]
        [HttpGet]
        public ActionResult ReadByGunsId(string Id)
        {
            try
            {
                return Ok(_InventoryRepository.ReadByGunsId(Id));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        [Route("ReadGunsByUserId")]
        [HttpGet]
        public ActionResult ReadGunsByUserId()
        {
            try
            {
                return Ok(_InventoryRepository.ReadGunsByUserId(User.Identity.Name?? ""));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        [Route("Update")]
        [HttpPost]
        public ActionResult Update([FromBody] Guns objGuns)
        {
            try
            {
                return Ok(_InventoryRepository.Update(objGuns, User.Identity.Name ?? ""));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        [Route("Delete/{Id}")]
        [HttpGet]
        public ActionResult Delete(string Id)
        {
            try
            {
                return Ok(_InventoryRepository.Delete(Id));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
