using AutoMapper;
using ProjectCore;

namespace Plugin2
{
  public class DomainProfile2 : Profile
  {
    public DomainProfile2()
    {
      this.CreateMap<DomainType2, IDto>().ConstructUsing(f => new Dto2()).As<Dto2>();

      this.CreateMap<DomainType2, Dto2>().ForMember(m => m.P2, a => a.MapFrom(x => x.Prop2))
          .IncludeBase<IDomainType, IDto>().ReverseMap();


      this.CreateMap<Dto2, IDomainType>().ConstructUsing(dto => new DomainType2()).As<DomainType2>();
    }
  }
}