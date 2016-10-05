using AutoMapper;
using ProjectCore;

namespace Plugin1
{
  public class DomainProfile1 : Profile
  {
    public DomainProfile1()
    {
      this.CreateMap<DomainType1, IDto>().IncludeBase<IDomainType, IDto>().ConstructUsing(f => new Dto1()).As<Dto1>();
      this.CreateMap<DomainType1, Dto1>().ForMember(m => m.P1, a => a.MapFrom(x => x.Prop1))
          .IncludeBase<DomainType1, IDto>();

      this.CreateMap<Dto1, IDomainType>().IncludeBase<IDto, IDomainType>().ConstructUsing(dto => new DomainType1())
          .As<DomainType1>();
      this.CreateMap<Dto1, DomainType1>().ForMember(m => m.Prop1, a => a.MapFrom(x => x.P1))
          .IncludeBase<Dto1, IDomainType>();
    }
  }
}
