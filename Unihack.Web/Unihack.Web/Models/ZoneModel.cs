using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace Unihack.Web.Models
{
   public class ZoneModel
    {
        public int ZoneId { get; set; }

        public string Name{ get; set; }

    }
}