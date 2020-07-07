using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DatabaseModel.Models
{
    public class PolicyDetail
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int Id { get; set; }
        public string Name { get; set; }
        public int PolicyId { get; set; }
        public Policy Policy { get; set; }
    }
}
