using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LicentaFinal.Models
{
    public class Cart
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public int InstrumentId { get; set; }

        public string InstrumentName { get; set; }

        public int InstrumentPrice { get; set; }

        public int InstrumentQuantity { get; set; }

        public string InstrumentType { get; set; }

        public int CartIntrumentQuantity { get; set; }
    }
}
