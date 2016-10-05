using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Plugin1;
using Plugin2;
using ProjectCore;
using Shouldly;
using Xunit;

namespace Tests
{
  [Trait("Category", "UnitTest")]
  public class EnumerableMappingTests
  {

    private readonly IMapper mapper;


    public EnumerableMappingTests()
    {
      this.mapper = new MapperConfiguration(cfg =>
                                            {
                                              cfg.AddProfile<DomainProfile1>();
                                              cfg.AddProfile<DomainProfile2>();
                                            }).CreateMapper();
    }

    [Fact]
    public void MapsEnumerableOfDomainObjectsUsingInterfaces()
    {
      IEnumerable<IDomainType> domainObjects = new IDomainType[]
                                               {
                                                   new DomainType1
                                                   {
                                                       Prop0 = 10,
                                                       Prop1 = 11
                                                   },
                                                   new DomainType2
                                                   {
                                                       Prop0 = 20,
                                                       Prop2 = 22
                                                   }
                                               };

      var dtos = this.mapper.Map<IEnumerable<IDto>>(domainObjects).ToArray();

      dtos.Length.ShouldBe(domainObjects.Count());

      var dto1 = dtos[0].ShouldBeOfType<Dto1>();
      dto1.P0.ShouldBe(10);
      dto1.P1.ShouldBe(11);

      var dto2 = dtos[1].ShouldBeOfType<Dto2>();
      dto2.P0.ShouldBe(20);
      dto2.P2.ShouldBe(22);
    }


    [Fact]
    public void MapsEnumerableOfDtosUsingInterfaces()
    {
      IEnumerable<IDto> dtos = new IDto[]
                               {
                                   new Dto1
                                   {
                                       P0 = 10,
                                       P1 = 11
                                   },
                                   new Dto2
                                   {
                                       P0 = 20,
                                       P2 = 22
                                   }
                               };

      var domainObjects = this.mapper.Map<IEnumerable<IDomainType>>(dtos).ToArray();

      domainObjects.Length.ShouldBe(dtos.Count());

      var domainObject1 = domainObjects[0].ShouldBeOfType<DomainType1>();
      domainObject1.Prop0.ShouldBe(10);
      domainObject1.Prop1.ShouldBe(11);

      var domainObject2 = domainObjects[1].ShouldBeOfType<DomainType2>();
      domainObject2.Prop0.ShouldBe(20);
      domainObject2.Prop0.ShouldBe(22);
    }
  }
}