using ELP.Model.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ELP.Model.Entities
{
    public class Event : AuditableEntity<long>
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
    }
}
