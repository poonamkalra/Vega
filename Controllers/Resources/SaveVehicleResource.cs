using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using Vega.Core;
using System.Collections.ObjectModel;

namespace Vega.Controllers.Resources
{
    public class ContactResource
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

    public class SaveVehicleResource
    {
        public int Id { get; set; }
        public int ModelId { get; set; }       
        public bool IsRegistered { get; set; }

        [Required]
        public ContactResource Contact { get; set; }
        public ICollection<int> Features {get;set;}

        public SaveVehicleResource()
        {
            Features  = new Collection<int>();  
        }
    }
}