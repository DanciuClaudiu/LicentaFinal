using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections;
using LicentaFinal.Models;

namespace LicentaFinal.ViewModels
{
    public class CartViewModel
    {
        public IEnumerable<Models.Cart> Cart { get; set; }

        public List<Instrument> RecomendedInstruments { get; set; }
    }
}
