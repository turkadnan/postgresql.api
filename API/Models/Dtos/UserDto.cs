using System;
using System.ComponentModel.DataAnnotations;

namespace API.Models.Dtos
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string EMail { get; set; }
        //public string Password { get; set; }
        public byte Status { get; set; }
        public DateTime CreateDate { get; set; }

        /*
        public DateTime? CreateDate { get; set; }
        public int? AddPersonID { get; set; }        
        public byte Status { get; set; }
        */
    }
}
