﻿using System.ComponentModel.DataAnnotations;

namespace P7CreateRestApi.Domain.DTOS.Requests
{
    public class UserLoginRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
