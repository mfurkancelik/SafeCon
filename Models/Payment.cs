using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SafeCon.Models
{
    public class Payment : BaseEntity
    {
        public string CardName { get; set; }
        public string CardNumber { get; set; }
        public string Skt { get; set; }
        public string Cvv { get; set; }




        [ForeignKey("UserId")]
        public User User { get; set; }

        public Guid UserId { get; set; }
    }
}