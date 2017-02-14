using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BashParserCore.Models
{
    public class InvitationModel
    {
        [Key]
        public Guid InvitationId { get; set; }
        public DateTime SendingTime { get; set; }
        public string Email { get; set; }
        public string SenderId { get; set; }
        public ApplicationUser Sender { get; set; }
    }
}
