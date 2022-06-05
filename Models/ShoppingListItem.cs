using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppinglistApp.Models
{
    public class ShoppingListItem
    {
        [Key]
        public string ItemName { get; set;}

        public ShoppingListItem(string itemName)
        {
            ItemName = itemName;
        }
    }
}
