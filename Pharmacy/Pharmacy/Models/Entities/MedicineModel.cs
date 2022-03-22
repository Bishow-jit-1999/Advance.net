using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Pharmacy.Models.Entities
{
    public class MedicineModel
    {
        public int id { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        public int rate { get; set; }
        [Required]
        public string generic { get; set; }
        public Nullable<int> supplier_id { get; set; }
        [Required]
        public int quantity { get; set; }
        public Nullable<System.DateTime> expired_date { get; set; }
        public Nullable<int> category_id { get; set; }
    }
}