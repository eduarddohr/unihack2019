using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace Unihack.Web.Models
{
   public class ManagerModel
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }

        public string ZoneName { get; set; }

    }
}