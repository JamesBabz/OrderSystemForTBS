﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL.Entities
{
    public class Employee 
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Username { get; set; }
        [NotMapped]
        public string Password { get; set; }
        public bool PasswordReset { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string ColorCode { get; set; }
        public string IsAdmin { get; set; }
        public DateTime LastLogin { get; set; }


    }
}
