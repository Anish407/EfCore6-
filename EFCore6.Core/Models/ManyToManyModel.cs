using EFCore6.Core.Entities;

namespace EFCore6.Core.Models
{
    public class ManyToManyModel
    {
        public Artist Artist { get; set; }
        public Cover Cover { get; set; }
    }
}
