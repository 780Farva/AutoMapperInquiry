using AutoMapper;
using Plugin1;
using Plugin2;
using ProjectCore;
using Shouldly;
using Xunit;

namespace Tests
{
  [Trait("Category", "UnitTest")]
  public class PolymorphicMappingTests
  {
    private readonly IMapper mapper;


    public PolymorphicMappingTests()
    {
      this.mapper = new MapperConfiguration(cfg =>
                                            {
                                              cfg.AddProfile<DomainProfile1>();
                                              cfg.AddProfile<DomainProfile2>();
                                            }).CreateMapper();
    }


    [Fact]
    public void MapsDto1ToDomain1UsingInterfaces()
    {
      this.testInheritanceTypeMapping<IDto, Dto1, IDomainType, DomainType1>();
    }


    [Fact]
    public void MapsDto2ToDomain2UsingInterfaces()
    {
      this.testInheritanceTypeMapping<IDto, Dto2, IDomainType, DomainType2>();
    }
    [Fact]
    public void MapsDomain1ToDto1UsingInterfaces()
    {
      this.testInheritanceTypeMapping<IDomainType, DomainType1, IDto, Dto1>();
    }


    [Fact]
    public void MapsDomain2ToDto2UsingInterfaces()
    {
      this.testInheritanceTypeMapping<IDomainType, DomainType2, IDto, Dto2>();
    }


    private void testInheritanceTypeMapping<TFromBase, TFrom, TToBase, TTo>() where TFrom : TFromBase, new()
        where TTo : TToBase, new()
    {
      TFromBase from = new TFrom();
      var to = this.mapper.Map<TToBase>(from);
      to.ShouldBeOfType<TTo>();
    }


  }
}