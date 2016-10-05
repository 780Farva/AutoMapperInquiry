using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Shouldly;
using Xunit;

namespace Tests
{
  [Trait("Category", "UnitTest")]
  public class MappingInquiry
  {
    private readonly IMapper mapper;


    public MappingInquiry()
    {
      this.mapper = new MapperConfiguration(cfg =>
                                            {
                                              cfg.AddProfile<CollectionMappingProfile>();
                                              cfg.AddProfile<DomainProfile1>();
                                              cfg.AddProfile<DomainProfile2>();
                                            }).CreateMapper();
    }


    [Fact]
    public void ConfigurationIsValid()
    {
      this.mapper.ConfigurationProvider.AssertConfigurationIsValid();
    }


    [Fact]
    public void MapsCollectionsFromDomainToDto()
    {
      var domainContainer = new DomainCollection();
      domainContainer.Entries = new IDomainType[]
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

      var dtoCollection = this.mapper.Map<DtoCollection>(domainContainer);
      dtoCollection.Entries.Count().ShouldBe(domainContainer.Entries.Count());

      dtoCollection.Entries.OfType<Dto2>().Count().ShouldBe(1);
      dtoCollection.Entries.OfType<Dto1>().Count().ShouldBe(1);
    }


    [Fact]
    public void MapsCollectionsFromDtoToDomain()
    {
      var dtoCollection = new DtoCollection();
      var dtos = new IDto[]
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
      foreach (var to in dtos)
      {
        dtoCollection.Add(to);
      }

      var domainCollection = this.mapper.Map<DomainCollection>(dtoCollection);
      domainCollection.Entries.Count().ShouldBe(dtoCollection.Entries.Count());

      domainCollection.Entries.OfType<DomainType2>().Count().ShouldBe(1);
      domainCollection.Entries.OfType<DomainType2>().Count().ShouldBe(1);
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


    [Fact]
    public void MapsFromDomain1ToDto1UsingInterfaces()
    {
      this.testInheritanceTypeMapping<IDomainType, DomainType1, IDto, Dto1>();
    }


    [Fact]
    public void MapsFromDomain2ToDto2UsingInterfaces()
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
