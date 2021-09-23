using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SafeCon.Models
{
    public class Vehicle : BaseEntity
    {
        public string InsurancePolicyNumber { get; set; }

        public string VehicleName { get; set; }

        public string TrailerType { get; set; }

        public double VehicleCapacity { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        public Guid UserId { get; set; }
    }
}
