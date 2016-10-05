using System.Linq;
using AutoMapper;
using Shouldly;
using Xunit;

namespace Tests
{
  [Trait("Category", "UnitTest")]
  public class CollectionMappingTests
  {
    private readonly IMapper mapper;


    public CollectionMappingTests()
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
      
      var dto1 = dtoCollection.Entries.ElementAt(0).ShouldBeOfType<Dto1>();
      dto1.P0.ShouldBe(10);
      dto1.P1.ShouldBe(11);

      var dto2 = dtoCollection.Entries.ElementAt(1).ShouldBeOfType<Dto2>();
      dto2.P0.ShouldBe(20);
      dto2.P2.ShouldBe(22);
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

      var domain1 = domainCollection.Entries.ElementAt(0).ShouldBeOfType<DomainType1>();
      domain1.Prop0.ShouldBe(10);
      domain1.Prop1.ShouldBe(11);

      var domain2 = domainCollection.Entries.ElementAt(1).ShouldBeOfType<DomainType2>();
      domain2.Prop0.ShouldBe(20);
      domain2.Prop2.ShouldBe(22);
    }
  }
}
