using System.Collections.Generic;

namespace ProjectCore
{
  public class DomainCollection
  {
    public IEnumerable<IDomainType> Entries { get; set; }
  }
}