using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.Text.Json.Serialization;

namespace RealStateApi.Models
{
    public class User
    {
        public int Id { get; set; }
        public string? UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? Phone { get; set; }
        [JsonIgnore]
        [ValidateNever]
        public ICollection<Property>? Properties { get; set; }
    }
}
