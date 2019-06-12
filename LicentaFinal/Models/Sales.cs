using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LicentaFinal.Models
{
    public class Sales
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public int IntrumentId { get; set; }

        public Guid SaleId { get; set; }

        public int Quantity { get; set; }

        public DateTime PurchaseDate { get; set; }

        public int InstrumentPrice { get; set; }
    }
}
