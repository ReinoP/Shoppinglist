using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppinglistApp.Models
{
  public class Shoppinglist
  {  
    public string UserID { get; set; }
    [Key]
    public int ListID { get; set; }
    public string ListName { get; set; }

    public ICollection<ShoppingListItem> Items { get; set; }
    public ICollection<User> AllowedUsers { get; set; }
  }
}
