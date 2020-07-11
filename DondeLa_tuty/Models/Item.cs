using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DondeLa_tuty.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace DondeLa_tuty.Models
{
    [Bind(Exclude ="ItemId")]
    public class Item
    {
        [ScaffoldColumn(false)]
		public int ItemId { get; set; }
        [DisplayName("Category")]
		public int CategoryId { get; set; }
        [DisplayName("Producer")]
		public int ProducerID { get; set; }
        [Required(ErrorMessage = "Se necesita el nombre del articulo")]
        [StringLength(160)]
        public string Titulo { get; set; }
        [Required(ErrorMessage = "Se requiere el precio")]
        [Range(500,5000,ErrorMessage = "El debe estar entre 500 y 5000")]
        public int Precio { get; set; }
        [DisplayName("Item art url")]
        [StringLength(1024)]
		public string ItemArtUrl { get; set; }
		public virtual Category Category { get; set; }
		public virtual Producer Producer { get; set; }
    }    
}