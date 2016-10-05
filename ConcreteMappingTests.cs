using AutoMapper;
using Shouldly;
using Xunit;

namespace Tests
{
  [Trait("Category", "UnitTest")]
  public class ConcreteMappingTests
  {
    private readonly IMapper mapper;


    public ConcreteMappingTests()
    {
      this.mapper = new MapperConfiguration(cfg =>
                                            {
                                              cfg.AddProfile<DomainProfile1>();
                                              cfg.AddProfile<DomainProfile2>();
                                            }).CreateMapper();
    }


    [Fact]
    public void MapsDomain1ToDto1()
    {
      var from = new DomainType1();
      var to = this.mapper.Map<Dto1>(from);
      to.ShouldBeOfType<Dto1>();
    }


    [Fact]
    public void MapsDomain2ToDto2()
    {
      var from = new DomainType2();
      var to = this.mapper.Map<Dto2>(from);
      to.ShouldBeOfType<Dto2>();
    }


    [Fact]
    public void MapsDto1ToDomain1()
    {
      var from = new Dto1();
      var to = this.mapper.Map<DomainType1>(from);
      to.ShouldBeOfType<DomainType1>();
    }


    [Fact]
    public void MapsDto2ToDomain2()
    {
      var from = new Dto2();
      var to = this.mapper.Map<DomainType2>(from);
      to.ShouldBeOfType<DomainType2>();
    }
  }
}
