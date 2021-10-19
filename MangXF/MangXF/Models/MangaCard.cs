using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangXF.Models
{
    public class MangaCard
    {
        public string url { get; set; }
        public string image { get; set; }
        public string title { get; set; }
        public override bool Equals(object obj)
        {
            if (obj is MangaCard)
                return url == ((MangaCard)obj).url;
            return false;
        }

    }
}
