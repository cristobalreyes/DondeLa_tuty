using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace DondeLa_tuty.Models
{
	public class ShoppingStoreEntities : DbContext
	{
		public DbSet<Item> Items {get; set;}
		public DbSet<Category> Categories { get; set; }
		public DbSet<Producer> Producers { get; set; }
	}
}