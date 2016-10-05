using AutoMapper;

namespace Tests
{
  public class DomainProfile1 : Profile
  {
    public DomainProfile1()
    {
      this.CreateMap<DomainType1, Dto1>().ForMember(m => m.P1, a => a.MapFrom(x => x.Prop1))
          .IncludeBase<IDomainType, IDto>().ReverseMap();
    }
  }
}