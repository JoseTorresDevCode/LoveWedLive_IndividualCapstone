using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;


namespace LoveWedLive_Capstone.Models
{
    public class VendorQuote 
    {
        [Key]
        public int Id { get; set; }
        
        public int PriceQuote { get; set; }
        public int QuotedHours { get; set; } 

        [ForeignKey("Vendor")]
        public int VendorId { get; set; }
        public Vendor Vendor { get; set; }

    }
}
