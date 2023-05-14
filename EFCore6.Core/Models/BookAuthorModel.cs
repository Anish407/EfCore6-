using EFCore6.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore6.Core.Models
{
    public class BookAuthorModel
    {
        public Author Author { get; set; }
        public Book Book { get; set; }
    }
}
