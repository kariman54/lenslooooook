using lensLook.Dal.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lensLook.Dal.Models
{
    public class BasketItems
    {
        public int Id    { get; set; }
        public string Name    { get; set; }
        public decimal  price      { get; set; }
        public int Quantity    { get; set; }
        public string    Photo    { get; set; }


        public int Productid { get; set; }
        public Product Product { get; set; }

        public int CustomerBasketId { get; set; }
        public BasketCustomer BasketCustomer { get; set; }

    }
}
