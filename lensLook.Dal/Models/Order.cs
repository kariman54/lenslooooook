using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace lensLook.Dal.Models
{
	public class Order
	{

        public int Id { get; set; }

        public string UserIdCreateOrder { get; set; }


        public string  NamePersone { get; set; }

        public Order(string buyerEmail, string shippingAddress,  decimal subTotal, ICollection<OrderItem> items)
		{
			BuyerEmail = buyerEmail;


			ShippingAddress = shippingAddress;
			SubTotal = subTotal;
			Items = items;

		}

        public Order()
        {
            
        }

		public string BuyerEmail { get; set; }


		public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;


		public string Status { get; set; } = "Pending";
		public string ShippingAddress { get; set; } // One To One   Total of the Two Dimations






		public ICollection<OrderItem> Items { get; set; } = new HashSet<OrderItem>();
		public decimal SubTotal { get; set; }



		//[NotMapped] // Don`t Mapp To Column In DataBase
		//public decimal Total => SubTotal + DeliveryMethod.Cost;  //drevied Attribute - SubTotal+DeliveryCoast



		#region Dervied Attribute
		public decimal GetTotal()
		{
			return SubTotal + 10;
		}
		#endregion




	}
}
