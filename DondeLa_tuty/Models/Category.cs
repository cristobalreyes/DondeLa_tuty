using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DondeLa_tuty.Models
{
	//clase categoria
    public partial class Category
    {
		public int CategoryId { get; set; }
		public string Nombre { get; set; }
		public string Descripcion { get; set; }
		public List<Item> Items { get; set; }
	}
}