using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TestNexMed.Models
{
    public class ModelSityWeather
    {
        [Required]
        [StringLength(50)]
        [Display(Name = "Введите город")]
        public string SityName { get; set; }
    }
}