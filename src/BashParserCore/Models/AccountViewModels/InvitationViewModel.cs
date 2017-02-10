using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BashParserCore.Models.AccountViewModels
{
    public class InvitationViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string SenderId { get; set; }
    }
}
