using AutoMapper;
using Vega.Core.Models;
using System.Linq;
using Vega.Controllers.Resources;


namespace Vega.Mappings
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<Make, MakeResource>();
            CreateMap<Make, KeyValuePairResource>();
            CreateMap<Model, KeyValuePairResource>();
            CreateMap<Vehicle, SaveVehicleResource>()
            .ForMember(vr => vr.Contact, opt => opt.MapFrom(v => new ContactResource{
                Name = v.ContactName,
                Email = v.ContactEmail,
                Phone  = v.ContactPhone
            }))
            .ForMember(vr => vr.Features, opt => opt.MapFrom(v => v.Features.Select(vf => vf.FeatureId)));
             
             CreateMap<Vehicle, VehicleResource>()
            .ForMember(vr => vr.Contact, opt => opt.MapFrom(v => new ContactResource{
                Name = v.ContactName,
                Email = v.ContactEmail,
                Phone  = v.ContactPhone
            }))
            .ForMember(vr => vr.Features, opt => opt.MapFrom(v => v.Features.Select(vf => new KeyValuePairResource{Id=vf.Feature.Id, Name=vf.Feature.Name })))
            .ForMember(vr => vr.Make, opt => opt.MapFrom(v => v.Model.Make));

            CreateMap<SaveVehicleResource, Vehicle>()
            .ForMember(v => v.Id, opt => opt.Ignore())
            .ForMember(v => v.ContactName, opt => opt.MapFrom(vr => vr.Contact.Name))           
            .ForMember(v => v.ContactEmail, opt => opt.MapFrom(vr => vr.Contact.Email))          
            .ForMember(v => v.ContactPhone, opt => opt.MapFrom(vr => vr.Contact.Phone))     
            .ForMember(v => v.Features, opt => opt.Ignore())   
            //.ForMember(v => v.Features, opt => opt.MapFrom(vr => vr.Features.Select(id => new VehicleFeature{FeatureId = id})));
            .AfterMap((vr, v) => {
                    var removedFeatures = v.Features.Where(f => !vr.Features.Contains(f.FeatureId)).ToList();

                    foreach(var r in removedFeatures)
                        v.Features.Remove(r);

                    var addedFeatures = vr.Features.Where(id => !v.Features.Any(f => f.FeatureId == id)).Select(id => new VehicleFeature{FeatureId = id}); 
                     foreach(var r in addedFeatures)
                        v.Features.Add(r); 
                });
        }
    }
}