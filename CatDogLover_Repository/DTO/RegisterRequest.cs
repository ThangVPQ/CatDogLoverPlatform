﻿using System;
using CatDogLover_Repository.DAO;

namespace CatDogLover_Repository.DTO
{
	public class RegisterRequest
	{
        public string Email { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string FirstName { get; set; }
        public string PhoneNumber { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public long BirthDate { get; set; }
    }
}

