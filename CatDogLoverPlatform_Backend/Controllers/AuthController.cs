using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatDogLover_Repository.DAO;
using CatDogLover_Repository.DTO;
using CatDogLover_Repository.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CatDogLoverPlatform_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    { 
        private readonly CatDogLoverDBContext _dBContext;
        public AuthController(CatDogLoverDBContext dBContext)
        {
            _dBContext = dBContext;
        }
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest)
        {
            try
            {
                User checkUser = _dBContext.Users.Where(t => t.Email.Equals(registerRequest.Email)).FirstOrDefault();
                if(checkUser == null)
                {
                    User newUser = FunctionConvert.ConvertObjectToObject<User, RegisterRequest>(registerRequest);
                    newUser.BirthDate = FunctionConvert.ConvertMilisecondToDateTime(registerRequest.BirthDate);
                    newUser.Password = FunctionConvert.HashPassword(registerRequest.Password);
                    newUser.InsertDate = DateTime.Now;
                    newUser.Status = 2;
                    newUser.RoleID = _dBContext.Roles.Where(t => t.RoleName.Equals("MEMBER")).FirstOrDefault().RoleID;
                    _dBContext.Users.Add(newUser);
                    _dBContext.SaveChanges();
                    FunctionConvert.sendOtp(_dBContext, newUser);
                    return Ok("Create Account Success");
                }else
                {
                    return BadRequest("Email has used");
                }

            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
        [HttpPost]
        [Route("confrim-account")]
        public async Task<IActionResult> ConfirmAccount([FromBody] ConfirmRequest confirmRequest)
        {
            try
            {
                User user = _dBContext.Users.Where(t => t.Email.Equals(confirmRequest.email)).FirstOrDefault();
                if(user != null && user.Otp.Equals(confirmRequest.Otp))
                {
                    user.Status = 1;
                    _dBContext.Users.Update(user);
                    _dBContext.SaveChanges();
                    return Ok(user.UserID);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            try
            {
                User userLogin = _dBContext.Users.Where(t => t.Email.Equals(loginRequest.Email) && t.Status == 1).Include(t=>t.Role).FirstOrDefault();
                if(userLogin != null && FunctionConvert.VerifyPassword(loginRequest.Password, userLogin.Password))
                {
                    UserDTO userDTO = FunctionConvert.ConvertObjectToObject<UserDTO, User>(userLogin);
                    userDTO.InsertedDated = (long)FunctionConvert.ConvertDateTimeToMilisecond(userLogin.InsertDate);
                    try
                    {
                        userDTO.UpdatedDate = (long)FunctionConvert.ConvertDateTimeToMilisecond(userLogin.UpdateDate);
                    }
                    catch
                    {
                        userDTO.UpdatedDate = 0;
                    }
                    userDTO.RoleName = userLogin.Role.RoleName;
                    return Ok(userDTO);
                }
                return BadRequest("Login Failed");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPost]
        [Route("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest resetPasswordRequest)
        {
            try
            {
                User user = _dBContext.Users.Where(t => t.Email.Equals(resetPasswordRequest.Email) && t.Status == 1).FirstOrDefault();
                if (user != null)
                {
                    FunctionConvert.sendOtp(_dBContext, user);
                    return Ok("Send Opt Success");
                }
                return BadRequest("Send Otp Unsuccess");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPost]
        [Route("confrim-new-password")]
        public async Task<IActionResult> ConfirmNewPassword([FromBody] ConfirmNewPassword confirmNewPassword)
        {
            try
            {
                User user = _dBContext.Users.Where(t => t.Email.Equals(confirmNewPassword.Email) && t.Status == 1 && t.Otp.Equals(confirmNewPassword.Otp)).FirstOrDefault();
                if (user != null)
                {
                    user.Password = FunctionConvert.HashPassword(confirmNewPassword.Password);
                    _dBContext.Users.Update(user);
                    _dBContext.SaveChanges();
                    return Ok("reset password Success");
                }
                return BadRequest("reset password Unsuccess");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}

