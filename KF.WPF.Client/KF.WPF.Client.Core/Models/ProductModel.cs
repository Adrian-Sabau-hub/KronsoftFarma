using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KF.WPF.Client.Core.Models
{
    public class ProductModel : BindableBase
    {
        private Guid productId;
        public Guid ProductId
        {
            get { return productId; }
            set { SetProperty(ref productId, value); }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set { SetProperty(ref name, value); }
        }

        private string description;
        public string Description
        {
            get { return description; }
            set { SetProperty(ref description, value); }
        }

        private decimal price;
        public decimal Price
        {
            get { return price; }
            set { SetProperty(ref price, value); }
        }

        private string producer;
        public string Producer
        {
            get { return producer; }
            set { SetProperty(ref producer, value); }
        }
    }
}
