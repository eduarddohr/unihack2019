using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Unihack.Web.Models
{
    public class UserModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Role { get; set; }

    }
}