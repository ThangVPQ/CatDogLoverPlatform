using CatDogLover_Repository.DAO;
using CatDogLover_Repository.DTO;
using CatDogLover_Repository.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CatDogLoverPlatform_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypeGoodsController : ControllerBase
    {
        private readonly CatDogLoverDBContext _dBContext;
        public TypeGoodsController(CatDogLoverDBContext dBContext)
        {
            _dBContext = dBContext;
        }
        [HttpPost]
        [Route("add-new-type-goods")]
        public async Task<IActionResult> AddNewTypeGoods([FromBody] TypeGoodsRequest typeGoodsRequest)
        {
            try
            {
                TypeGoods typeGoods = _dBContext.TypeGoods.Where(t => t.TypeGoodsName.Equals(typeGoodsRequest.TypeGoodsName)).FirstOrDefault();
                if (typeGoods == null)
                {
                    TypeGoods typeGoodsNew = new TypeGoods()
                    {
                        TypeGoodsName = typeGoodsRequest.TypeGoodsName
                    };
                    _dBContext.TypeGoods.Add(typeGoodsNew);
                    _dBContext.SaveChanges();
                    return Ok("Create Type Goods Success");
                }
                else
                {
                    return BadRequest("Create Type Goods Unsuccess");
                }

            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
        [HttpPost]
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
    }
}
