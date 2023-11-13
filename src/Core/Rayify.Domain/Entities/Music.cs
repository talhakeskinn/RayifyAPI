using Rayify.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rayify.Domain.Entities
{
    public class Music : BaseEntity
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public string Language { get; set; }
        public string Path { get; set; }
        public DateTime Published { get; set; }
        public ICollection<Tag> Tags { get; set; }
        public Genre Genre { get; set; }
        public Album Album { get; set; }
        public ICollection<Singer> Singers { get; set; }

    }
}
