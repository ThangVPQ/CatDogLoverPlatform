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
        [Route("create-news-feed")]
        public async Task<IActionResult> CreateNewFeed([FromBody] NewsFeedRequest newFeedRequest)
        {
            try
            {
                NewsFeed newsFeed = FunctionConvert.ConvertObjectToObject<NewsFeed, NewsFeedRequest>(newFeedRequest);
                newsFeed.TypeNewsFeedID = _dBContext.TypeNewsFeeds.Where(t => t.TypesNewFeedName.Equals("New Feed")).FirstOrDefault().TypesNewFeedID;
                newsFeed.InsertDate = DateTime.Now;
                newsFeed.Status = 1;
                _dBContext.NewsFeeds.Add(newsFeed);
                _dBContext.SaveChanges();
                if (newFeedRequest.ListImage != null)
                {
                    foreach (var Image in newFeedRequest.ListImage)
                    {
                            Image imageInit = new Image()
                            {
                                InsertDate = (DateTime)newsFeed.InsertDate,
                                UrlImage = Image,
                                NewsFeedID = (Guid)newsFeed.NewsFeedID
                            };
                            _dBContext.Images.Add(imageInit);
                            _dBContext.SaveChanges();
                    }
                }
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
        [HttpPost]
        [Route("create-news-feed-for-sale")]
        public async Task<IActionResult> CreateNewFeedForSale([FromBody] NewsFeedForSaleRequest newFeedRequest)
        {
            try
            {
                NewsFeed newsFeed = FunctionConvert.ConvertObjectToObject<NewsFeed, NewsFeedForSaleRequest>(newFeedRequest);
                newsFeed.TypeNewsFeedID = _dBContext.TypeNewsFeeds.Where(t => t.TypesNewFeedName.Equals("Sale Product")).FirstOrDefault().TypesNewFeedID;
                newsFeed.InsertDate = DateTime.Now;
                newsFeed.Status = 3;
                _dBContext.NewsFeeds.Add(newsFeed);
                _dBContext.SaveChanges();
                if (newFeedRequest.ListImage != null)
                {
                    foreach (var image in newFeedRequest.ListImage)
                    {

                            Image imageInit = new Image()
                            {
                                InsertDate = (DateTime)newsFeed.InsertDate,
                                UrlImage = image,
                                NewsFeedID = (Guid)newsFeed.NewsFeedID
                            };
                            _dBContext.Images.Add(imageInit);
                            _dBContext.SaveChanges();
                    }
                }
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
        [HttpPost]
        [Route("get-news-feed-for-sale")]
        public async Task<IActionResult> GetNewsFeedForSales([FromBody] PaginateRequest paginateRequest)
        {
            try
            {
                List<NewsFeed> newsFeeds = _dBContext.NewsFeeds.Where(s => s.Status == 1 && (s.Title.Contains(paginateRequest.Search) || s.Content.Contains(paginateRequest.Search)) && s.TypeNewsFeedID.Equals(_dBContext.TypeNewsFeeds.Where(t => t.TypesNewFeedName.Equals("Sale Product")).FirstOrDefault().TypesNewFeedID)).OrderByDescending(t => t.InsertDate).Skip((paginateRequest.CurrentPage - 1) * paginateRequest.PageSize).Take(paginateRequest.PageSize).ToList();
                List<NewsFeedForSalesDTO> newsFeedDTO = FunctionConvert.ConvertListToList<NewsFeedForSalesDTO, NewsFeed>(newsFeeds);
                for (int i = 0; i < newsFeedDTO.Count; i++)
                {
                    newsFeedDTO[i].UserName = _dBContext.Users.Where(u => u.UserID.Equals(newsFeeds[i].UserID)).FirstOrDefault().FullName;
                    newsFeedDTO[i].TypeGoodsName = _dBContext.TypeGoods.Where(u => u.TypeGoodsID.Equals(newsFeeds[i].TypeGoodsID)).FirstOrDefault().TypeGoodsName;
                    newsFeedDTO[i].InsertDated = FunctionConvert.ConvertDateTimeToMilisecond(newsFeeds[i].InsertDate);
                    if (newsFeeds[i].UpdateDate.HasValue)
                    {
                        newsFeedDTO[i].UpdateDated = FunctionConvert.ConvertDateTimeToMilisecond(newsFeeds[i].UpdateDate);
                    }
                    int countComments = _dBContext.Comments.Where(t => t.NewsFeedID.Equals(newsFeeds[i].NewsFeedID)).Count();
                    int countLike = _dBContext.NumberOfInteractions.Where(t => t.NewsFeedID.Equals(newsFeeds[i].NewsFeedID)).Count();
                    List<Image> images = _dBContext.Images.Where(t => t.NewsFeedID.Equals(newsFeeds[i].NewsFeedID)).ToList();
                    List<ImageDTO> imageDTOs = FunctionConvert.ConvertListToList<ImageDTO, Image>(images);
                    newsFeedDTO[i].CommentQuantity = countComments;
                    newsFeedDTO[i].LikeQuantity = countLike;
                    newsFeedDTO[i].ListImages = imageDTOs;
                }
                return Ok(new PaginatedData<NewsFeedForSalesDTO>
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
        [Route("get-news-feed")]
        public async Task<IActionResult> GetNewsFeed([FromBody] PaginateRequest paginateRequest)
        {
            try
            {
                List<NewsFeed> newsFeeds = _dBContext.NewsFeeds.Where(s => s.Status == 1 && (s.Title.Contains(paginateRequest.Search) || s.Content.Contains(paginateRequest.Search)) && s.TypeNewsFeedID.Equals(_dBContext.TypeNewsFeeds.Where(t => t.TypesNewFeedName.Equals("New Feed")).FirstOrDefault().TypesNewFeedID)).OrderByDescending(t => t.InsertDate).Skip((paginateRequest.CurrentPage - 1) * paginateRequest.PageSize).Take(paginateRequest.PageSize).ToList();
                int count = newsFeeds.Count;
                List<NewsFeedDTO> newsFeedDTO = FunctionConvert.ConvertListToList<NewsFeedDTO, NewsFeed>(newsFeeds);
                for (int i = 0; i < newsFeedDTO.Count; i++)
                {
                    newsFeedDTO[i].UserName = _dBContext.Users.Where(u => u.UserID.Equals(newsFeeds[i].UserID)).FirstOrDefault().FullName;
                    newsFeedDTO[i].InsertDated = FunctionConvert.ConvertDateTimeToMilisecond(newsFeeds[i].InsertDate);
                    if (newsFeeds[i].UpdateDate.HasValue)
                    {
                        newsFeedDTO[i].UpdateDated = FunctionConvert.ConvertDateTimeToMilisecond(newsFeeds[i].UpdateDate);
                    }
                    int countComments = _dBContext.Comments.Where(t => t.NewsFeedID.Equals(newsFeeds[i].NewsFeedID)).Count();
                    int countLike = _dBContext.NumberOfInteractions.Where(t => t.NewsFeedID.Equals(newsFeeds[i].NewsFeedID)).Count();
                    List<Image> images = _dBContext.Images.Where(t => t.NewsFeedID.Equals(newsFeeds[i].NewsFeedID)).ToList();
                    List<ImageDTO> imageDTOs = FunctionConvert.ConvertListToList<ImageDTO, Image>(images);
                    newsFeedDTO[i].LikeQuantity = countLike;
                    newsFeedDTO[i].CommentQuantity = countComments;
                    newsFeedDTO[i].ListImages = imageDTOs;
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
        public async Task<IActionResult> UpdateNewFeed([FromBody] UpdateNewFeedRequest updateNewFeedRequest)
        {
            try
            {
                NewsFeed newsFeed = _dBContext.NewsFeeds.Where(s => s.NewsFeedID.Equals(updateNewFeedRequest.NewsFeedID)).FirstOrDefault();
                newsFeed.NewsFeedID = updateNewFeedRequest.NewsFeedID;
                newsFeed.TypeGoodsID = updateNewFeedRequest.TypeGoodsID ?? newsFeed.TypeGoodsID;
                newsFeed.Title = updateNewFeedRequest.Title ?? newsFeed.Title;
                newsFeed.Content = updateNewFeedRequest.Content ?? newsFeed.Content;
                newsFeed.Status = updateNewFeedRequest.Status ?? newsFeed.Status;
                newsFeed.UpdateDate = DateTime.Now;
                newsFeed.UpdateBy = updateNewFeedRequest.UserID;
                if (!updateNewFeedRequest.ImageUpdateRequests.IsNullOrEmpty())
                {
                    foreach (var image in updateNewFeedRequest.ImageUpdateRequests)
                    {
                        if (image.ImageID.HasValue)
                        {
                            Image imageUpdate = _dBContext.Images.Where(t => t.NewsFeedID.Equals(image.ImageID)).FirstOrDefault();
                            imageUpdate.UrlImage = image.UrlImage;
                            imageUpdate.UpdateDate = DateTime.Now;
                            _dBContext.Images.Update(imageUpdate);
                            _dBContext.SaveChanges();
                        }
                        else
                        {
                            Image imageUpdate = new();
                            imageUpdate.UrlImage = image.UrlImage;
                            imageUpdate.NewsFeedID = updateNewFeedRequest.NewsFeedID;
                            imageUpdate.InsertDate = DateTime.Now;
                            _dBContext.Images.Add(imageUpdate);
                            _dBContext.SaveChanges();
                        }
                    }
                }
                _dBContext.NewsFeeds.Update(newsFeed);
                _dBContext.SaveChanges();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
        [HttpPost]
        [Route("update-news-feed-for-sales")]
        public async Task<IActionResult> UpdateNewFeedForSales([FromBody] UpdateNewFeedForSalesRequest updateNewFeedRequest)
        {
            try
            {
                NewsFeed newsFeed = _dBContext.NewsFeeds.Where(s => s.NewsFeedID.Equals(updateNewFeedRequest.NewsFeedID)).FirstOrDefault();
                newsFeed.NewsFeedID = updateNewFeedRequest.NewsFeedID;
                newsFeed.TypeGoodsID = updateNewFeedRequest.TypeGoodsID ?? newsFeed.TypeGoodsID;
                newsFeed.Title = updateNewFeedRequest.Title ?? newsFeed.Title;
                newsFeed.Content = updateNewFeedRequest.Content ?? newsFeed.Content;
                newsFeed.Price = updateNewFeedRequest.Price ?? newsFeed.Price;
                newsFeed.PhoneNumber = updateNewFeedRequest.PhoneNumber ?? newsFeed.PhoneNumber;
                newsFeed.Address = updateNewFeedRequest.Address ?? newsFeed.Address;
                newsFeed.UpdateDate = DateTime.Now;
                newsFeed.UpdateBy = updateNewFeedRequest.UserID;
                newsFeed.Status = 3;
                if (!updateNewFeedRequest.ImageUpdateRequests.IsNullOrEmpty())
                {
                    foreach (var image in updateNewFeedRequest.ImageUpdateRequests)
                    {
                        if (image.ImageID.HasValue)
                        {
                            Image imageUpdate = _dBContext.Images.Where(t => t.NewsFeedID.Equals(image.ImageID)).FirstOrDefault();
                            imageUpdate.UrlImage = image.UrlImage;
                            imageUpdate.UpdateDate = DateTime.Now;
                            _dBContext.Images.Update(imageUpdate);
                            _dBContext.SaveChanges();
                        }
                        else
                        {
                            Image imageUpdate = new();
                            imageUpdate.UrlImage = image.UrlImage;
                            imageUpdate.NewsFeedID = updateNewFeedRequest.NewsFeedID;
                            imageUpdate.InsertDate = DateTime.Now;
                            _dBContext.Images.Add(imageUpdate);
                            _dBContext.SaveChanges();
                        }
                    }
                }
                _dBContext.NewsFeeds.Update(newsFeed);
                _dBContext.SaveChanges();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
        [HttpPost]
        [Route("get-news-feed-by-id")]
        public async Task<IActionResult> GetNewsFeedById([FromBody] IdRequest idRequest)
        {
            try
            {
                NewsFeed newsFeed = _dBContext.NewsFeeds.Where(s => s.NewsFeedID.Equals(idRequest.Id)).FirstOrDefault();
                NewsFeedByID newsFeedDTO = FunctionConvert.ConvertObjectToObject<NewsFeedByID, NewsFeed>(newsFeed);
                newsFeedDTO.UserName = _dBContext.Users.Where(u => u.UserID.Equals(newsFeed.UserID)).FirstOrDefault().FullName;
                newsFeedDTO.InsertDated = FunctionConvert.ConvertDateTimeToMilisecond(newsFeed.InsertDate);
                int countComments = _dBContext.Comments.Where(t => t.NewsFeedID.Equals(newsFeed.NewsFeedID)).Count();
                int countLike = _dBContext.NumberOfInteractions.Where(t => t.NewsFeedID.Equals(newsFeed.NewsFeedID)).Count();
                List<Image> images = _dBContext.Images.Where(t => t.NewsFeedID.Equals(newsFeed.NewsFeedID)).ToList();
                List<ImageDTO> imageDTOs = FunctionConvert.ConvertListToList<ImageDTO, Image>(images);
                List<Comment> comments = _dBContext.Comments.Where(t => t.NewsFeedID.Equals(newsFeed.NewsFeedID) && t.Status == 1).ToList();
                List<CommentDTO> commentDTOs = FunctionConvert.ConvertListToList<CommentDTO, Comment>(comments);
                for (int j = 0; j < commentDTOs.Count; j++)
                {
                    commentDTOs[j].InsertDated = (long)FunctionConvert.ConvertDateTimeToMilisecond(comments[j].InsertDate);
                    commentDTOs[j].UserName = _dBContext.Users.Where(t => t.UserID.Equals(comments[j].UserID)).FirstOrDefault().FullName;
                }
                List<NumberOfInteraction> numberOfInteractions = _dBContext.NumberOfInteractions.Where(t => t.NewsFeedID.Equals(newsFeed.NewsFeedID)).ToList();
                List<NumberOfInteractionDTO> numberOfInteractionDTOs = FunctionConvert.ConvertListToList<NumberOfInteractionDTO, NumberOfInteraction>(numberOfInteractions);
                for (int k = 0; k < numberOfInteractionDTOs.Count; k++)
                {
                    numberOfInteractionDTOs[k].InsertDated = (long)FunctionConvert.ConvertDateTimeToMilisecond(numberOfInteractions[k].InsertDate);
                    numberOfInteractionDTOs[k].UserName = _dBContext.Users.Where(t => t.UserID.Equals(numberOfInteractions[k].UserID)).FirstOrDefault().FullName;
                }
                newsFeedDTO.LikeQuantity = countLike;
                newsFeedDTO.CommentQuantity = countComments;
                newsFeedDTO.ListImages = imageDTOs;
                newsFeedDTO.NumberOfInteractionDTOs = numberOfInteractionDTOs;
                newsFeedDTO.CommentDTOs = commentDTOs;
                return Ok(newsFeedDTO);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
        [HttpPost]
        [Route("get-news-feed-for-sale-by-id")]
        public async Task<IActionResult> GetNewsFeedForSaleById([FromBody] IdRequest idRequest)
        {
            try
            {
                NewsFeed newsFeed = _dBContext.NewsFeeds.Where(s => s.NewsFeedID.Equals(idRequest.Id)).FirstOrDefault();
                NewsFeedForSaleByID newsFeedDTO = FunctionConvert.ConvertObjectToObject<NewsFeedForSaleByID, NewsFeed>(newsFeed);
                newsFeedDTO.UserName = _dBContext.Users.Where(u => u.UserID.Equals(newsFeed.UserID)).FirstOrDefault().FullName;
                newsFeedDTO.InsertDated = FunctionConvert.ConvertDateTimeToMilisecond(newsFeed.InsertDate);
                int countComments = _dBContext.Comments.Where(t => t.NewsFeedID.Equals(newsFeed.NewsFeedID)).Count();
                int countLike = _dBContext.NumberOfInteractions.Where(t => t.NewsFeedID.Equals(newsFeed.NewsFeedID)).Count();
                List<Image> images = _dBContext.Images.Where(t => t.NewsFeedID.Equals(newsFeed.NewsFeedID)).ToList();
                List<ImageDTO> imageDTOs = FunctionConvert.ConvertListToList<ImageDTO, Image>(images);
                List<Comment> comments = _dBContext.Comments.Where(t => t.NewsFeedID.Equals(newsFeed.NewsFeedID) && t.Status == 1).ToList();
                List<CommentDTO> commentDTOs = FunctionConvert.ConvertListToList<CommentDTO, Comment>(comments);
                for (int j = 0; j < commentDTOs.Count; j++)
                {
                    commentDTOs[j].InsertDated = (long)FunctionConvert.ConvertDateTimeToMilisecond(comments[j].InsertDate);
                    commentDTOs[j].UserName = _dBContext.Users.Where(t => t.UserID.Equals(comments[j].UserID)).FirstOrDefault().FullName;
                }
                List<NumberOfInteraction> numberOfInteractions = _dBContext.NumberOfInteractions.Where(t => t.NewsFeedID.Equals(newsFeed.NewsFeedID)).ToList();
                List<NumberOfInteractionDTO> numberOfInteractionDTOs = FunctionConvert.ConvertListToList<NumberOfInteractionDTO, NumberOfInteraction>(numberOfInteractions);
                for (int k = 0; k < numberOfInteractionDTOs.Count; k++)
                {
                    numberOfInteractionDTOs[k].InsertDated = (long)FunctionConvert.ConvertDateTimeToMilisecond(numberOfInteractions[k].InsertDate);
                    numberOfInteractionDTOs[k].UserName = _dBContext.Users.Where(t => t.UserID.Equals(numberOfInteractions[k].UserID)).FirstOrDefault().FullName;
                }
                newsFeedDTO.LikeQuantity = countLike;
                newsFeedDTO.CommentQuantity = countComments;
                newsFeedDTO.ListImages = imageDTOs;
                newsFeedDTO.NumberOfInteractionDTOs = numberOfInteractionDTOs;
                newsFeedDTO.CommentDTOs = commentDTOs;
                return Ok(newsFeedDTO);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
        [HttpPost]
        [Route("get-news-feed-for-sale-by-user-id")]
        public async Task<IActionResult> GetNewsFeedForSalebyUserID([FromBody] PaginateRequestByUserID paginateRequest)
        {
            try
            {
                List<NewsFeed> newsFeeds = _dBContext.NewsFeeds.Where(s => s.Status != 2 && s.UserID.Equals(paginateRequest.UserID) && (s.Title.Contains(paginateRequest.Search) || s.Content.Contains(paginateRequest.Search)) && s.TypeNewsFeedID.Equals(_dBContext.TypeNewsFeeds.Where(t => t.TypesNewFeedName.Equals("Sale Product")).FirstOrDefault().TypesNewFeedID)).Skip((paginateRequest.CurrentPage - 1) * paginateRequest.PageSize).Take(paginateRequest.PageSize).ToList();
                List<NewsFeedForSalesDTO> newsFeedDTO = FunctionConvert.ConvertListToList<NewsFeedForSalesDTO, NewsFeed>(newsFeeds);
                for (int i = 0; i < newsFeedDTO.Count; i++)
                {
                    newsFeedDTO[i].UserName = _dBContext.Users.Where(u => u.UserID.Equals(newsFeeds[i].UserID)).FirstOrDefault().FullName;
                    newsFeedDTO[i].TypeGoodsName = _dBContext.TypeGoods.Where(u => u.TypeGoodsID.Equals(newsFeeds[i].TypeGoodsID)).FirstOrDefault().TypeGoodsName;
                    newsFeedDTO[i].InsertDated = FunctionConvert.ConvertDateTimeToMilisecond(newsFeeds[i].InsertDate);
                    int countComments = _dBContext.Comments.Where(t => t.NewsFeedID.Equals(newsFeeds[i].NewsFeedID)).Count();
                    int countLike = _dBContext.NumberOfInteractions.Where(t => t.NewsFeedID.Equals(newsFeeds[i].NewsFeedID)).Count();
                    List<Image> images = _dBContext.Images.Where(t => t.NewsFeedID.Equals(newsFeeds[i].NewsFeedID)).ToList();
                    List<ImageDTO> imageDTOs = FunctionConvert.ConvertListToList<ImageDTO, Image>(images);
                    newsFeedDTO[i].CommentQuantity = countComments;
                    newsFeedDTO[i].LikeQuantity = countLike;
                    newsFeedDTO[i].ListImages = imageDTOs;
                }
                return Ok(new PaginatedData<NewsFeedForSalesDTO>
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
        [Route("get-news-feed-by-user-id")]
        public async Task<IActionResult> GetNewsFeedByUserID([FromBody] PaginateRequestByUserID paginateRequest)
        {
            try
            {
                List<NewsFeed> newsFeeds = _dBContext.NewsFeeds.Where(s => s.Status != 2 && s.UserID.Equals(paginateRequest.UserID) && (s.Title.Contains(paginateRequest.Search) || s.Content.Contains(paginateRequest.Search)) && s.TypeNewsFeedID.Equals(_dBContext.TypeNewsFeeds.Where(t => t.TypesNewFeedName.Equals("New Feed")).FirstOrDefault().TypesNewFeedID)).Skip((paginateRequest.CurrentPage - 1) * paginateRequest.PageSize).Take(paginateRequest.PageSize).ToList();
                int count = newsFeeds.Count;
                List<NewsFeedDTO> newsFeedDTO = FunctionConvert.ConvertListToList<NewsFeedDTO, NewsFeed>(newsFeeds);
                for (int i = 0; i < newsFeedDTO.Count; i++)
                {
                    newsFeedDTO[i].UserName = _dBContext.Users.Where(u => u.UserID.Equals(newsFeeds[i].UserID)).FirstOrDefault().FullName;
                    newsFeedDTO[i].InsertDated = FunctionConvert.ConvertDateTimeToMilisecond(newsFeeds[i].InsertDate);
                    int countComments = _dBContext.Comments.Where(t => t.NewsFeedID.Equals(newsFeeds[i].NewsFeedID)).Count();
                    int countLike = _dBContext.NumberOfInteractions.Where(t => t.NewsFeedID.Equals(newsFeeds[i].NewsFeedID)).Count();
                    List<Image> images = _dBContext.Images.Where(t => t.NewsFeedID.Equals(newsFeeds[i].NewsFeedID)).ToList();
                    List<ImageDTO> imageDTOs = FunctionConvert.ConvertListToList<ImageDTO, Image>(images);
                    newsFeedDTO[i].LikeQuantity = countLike;
                    newsFeedDTO[i].CommentQuantity = countComments;
                    newsFeedDTO[i].ListImages = imageDTOs;
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
        [Route("get-news-feed-for-sale-with-status-request")]
        public async Task<IActionResult> GetNewsFeedForSaleWithStatusRequest([FromBody] PaginateRequest paginateRequest)
        {
            try
            {
                List<NewsFeed> newsFeeds = _dBContext.NewsFeeds.Where(s => s.Status == 3 && (s.Title.Contains(paginateRequest.Search) || s.Content.Contains(paginateRequest.Search)) && s.TypeNewsFeedID.Equals(_dBContext.TypeNewsFeeds.Where(t => t.TypesNewFeedName.Equals("Sale Product")).FirstOrDefault().TypesNewFeedID)).OrderByDescending(t => t.InsertDate).Skip((paginateRequest.CurrentPage - 1) * paginateRequest.PageSize).Take(paginateRequest.PageSize).ToList();
                List<NewsFeedForSalesDTO> newsFeedDTO = FunctionConvert.ConvertListToList<NewsFeedForSalesDTO, NewsFeed>(newsFeeds);
                for (int i = 0; i < newsFeedDTO.Count; i++)
                {
                    newsFeedDTO[i].UserName = _dBContext.Users.Where(u => u.UserID.Equals(newsFeeds[i].UserID)).FirstOrDefault().FullName;
                    newsFeedDTO[i].TypeGoodsName = _dBContext.TypeGoods.Where(u => u.TypeGoodsID.Equals(newsFeeds[i].TypeGoodsID)).FirstOrDefault().TypeGoodsName;
                    newsFeedDTO[i].InsertDated = FunctionConvert.ConvertDateTimeToMilisecond(newsFeeds[i].InsertDate);
                    int countComments = _dBContext.Comments.Where(t => t.NewsFeedID.Equals(newsFeeds[i].NewsFeedID)).Count();
                    int countLike = _dBContext.NumberOfInteractions.Where(t => t.NewsFeedID.Equals(newsFeeds[i].NewsFeedID)).Count();
                    List<Image> images = _dBContext.Images.Where(t => t.NewsFeedID.Equals(newsFeeds[i].NewsFeedID)).ToList();
                    List<ImageDTO> imageDTOs = FunctionConvert.ConvertListToList<ImageDTO, Image>(images);
                    newsFeedDTO[i].CommentQuantity = countComments;
                    newsFeedDTO[i].LikeQuantity = countLike;
                    newsFeedDTO[i].ListImages = imageDTOs;
                }
                return Ok(new PaginatedData<NewsFeedForSalesDTO>
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
        [Route("confirm-news-feed-for-sales")]
        public async Task<IActionResult> ConfirmNewsFeedforSale([FromBody] ConfirmNewsFeedForSale confirmNewsFeedForSale)
        {
            try
            {
                NewsFeed newsFeed = _dBContext.NewsFeeds.Where(t => t.NewsFeedID.Equals(confirmNewsFeedForSale.NewsFeedID)).FirstOrDefault();
                newsFeed.Status = confirmNewsFeedForSale.Status? 1 : 4;
                _dBContext.NewsFeeds.Update(newsFeed);
                _dBContext.SaveChanges();
                return Ok(newsFeed);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
    }
}

