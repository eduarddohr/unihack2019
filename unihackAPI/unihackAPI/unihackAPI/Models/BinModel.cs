using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace unihackAPI.Models
{
    public class BinModel
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Type { get; set; }
        [Required]
        public float Latitude { get; set; }
        [Required]
        public float Longitude { get; set; }
        [Required]
        public float Capacity { get; set; }
        [Required]
        public int Zone { get; set; }
        public string ZoneName { get; set; }
        public List<CollectorModel> Collectors { get; set; }
        public string ManagerId { get; set; }
        public string ManagerName { get; set; }
    }
}