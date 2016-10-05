using AutoMapper;

namespace ProjectCore
{
  public class DomainDtoInterfaceProfile : Profile
  {
    public DomainDtoInterfaceProfile()
    {
      this.CreateMap<IDomainType, IDto>().ForMember(m => m.P0, a => a.MapFrom(x => x.Prop0));
      this.CreateMap<IDto, IDomainType>().ForMember(m => m.Prop0, a => a.MapFrom(x => x.P0));
    }
  }
}