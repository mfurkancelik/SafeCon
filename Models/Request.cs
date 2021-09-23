using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SafeCon.Models
{
    public class Request : BaseEntity
    {
        [ForeignKey("PickUpAddressId")]
        public Address PickUpAddress { get; set; }

        public Guid PickUpAddressId { get; set; }

        [ForeignKey("DestinationAddressId")]
        public Address DestinationAddress { get; set; }

        public Guid DestinationAddressId { get; set; }


        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public double  FreightAmount { get; set; }

        public string FreightType { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        public Guid UserId { get; set; }

    }
}
