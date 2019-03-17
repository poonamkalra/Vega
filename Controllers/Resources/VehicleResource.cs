using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using vega.Models;
using System.Collections.ObjectModel;

namespace vega.Controllers.Resources
{
    public class Contact
    {
        [Required]
        [StringLength(255)]
        public string Name {get;set;}
        [Required]
        [StringLength(255)]
        public string Email {get;set;}
        [Required]
        [StringLength(255)]
        public string Phone {get;set;}
    }

    public class VehicleResource
    {
        public int Id { get; set; }
        public int ModelId { get; set; }       
        public bool IsRegistered { get; set; }

        [Required]
        public Contact Contact { get; set; }
        public ICollection<int> Features {get;set;}

        public VehicleResource()
        {
            Features  = new Collection<int>();  
        }
    }
}