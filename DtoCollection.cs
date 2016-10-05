using System.Collections.Generic;

namespace Tests
{
  public class DtoCollection
  {
    private readonly IList<IDto> entries = new List<IDto>();
    public IEnumerable<IDto> Entries => this.entries;
    public void Add(IDto entry) { this.entries.Add(entry); }
  }
}