using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangAUI.Models
{
    public record MangaData
    {
        public string Name {  get; set; }
        public string Url {  get; set; }
        public List<ChapterData> Chapters { get; set; }
    }
}
