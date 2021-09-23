using System;

namespace SafeCon.Models
{
    public class BaseEntity : IEntity
    {
        public BaseEntity()
        {
            this.Id = Guid.NewGuid();
            this.CreatedDate = DateTime.Now;
            this.CreatedComputerName = Environment.MachineName;
            this.CreatedBy = Guid.NewGuid();
        }

        public Guid Id { get; set; }
        public string CreatedComputerName { get; set; }        
        public DateTime CreatedDate { get; set; }
        public Guid CreatedBy { get; set; }

        #region Düzenleme
        public string ModifiedComputerName { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public Guid? ModifiedBy { get; set; }
        #endregion


    }
}
