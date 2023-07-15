using System;
using System.Collections.Generic;
using System.Text;

namespace EMS.Domain.Entities
{
    public class BaseEntity
    {
       
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; private set; }

        public BaseEntity()
        {
            this.CreatedDate = DateTime.Now;
            this.ModifiedDate = DateTime.Now;
        }
    }
}
