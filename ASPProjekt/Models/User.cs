using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASPProjekt.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [InverseProperty("Organizer")]
        public List<Event> HostedEvents { get; set; }
        [InverseProperty("Attendees")]
        public List<Event> JoinedEvents { get; set; }
    }
}
