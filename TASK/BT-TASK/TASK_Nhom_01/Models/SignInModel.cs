﻿using System.ComponentModel.DataAnnotations;

namespace TASK_Nhom_01.Models
{
    public class SignInModel
    {
        [Required,EmailAddress]
        public string Email { get; set; } = null!;
        [Required]
        public string Password { get; set; }    
    }
}
