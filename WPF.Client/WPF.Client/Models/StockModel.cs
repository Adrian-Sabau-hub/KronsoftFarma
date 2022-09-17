using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WPF.Client.Models
{
    public class StockModel : BindableBase
    {
        private Guid stockId;
        public Guid StockId
        {
            get { return stockId; }
            set { SetProperty(ref stockId, value); }
        }

        private Guid prductId;
        public Guid ProductId
        {
            get { return prductId; }
            set { SetProperty(ref prductId, value); }
        }

        private string productName;
        public string ProductName
        {
            get { return productName; }
            set { SetProperty(ref productName, value); }
        }

        private float quantity;
        public float Quantity
        {
            get { return quantity; }
            set { SetProperty(ref quantity, value); }
        }

    }
}
