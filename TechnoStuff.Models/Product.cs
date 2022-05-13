﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechnoStuff.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string Title { get; set; }
        
        public string? Description { get; set; }
        
        [Required]
        public string Manufacturer { get; set; }
        
        [Required]
        [Range(1, 10000)]
        public double ListPrice { get; set; }
        
        [Required]
        [Range(1, 10000)]
        public double Price { get; set; }
        
        [Required]
        [Range(1, 10000)]
        public double Price50 { get; set; }
        
        [Required]
        [Range(1, 10000)]
        public double Price100 { get; set; }
        
        [ValidateNever]
        public string? Image { get; set; }
        
        [Required]
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        [ValidateNever]
        public Category Category { get; set; }
        
        [Required]
        public int LaptopTypeId { get; set; }
        [ForeignKey("LaptopTypeId")]
        [ValidateNever]
        public LaptopType LaptopType { get; set; }
    }
}
