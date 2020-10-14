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
        public string Id { get; set; }

        [ForeignKey("Vendor")]
        public string VendorId { get; set; }
        public Vendor Vendor { get; set; }

    }
}
