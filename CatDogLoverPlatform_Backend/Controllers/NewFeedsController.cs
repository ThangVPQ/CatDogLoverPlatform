using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatDogLover_Repository.DAO;
using CatDogLover_Repository.DTO;
using CatDogLover_Repository.Utils;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CatDogLoverPlatform_Backend.Controllers
{
    public class NewFeedsController : ControllerBase
    {
        private readonly CatDogLoverDBContext _dBContext;
        public NewFeedsController(CatDogLoverDBContext dBContext)
        {
            _dBContext = dBContext;
        }
        [HttpPost]
        [Route("create-new-feed")]
        public async Task<IActionResult> CreateNewFeed([FromBody] NewFeedRequest newFeedRequest)
        {
            try
            {
                NewsFeed newsFeed = FunctionConvert.ConvertObjectToObject<NewsFeed, NewFeedRequest>(newFeedRequest);
                newsFeed.InsertDate = DateTime.Now;
                newsFeed.BirthDate = FunctionConvert.ConvertMilisecondToDateTime(newFeedRequest.BirthDate);
                newsFeed.Status = 1;
                if (newFeedRequest.ListImage != null)
                {
                    List<Image> images = new List<Image>();
                    foreach (var image in newFeedRequest.ListImage)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            image.CopyTo(memoryStream);
                            byte[] fileBytes = memoryStream.ToArray();
                            Image imageInit = new Image()
                            {
                                InsertDate = newsFeed.InsertDate,
                                SourceImage = fileBytes
                            };
                            images.Add(imageInit);
                        }
                    }
                    newsFeed.Images = images;
                }
                _dBContext.NewsFeeds.Add(newsFeed);
                _dBContext.SaveChanges();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
        [HttpPost]
        [Route("get-news-feed")]
        public async Task<IActionResult> GetNewsFeed([FromBody] PaginateRequest paginateRequest)
        {
            try
            {
                List<NewsFeed> newsFeeds = _dBContext.NewsFeeds.Skip((paginateRequest.CurrentPage - 1) * paginateRequest.PageSize).Take(paginateRequest.PageSize).ToList();
                foreach (var newFeed in newsFeeds)
                {
                    List<Comment> comments = _dBContext.Comments.Where(t => t.NewsFeedID.Equals(newFeed.NewsFeedID)).ToList();
                    List<NumberOfInteraction> numberOfInteractions = _dBContext.NumberOfInteractions.Where(t => t.NewsFeedID.Equals(newFeed.NewsFeedID)).ToList();
                    newFeed.Comments = comments;
                    newFeed.NumberOfInteractions = numberOfInteractions;
                }
                return Ok(new PaginatedData<NewsFeed>
                {
                    Data = newsFeeds,
                    CurrentPage = paginateRequest.CurrentPage,
                    TotalPages = _dBContext.NewsFeeds.Count(),
                    PageSize = paginateRequest.PageSize
                });
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
        [HttpPost]
        [Route("get-new-feed-by-id")]
        public async Task<IActionResult> GetNewFeedById([FromBody] IdRequest idRequest)
        {
            try
            {
                NewsFeed newsFeed = _dBContext.NewsFeeds.Where(t => t.NewsFeedID.Equals(idRequest.Id)).FirstOrDefault();
                List<Comment> comments = _dBContext.Comments.Where(t => t.NewsFeedID.Equals(newsFeed.NewsFeedID)).ToList();
                List<NumberOfInteraction> numberOfInteractions = _dBContext.NumberOfInteractions.Where(t => t.NewsFeedID.Equals(newsFeed.NewsFeedID)).ToList();
                newsFeed.Comments = comments;
                newsFeed.NumberOfInteractions = numberOfInteractions;
                return Ok(newsFeed);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
    }
}

