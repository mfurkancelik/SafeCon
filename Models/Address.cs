using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SafeCon.Models
{
    public class Address : BaseEntity
    {
        public string Line { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
        
        public Guid UserId { get; set; }

        public override string ToString()
        {
            return Line +"-" + City +"-" + ZipCode + "-" + Country;
        }
    }
}
