using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore6.Core.Entities
{
    public class Artist
    {
        public Artist()
        {
            Covers = new List<Cover>();
        }
        public int ArtistId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        // Many to many
        public List<Cover> Covers { get; set; }
    }
}
