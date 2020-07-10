using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DondeLa_tuty.Models
{
    public class Item
    {
		public int ItemId { get; set; }

		public int CategoriaId { get; set; }
		public int ProducerID { get; set; }
		public string Titulo { get; set; }
		public decimal Precio { get; set; }
		public string ItemArtUrl { get; set; }
		public Category Categoria { get; set; }
		public Producer Producer { get; set; }
    }
}