using Rayify.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rayify.Domain.Entities
{
    public class Singer : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Music> Musics { get; set; }
        public ICollection<Album> Albums { get; set; }
    }
}
