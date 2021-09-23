using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SafeCon.Models
{
    public class Offer : BaseEntity
    {
        [ForeignKey("RequestId")]
        public Request Request { get; set; }

        public Guid RequestId { get; set; }

        [ForeignKey("VehicleId")]
        public Vehicle Vehicle { get; set; }

        public Guid VehicleId { get; set; }

        public double Price { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        public Guid UserId { get; set; }

    }
}
