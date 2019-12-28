using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using vega.Controllers.Resources;
using vega.Models;

namespace vega.Mapping
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            //Domain to API Resource
            CreateMap<Photo, PhotoResource>();
            CreateMap(typeof(QueryResult<>), typeof(QueryResultResource<>));
            CreateMap<VehicleQueryResource, VehicleQuery>();
            CreateMap<Make, MakeResource>();
            CreateMap<Model, ModelResource>();
            CreateMap<Model, KeyValuePairResource>();
            CreateMap<ModelFeatures, KeyValuePairResource>();
            CreateMap<Feature, KeyValuePairResource>();
            CreateMap<Make, KeyValuePairResource>();

            CreateMap<VehicleFeatures, KeyValuePairResource>()
                .ForMember(kvp => kvp.Id, opt => opt.MapFrom(vf => vf.Feature.Id))
                .ForMember(kvp => kvp.Name, opt => opt.MapFrom(vf => vf.Feature.Name));

            CreateMap<Vehicle, SaveVehicleResource>()
                .ForMember(vr => vr.Contact, opt => opt.MapFrom(v => new ContactResource { Name = v.ContactName, Email = v.ContactEmail, Phone = v.ContactPhone }))
                .ForMember(vr => vr.Make, opt => opt.MapFrom(v => v.Model.Make))
                .ForMember(vr => vr.VehicleFeatures, opt => opt.MapFrom(v => v.VehicleFeatures.Select(vf=>vf.FeatureId)));

            CreateMap<Vehicle, VehicleResource>()
                .ForMember(vr => vr.Make, opt => opt.MapFrom(v => v.Model.Make))
                .ForMember(vr => vr.Contact, opt => opt.MapFrom(v => new ContactResource { Name = v.ContactName, Email = v.ContactEmail, Phone = v.ContactPhone }));
       
            //API Resource to Domain    
            CreateMap<SaveVehicleResource, Vehicle>()
                .ForMember(v => v.ContactName, opt => opt.MapFrom(vr => vr.Contact.Name))
                .ForMember(v => v.ContactEmail, opt => opt.MapFrom(vr => vr.Contact.Email))
                .ForMember(v => v.ContactPhone, opt => opt.MapFrom(vr => vr.Contact.Phone))
                .ForMember(v => v.VehicleFeatures, opt => opt.MapFrom(vr => vr.VehicleFeatures.Select(id => new VehicleFeatures { FeatureId = id })));

            CreateMap<Vehicle, VehicleResourceList>()
                 .ForMember(vr => vr.Contact, opt => opt.MapFrom(v => new ContactResource { Name = v.ContactName, Email = v.ContactEmail, Phone = v.ContactPhone }));

        }
    }
}
