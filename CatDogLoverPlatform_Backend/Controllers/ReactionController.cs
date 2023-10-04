using CatDogLover_Repository.DAO;
using CatDogLover_Repository.DTO;
using CatDogLover_Repository.Utils;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace CatDogLoverPlatform_Backend.Controllers
{
    public class ReactionController : ControllerBase
    {
        private readonly CatDogLoverDBContext _dBContext;
        public ReactionController(CatDogLoverDBContext dBContext)
        {
            _dBContext = dBContext;
        }
        [HttpPost]
        [Route("like-news-feed")]
        public async Task<IActionResult> LikeNewsFeed([FromBody] LikeRequest LikeRequest)
        {
            try
            {
                NumberOfInteraction numberOfInteraction = _dBContext.NumberOfInteractions.Where(t => t.NewsFeedID.Equals(LikeRequest.NewsFeedID) && t.UserID.Equals(LikeRequest.UserID)).FirstOrDefault();
                if (numberOfInteraction == null)
                {
                    NumberOfInteraction newNumberOfInteraction = FunctionConvert.ConvertObjectToObject<NumberOfInteraction, LikeRequest>(LikeRequest);
                    newNumberOfInteraction.InsertDate = DateTime.Now;
                    _dBContext.NumberOfInteractions.Add(newNumberOfInteraction);
                    _dBContext.SaveChanges();
                    return Ok("Like post Success");
                }
                else
                {
                    _dBContext.NumberOfInteractions.Remove(numberOfInteraction);
                    _dBContext.SaveChanges();
                    return Ok("Unlike post Success");
                }

            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
        [HttpPost]
        [Route("comment-news-feed")]
        public async Task<IActionResult> CommnentNewsFeed([FromBody] CommentRequest commentRequest)
        {
            try
            {
                Comment newComment = FunctionConvert.ConvertObjectToObject<Comment, CommentRequest>(commentRequest);
                newComment.InsertDate = DateTime.Now;
                newComment.Status = 1;
                _dBContext.Comments.Add(newComment);
                _dBContext.SaveChanges();
                return Ok("Comment post Success");
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
