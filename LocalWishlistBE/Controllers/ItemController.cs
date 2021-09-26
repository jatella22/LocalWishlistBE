using LocalWishlistBE.Models;
using LocalWishlistBE.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace LocalWishlistBE.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class ItemController : ControllerBase
    {
        private IRepositoryWrapper _repoWrapper;
        public ItemController(IRepositoryWrapper repoWrapper)
        {
            _repoWrapper = repoWrapper;
        }

        [HttpGet]
        public IEnumerable<Item> GetAllItems()
        {
            var items = _repoWrapper.Item.FindAll();
            return items;
        }

        [HttpPost]
        public IActionResult SaveItem([FromBody] Item item)
        {
            _repoWrapper.Item.Create(item);
            _repoWrapper.Save();
            return Ok();
        }
    }
}
