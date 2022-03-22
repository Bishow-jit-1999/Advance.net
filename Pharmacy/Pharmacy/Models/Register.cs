using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Pharmacy.Models
{
    public class Register
    {
        public int id { get; set; }
        [Required]
        public string name { get; set; }
        public string role { get; set; }
        [Required]
        public string email { get; set; }
        [Required]
        public string password { get; set; }
        [Required]
        public string phone { get; set; }
        [Required]
        public string address { get; set; }
        public byte[] is_active { get; set; }
        public System.DateTime created_at { get; set; }
        public System.DateTime updated_at { get; set; }

    }
}