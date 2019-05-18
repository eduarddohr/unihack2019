using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace Unihack.Web.Models
{
   public class BinViewModel
    {
        public string Name { get; set; }

        public string Picture{ get; set; }

        public float Capacity { get; set; }

        public string Manager { get; set; }
        
        public List<string> Collectors { get; set; }

    }
}