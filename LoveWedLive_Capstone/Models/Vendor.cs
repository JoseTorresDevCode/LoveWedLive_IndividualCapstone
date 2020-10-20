using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LoveWedLive_Capstone.Models
{
    
    public class Vendor
    {
        [Key]
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string VendorType { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string SubscriptionType { get; set; }
        public double Lat { get; set; }
        public double Long { get; set; }

        [ForeignKey("Address")]
        public int AddressId { get; set; }
        public Address Address { get; set; }


        [ForeignKey("IdentityUser")]
        public string IdentityUserId { get; set; }
        public IdentityUser IdentityUser { get; set; }
    }
}
