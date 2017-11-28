﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Entities
{
    public class Employee : IEntity
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string MacAddress { get; set; }

    }
}
