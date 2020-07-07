using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DatabaseModel.Models
{
    public class Policy
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int Id { get; set; }
        public string Name { get; set; }
        public List<PolicyDetail> Policys { get; } = new List<PolicyDetail>();
    }
}
