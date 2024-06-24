using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lensLook.Dal.Models
{
	public class OrderItem
	{
        public int Id { get; set; }

        public OrderItem()
		{

		}
		public OrderItem(ProductItemOrder product, decimal price, int quantity)
		{
			Product = product;
			Price = price;
			Quantity = quantity;
		}

		public ProductItemOrder Product { get; set; } //Not Have Change

		public decimal Price { get; set; }// can U Have Deffirent here 

		public int Quantity { get; set; }
	}



	[NotMapped]
	public class ProductItemOrder
	{
		public ProductItemOrder()
		{

		}
		public ProductItemOrder(int productId, string productName, string productPictureUrl)
		{
			ProductId = productId;
			ProductName = productName;
			ProductPictureUrl = productPictureUrl;
		}

		public int ProductId { get; set; }

		public string ProductName { get; set; }
		public string ProductPictureUrl { get; set; }






	}




}
