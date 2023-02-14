using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace RealStateApi.Models
{
    public class Property
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Detail { get; set; }
        public string Address { get; set; }
        public string ImageUrl { get; set; }
        public double price { get; set; }
        public bool IsTrending { get; set; }

        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        [JsonIgnore]
        [ValidateNever]
        public Category Category { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        [ValidateNever]
        [JsonIgnore]
        public User User { get; set; }
    }
}
