using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppinglistApp.Models
{
    public class Shoppinglist
    {
        [Key]
        public int UserID { get; set; }
        public int ListID { get; set; }
        public ICollection<ShoppingListItem> Items { get; set; }
        public ICollection<User> AllowedUsers { get; set; }
    }
}
