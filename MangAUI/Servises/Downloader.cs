using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using HtmlAgilityPack;
using MangAUI.Models;
using static System.Console;

namespace MangAUI.Servises
{
    class Downloader
    {
        //private string FolderUrl = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Manga\\";
        private string FolderUrl = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\Manga\\";


        protected string URL;
        public Downloader(string url)
        {
            URL = url.Replace("http:", "https:");
        }

        public IEnumerable<string> GetImages()
        {
            var web = new HtmlWeb();
            var document = web.LoadFromWebAsync(URL).Result;
            yield return "";
        }

        public IEnumerable<ChapterCard> GetChaptersList()
        {

            var web = new HtmlWeb();
            var document = web.LoadFromWebAsync(URL).Result;

            foreach(var chapter in document.DocumentNode.Descendants("div")
                .Where(x => x.Attributes["class"]?.Value == "chapterlist")
                .FirstOrDefault()
                .Descendants("tr")
                .Skip(1)
                .Select(x => x.Descendants("a").FirstOrDefault())) 
            {
                var chapterCard = new ChapterCard();
                chapterCard.url = chapter.Attributes["href"].Value;
                chapterCard.name = chapter.InnerText;
                yield return chapterCard;
            }
        }

        public IEnumerable<MangaCard> GetMainMangaList() 
        {

            var web = new HtmlWeb();
            var document = web.LoadFromWebAsync(URL).Result;

            foreach(var dl in document.DocumentNode.Descendants("dl")) 
            {
                var card = new MangaCard();
                card.image = dl.Descendants("dd")?
                    .FirstOrDefault()?
                    .Descendants("a")?
                    .FirstOrDefault()?
                    .Descendants("img")?
                    .FirstOrDefault()?
                    .Attributes["src"]?.Value;
                card.title = dl.Descendants("dt")?
                    .FirstOrDefault()?
                    .Descendants("a")?
                    .FirstOrDefault()?.InnerText.Replace('\n', ' ');
                card.url = dl.Descendants("dt")?
                    .FirstOrDefault()?
                    .Descendants("a")?
                    .FirstOrDefault()?
                    .Attributes["href"]?.Value;
                yield return card;
            }
        }

        public async Task<MangaData> GetChapters()
        {
            MangaData data = new MangaData();
            data.Chapters = new List<ChapterData>();
            data.Url = URL;

            var web = new HtmlWeb();
            var document = await web.LoadFromWebAsync(URL);
            data.Name = document.DocumentNode.Descendants("div")
                .Where(x => x.Attributes["class"].Value == "bookmessagebox")
                .FirstOrDefault()
                .Descendants("h1")
                .FirstOrDefault()
                .InnerText;
            var container = document.DocumentNode.Descendants("div")
                .Where(x => x.Attributes["class"].Value == "chapterlist")
                .FirstOrDefault()
                .Descendants("tr")
                .Skip(1)
                .ToList();
            foreach(var item in container)
            {
                try
                {
                    var tds = item.Descendants("td");
                    var a = tds.FirstOrDefault().Descendants("a").FirstOrDefault();
                    var name = a.InnerText;
                    var url = a.Attributes["href"].Value;
                    if (url.EndsWith("/"))
                        url = url.Remove(url.Length - 1);
                    ChapterData chapter = new ChapterData();
                    chapter.Name = name;
                    chapter.Url = url;
                    //var webC = new HtmlWeb();
                    //var documentC = await web.LoadFromWebAsync(url);
                    //int n = 0;
                    //int.TryParse(documentC.DocumentNode.Descendants("select")
                    //    .Where(x => x.Attributes["id"]?.Value == "page")
                    //    .FirstOrDefault()
                    //    .Descendants("option")
                    //    .Last()
                    //    .InnerText, out n);
                    //chapter.Count = n;
                    data.Chapters.Add(chapter);
                }
                catch (Exception ex)
                {
                    data.Chapters.Add(null);
                }
            }

            return data;

        }
    }
}
