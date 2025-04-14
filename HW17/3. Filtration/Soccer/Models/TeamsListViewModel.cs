using Microsoft.AspNetCore.Mvc.Rendering;

namespace Soccer.Models
{
    public class TeamsListViewModel
    {
        public IEnumerable<Teams> Teams { get; set; } = new List<Teams>();
        public string? Coach { get; set; }
    }
}
