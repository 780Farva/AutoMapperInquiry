using System.Linq;
using AutoMapper;

namespace ProjectCore
{
  public class CollectionMappingProfile : Profile
  {
    public CollectionMappingProfile()
    {
      this.CreateMap<DtoCollection, DomainCollection>().
           ForMember(fc => fc.Entries, opt => opt.Ignore()).
           AfterMap((tc, fc, ctx) => fc.Entries = tc.Entries.Select(e => ctx.Mapper.Map<IDomainType>(e)).ToArray());

      this.CreateMap<DomainCollection, DtoCollection>().
           AfterMap((fc, tc, ctx) =>
                    {
                      foreach (var t in fc.Entries.Select(e => ctx.Mapper.Map<IDto>(e))) tc.Add(t);
                    });
    }
  }
}