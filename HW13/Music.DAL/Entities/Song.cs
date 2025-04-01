using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.DAL.Entities
{
    public class Song
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public Genre? Genre { get; set; }
        public int? AuthorId { get; set; }
        public Author? Author { get; set; }
    }

}
