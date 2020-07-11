using DondeLa_tuty.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DondeLa_tuty.ViewModels
{
    public class ShoppingCartViewModel
    {
        
        
        public List<Cart> CartItems { get; set; }
        public decimal CartTotal { get; set; }
       

    }
}