using CatDogLover_Repository.DAO;
using CatDogLover_Repository.DTO;
using CatDogLover_Repository.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CatDogLoverPlatform_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MasterDataController : ControllerBase
    {
        private readonly CatDogLoverDBContext _dBContext;
        public MasterDataController(CatDogLoverDBContext dBContext)
        {
            _dBContext = dBContext;
        }
        [HttpGet]
        [Route("get-all-type-goods")]
        public async Task<IActionResult> GetAllTypeGoods()
        {
            try
            {
                List<TypeGoods> listTypeGoods = _dBContext.TypeGoods.ToList();
                return Ok(listTypeGoods);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
        [HttpGet]
        [Route("get-all-type-news-feed")]
        public async Task<IActionResult> GetAllTypenewsfeed()
        {
            try
            {
                List<TypeNewsFeed> typeNewsFeeds = _dBContext.TypeNewsFeeds.ToList();
                return Ok(typeNewsFeeds);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
