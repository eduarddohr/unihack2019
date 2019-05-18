using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace unihackAPI.Models
{
    public class ManagerModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int ZoneId { get; set; }
        public string ZoneName { get; set; }
        public List<CollectorModel> Collectors { get; set; }

    }
}