using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatabaseModel.Models
{
    public class Request
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Note { get; set; }
        public int PersonId { get; set; }
        public int CurrentSigningLevel { get; set; }
        public SigningStatus Status { get; set; }
        public int FinalSigningPersonId { get; set; }
        public Person Person { get; set; }
        public string History { get; set; }
    }
    public enum SigningStatus
    {
        Sending,
        Approve,
        Rject
    }
}
