using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        [DisplayName("Id")]
        public int Id { get; set; }

        [DisplayName("Hours Requested")]

        public int HoursRequested { get; set; }

        [DisplayName("Wedding Day")]
        public DateTime DayOfWedding { get; set; }
        public QuoteRequest()
        {
            DateAndTimeOfRequest = DateTime.Now;
        }
        [DisplayName("Submitted")]
        public DateTime DateAndTimeOfRequest { get; private set; }

        [DisplayName("Address")]
        public string VenueStreetName { get; set; }

        [DisplayName("City")]
        public string VenueCity { get; set; }

        [DisplayName("State")]
        public string VenueState { get; set; }

        [DisplayName("Zip")]
        public string VenueZip { get; set; }

        [DisplayName("Photographer")]

        public bool IsRequestingPhotographer { get; set; }

        [DisplayName("Photobooth")]
        public bool IsRequestingPhotoBooth { get; set; }

        [DisplayName("DJ")]
        public bool IsRequestingDJ { get; set; }

        [DisplayName("Wedding Officiant")]
        public bool IsRequestingOfficiant { get; set; }

        [DisplayName("Wedding Day HairStylist")]
        public bool IsRequestngWeddingStylist { get; set; }

        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        


    }
}
