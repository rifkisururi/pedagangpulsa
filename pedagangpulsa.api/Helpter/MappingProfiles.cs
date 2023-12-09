using AutoMapper;
using pedagangpulsa.api.Domain.Entities;
using pedagangpulsa.api.DTO.Request;

namespace pedagangpulsa.api.Helpter
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles() {
            // Entities -> DTO 
            
            // DTO -> Entities
            CreateMap<RegisterDTO, konter>();
        }
    }
}
