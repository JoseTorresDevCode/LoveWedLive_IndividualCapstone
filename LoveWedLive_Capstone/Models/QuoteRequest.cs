using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LoveWedLive_Capstone.Models
{
    
    public class QuoteRequest
    {
        [Key]
        public int Id { get; set; }
        public int PriceQuoted { get; set; }
        public int HoursRequested { get; set; }

        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        


    }
}
