using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatDogLover_Repository.DAO;
using CatDogLover_Repository.DTO;
using CatDogLover_Repository.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
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
        public async Task<IActionResult> CreateNewFeed([FromForm] NewFeedRequest newFeedRequest)
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
                List<NewsFeed> newsFeeds = _dBContext.NewsFeeds.Where(s => s.Status != 2).Skip((paginateRequest.CurrentPage - 1) * paginateRequest.PageSize).Take(paginateRequest.PageSize).ToList();
                List<NewsFeedDTO> newsFeedDTO = FunctionConvert.ConvertListToList<NewsFeedDTO, NewsFeed>(newsFeeds);
                for (int i = 0; i < newsFeedDTO.Count; i++)
                {
                    newsFeedDTO[i].UserName = _dBContext.Users.Where(u => u.UserID.Equals(newsFeeds[i].UserID)).FirstOrDefault().FullName;
                    newsFeedDTO[i].TypeGoodsName = _dBContext.TypeGoods.Where(u => u.TypeGoodsID.Equals(newsFeeds[i].TypeGoodsID)).FirstOrDefault().TypeGoodsName;
                    newsFeedDTO[i].InsertDated = FunctionConvert.ConvertDateTimeToMilisecond(newsFeeds[i].InsertDate);
                    newsFeedDTO[i].BirthDated = FunctionConvert.ConvertDateTimeToMilisecond(newsFeeds[i].BirthDate);
                    //newsFeedDTO[i].UpdateDated = FunctionConvert.ConvertDateTimeToMilisecond(newsFeeds[i].UpdateDate);
                    List<Comment> comments = _dBContext.Comments.Where(t => t.NewsFeedID.Equals(newsFeeds[i].NewsFeedID)).ToList();
                    List<CommentDTO> commentDTOs = FunctionConvert.ConvertListToList<CommentDTO, Comment>(comments);
                    for (int j = 0; j < commentDTOs.Count; j++)
                    {
                        commentDTOs[i].InsertDated = (long)FunctionConvert.ConvertDateTimeToMilisecond(comments[i].InsertDate);
                        //commentDTOs[i].UpdateDated = (long)FunctionConvert.ConvertDateTimeToMilisecond(comments[i].UpdateDate);
                        commentDTOs[i].UserName = _dBContext.Users.Where(t => t.UserID.Equals(comments[i].UserID)).FirstOrDefault().FullName;
                    }
                    List<NumberOfInteraction> numberOfInteractions = _dBContext.NumberOfInteractions.Where(t => t.NewsFeedID.Equals(newsFeeds[i].NewsFeedID)).ToList();
                    List<NumberOfInteractionDTO> numberOfInteractionDTOs = FunctionConvert.ConvertListToList<NumberOfInteractionDTO, NumberOfInteraction>(numberOfInteractions);
                    for (int k = 0; k < numberOfInteractionDTOs.Count; k++)
                    {
                        numberOfInteractionDTOs[i].InsertDated = (long)FunctionConvert.ConvertDateTimeToMilisecond(numberOfInteractions[i].InsertDate);
                        numberOfInteractionDTOs[i].UserName = _dBContext.Users.Where(t => t.UserID.Equals(numberOfInteractions[i].UserID)).FirstOrDefault().FullName;
                    }
                    List<Image> images = _dBContext.Images.Where(t => t.NewsFeedID.Equals(newsFeeds[i].NewsFeedID)).ToList();
                    List<ImageDTO> imageDTOs = FunctionConvert.ConvertListToList<ImageDTO, Image>(images);
                    for (int f = 0; f < imageDTOs.Count; f++)
                    {
                        imageDTOs[i].InsertDated = FunctionConvert.ConvertDateTimeToMilisecond(images[i].InsertDate);
                    }
                    newsFeedDTO[i].NumberOfInteractionDTOs = numberOfInteractionDTOs;
                    newsFeedDTO[i].CommentDTOs = commentDTOs;
                    newsFeedDTO[i].ImageDTOs = imageDTOs;
                }
                return Ok(new PaginatedData<NewsFeedDTO>
                {
                    Data = newsFeedDTO,
                    CurrentPage = paginateRequest.CurrentPage,
                    TotalPages = _dBContext.NewsFeeds.Where(s => s.Status != 2).Count(),
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
        [HttpPost]
        [Route("delete-news-feed")]
        public async Task<IActionResult> DeleteNewFeed([FromBody] IdRequest idRequest)
        {
            try
            {
                NewsFeed newsFeed = _dBContext.NewsFeeds.Where(t => t.NewsFeedID.Equals(idRequest.Id)).FirstOrDefault();
                newsFeed.Status = 2;
                _dBContext.NewsFeeds.Update(newsFeed);
                _dBContext.SaveChanges();
                return Ok(newsFeed);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
        [HttpPost]
        [Route("update-new-feed")]
        public async Task<IActionResult> UpdateNewFeed([FromForm] UpdateNewFeedRequest updateNewFeedRequest)
        {
            try
            {
                NewsFeed newsFeed = _dBContext.NewsFeeds.Where(s => s.NewsFeedID.Equals(updateNewFeedRequest.NewsFeedID)).FirstOrDefault();
                newsFeed.NewsFeedID = updateNewFeedRequest.NewsFeedID;
                newsFeed.TypeGoodsID = updateNewFeedRequest.TypeGoodsID ?? newsFeed.TypeGoodsID;
                newsFeed.PhoneNumber = updateNewFeedRequest.PhoneNumber ?? newsFeed.PhoneNumber;
                newsFeed.Title = updateNewFeedRequest.Title ?? newsFeed.Title;
                newsFeed.Content = updateNewFeedRequest.Content?? newsFeed.Content;
                newsFeed.Address = updateNewFeedRequest.Address?? newsFeed.Address;
                newsFeed.Price = updateNewFeedRequest.Price ?? newsFeed.Price;
                newsFeed.BirthDate = updateNewFeedRequest.BirthDate.HasValue ? FunctionConvert.ConvertMilisecondToDateTime(updateNewFeedRequest.BirthDate): newsFeed.BirthDate;
                newsFeed.Status = updateNewFeedRequest.Status ?? newsFeed.Status;
                if (!updateNewFeedRequest.ImageUpdateRequests.IsNullOrEmpty())
                {
                    foreach (var image in updateNewFeedRequest.ImageUpdateRequests)
                    {
                        if (image.ImageID.HasValue)
                        {
                            Image imageUpdate = _dBContext.Images.Where(t => t.NewsFeedID.Equals(image.ImageID)).FirstOrDefault();
                            using (var memoryStream = new MemoryStream())
                            {
                                image.SourceImage.CopyTo(memoryStream);
                                byte[] fileBytes = memoryStream.ToArray();
                                imageUpdate.SourceImage = fileBytes;
                            }
                            imageUpdate.UpdateDate = DateTime.Now;
                            _dBContext.Images.Update(imageUpdate);
                            _dBContext.SaveChanges();
                        }
                        else
                        {
                            Image imageUpdate = new();
                            using (var memoryStream = new MemoryStream())
                            {
                                image.SourceImage.CopyTo(memoryStream);
                                byte[] fileBytes = memoryStream.ToArray();
                                imageUpdate.SourceImage = fileBytes;
                            }
                            imageUpdate.NewsFeedID = updateNewFeedRequest.NewsFeedID;
                            imageUpdate.InsertDate =  DateTime.Now;
                            _dBContext.Images.Add(imageUpdate);
                            _dBContext.SaveChanges();
                        }
                    }                }
                _dBContext.NewsFeeds.Update(newsFeed);
                _dBContext.SaveChanges();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}

