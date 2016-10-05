using AutoMapper;
using ProjectCore;

namespace Plugin2
{
  public class DomainProfile2 : Profile
  {
    public DomainProfile2()
    {
      this.CreateMap<DomainType2, IDto>().IncludeBase<IDomainType, IDto>().ConstructUsing(f => new Dto2()).As<Dto2>();
      this.CreateMap<DomainType2, Dto2>().ForMember(m => m.P2, a => a.MapFrom(x => x.Prop2)).IncludeBase<DomainType2, IDto>();

      this.CreateMap<Dto2, IDomainType>().IncludeBase<IDto, IDomainType>().ConstructUsing(dto => new DomainType2()).As<DomainType2>();
      this.CreateMap<Dto2, DomainType2>().ForMember(m => m.Prop2, a => a.MapFrom(x => x.P2)).IncludeBase<Dto2, IDomainType>();
    }
  }
}