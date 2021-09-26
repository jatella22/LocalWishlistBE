using LocalWishlistBE.Models.Enums;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LocalWishlistBE.Models
{
    public class User : IdentityUser
    {
        public string Name { get; set; } 
        public string LastName { get; set; }
        public int UserType { get; set; }
    }
}
