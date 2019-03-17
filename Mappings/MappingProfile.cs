using AutoMapper;
using vega.Models;
using System.Linq;
using vega.Controllers.Resources;
namespace vega.Mappings
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<Make, MakeResource>();
            CreateMap<Model, ModelResource>();
            CreateMap<Vehicle, VehicleResource>()
            .ForMember(vr => vr.Contact, opt => opt.MapFrom(v => new Contact{
                Name = v.ContactName,
                Email = v.ContactEmail,
                Phone  = v.ContactPhone
            }))
            .ForMember(vr => vr.Features, opt => opt.MapFrom(v => v.Features.Select(vf => vf.FeatureId)));

            CreateMap<VehicleResource, Vehicle>()
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