using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace LoveWedLive_Capstone.Models
{
    

    public class QuoteRequest
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "How Many Hours are you Requesting?", Order = -9)]

        public int HoursRequested { get; set; }

        [Display(Name = "Select the Day of your Wedding", Order = -9)]
        public DateTime DayOfWedding { get; set; }
        public QuoteRequest()
        {
            DateAndTimeOfRequest = DateTime.Now;
        }  
        public DateTime DateAndTimeOfRequest { get; private set; }

        [Display(Name = "Address", Order = -9)]
        public string VenueStreetName { get; set; }

        [Display(Name = "City", Order = -9)]
        public string VenueCity { get; set; }

        [Display(Name = "State", Order = -9)]
        public string VenueState { get; set; }

        [Display(Name = "Zip Code", Order = -9)]
        public string VenueZip { get; set; }

        [Display(Name = "Photographer", Order = -9)]

        public bool IsRequestingPhotographer { get; set; }

        [Display(Name = "Photo Booth", Order = -9)]
        public bool IsRequestingPhotoBooth { get; set; }

        [Display(Name = "DJ", Order = -9)]
        public bool IsRequestingDJ { get; set; }

        [Display(Name = "Wedding Officiant", Order = -9)]
        public bool IsRequestingOfficiant { get; set; }

        [Display(Name = "Wedding Day Hair Stylist", Order = -9)]
        public bool IsRequestngWeddingStylist { get; set; }

        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        


    }
}
