using System;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using CatDogLover_Repository.DAO;

namespace CatDogLover_Repository.Utils
{
    public class FunctionConvert
    {
        #region Convert Object to object
        public static TDestination ConvertObjectToObject<TDestination, TSource>(TSource source)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<TSource, TDestination>());
            var mapper = new Mapper(config);
            return mapper.Map<TDestination>(source);
        }
        #endregion

        #region Convert list Object to list object
        public static List<TDestination> ConvertListToList<TDestination, TSource>(List<TSource> sourceList)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<TSource, TDestination>());
            var mapper = new Mapper(config);
            return mapper.Map<List<TDestination>>(sourceList);
        }
        #endregion

        #region Convert DateTime To Milisecond
        public static long? ConvertDateTimeToMilisecond(DateTime? dateTime)
        {
            long? convert = null;

            if (dateTime != null)
            {
                convert = new DateTimeOffset((DateTime)dateTime).ToUnixTimeMilliseconds();
            }

            return convert;
        }
        #endregion

        #region Convert Milisecond to DateTime
        public static DateTime ConvertMilisecondToDateTime(long? dateTimeLong)
        {
            return DateTimeOffset.FromUnixTimeSeconds((long)dateTimeLong).DateTime;
        }
        #endregion
        public static string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }
        public static bool VerifyPassword(string inputPassword, string hashedPassword)
        {
            string inputHash = HashPassword(inputPassword);
            return inputHash == hashedPassword;
        }
        public static void sendOtp(CatDogLoverDBContext dogLoverDBContext, User user)
        {
            string smtpServer = "smtp.gmail.com"; // Thay thế bằng máy chủ SMTP của bạn
            int smtpPort = 587; // Thay thế bằng cổng SMTP của bạn
            string smtpUsername = "Thangvpqse150437@gmail.com";
            string smtpPassword = "gssmyphedhnasttc";

            // Tạo đối tượng SmtpClient
            SmtpClient smtpClient = new SmtpClient(smtpServer)
            {
                Port = smtpPort,
                Credentials = new NetworkCredential(smtpUsername, smtpPassword),
                EnableSsl = true, // Sử dụng SSL để bảo mật
            };
            Random random = new Random();
            string opt = "";

            for (int i = 0; i < 6; i++)
            {
                int randomNumber = random.Next(0, 10); // Sinh số ngẫu nhiên từ 0 đến 9
                opt += randomNumber.ToString();
            }
            MailMessage mailMessage = new MailMessage
            {
                From = new MailAddress("Thangvpqse150437@gmail.com"), // Địa chỉ email gửi
                Subject = "OPT CONFIRM", // Tiêu đề email
                Body = opt, // Nội dung email
                IsBodyHtml = false, // Có sử dụng HTML trong nội dung email hay không
            };
            // Thêm địa chỉ email người nhận
            mailMessage.To.Add(user.Email);

            try
            {
                // Gửi email
                smtpClient.Send(mailMessage);
                user.Otp = opt;
                dogLoverDBContext.Users.Update(user);
                dogLoverDBContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi gửi email: " + ex.Message);
            }
        }
    }
    
}

