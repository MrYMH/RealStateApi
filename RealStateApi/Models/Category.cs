using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.Text.Json.Serialization;

namespace RealStateApi.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        [ValidateNever]
        [JsonIgnore]
        public ICollection<Property> Properties { get; set; }
    }
}
