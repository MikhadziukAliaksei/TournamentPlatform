using System.Collections.Generic;

namespace TournamentPlatform.BLL.Models
{
    public class BracketData
    {
        public IEnumerable<string[]> teams { get; set; }
        public IEnumerable<IEnumerable<IEnumerable<int?[]>>> results { get; set; }
    }
}