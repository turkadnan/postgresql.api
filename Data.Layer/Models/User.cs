using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Layer.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string EMail { get; set; }
        public string Password { get; set; }
        public byte Status { get; set; }
        public DateTime CreateDate { get; set; }
    }
}