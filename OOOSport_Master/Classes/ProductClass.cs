using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOOSport_Master.Classes
{
    
    public  class ProductClass
    {
        public string Artikle { get; set; }

        private string _productphoto;
        public string ProductPhoto
        {
            get
            {
                return _productphoto;
            }
            set
            {
                if (value == "") 
                {
                    _productphoto = "no product.png";
                }
                else
                {
                    _productphoto= value;
                }
               
            }
        }
        public string _image;
        public string Image
        {
            get
            {
                return _image;
            }
            set
            {
                _image = "Image\\" + _productphoto;
            }
        }
        public string ProductName { get; set; }

        public string ProductDecription { get; set; }

        public string ProductCategory { get; set; }

        public double ProductCost { get; set; }

        public double ProductDiscount { get; set; }

        public double ProductCostDiscount { get; set; }
        public int ProductCount { get; set; }
    }
}
