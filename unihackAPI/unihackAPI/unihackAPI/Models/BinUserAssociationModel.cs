using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace unihackAPI.Models
{
    public class BinUserAssociationModel
    {
        public Guid BinId { get; set; }
        public string UserId { get; set; }
    }
}