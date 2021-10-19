using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using MangXF.Models;
using Newtonsoft.Json;
using SkiaSharp;
using Xamarin.Forms;

namespace MangXF.Servises
{
    class Downloader
    {
        private string FolderUrl = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Manga\\";
        //private string FolderUrl = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\Manga\\";


        protected string URL;
        public Downloader(string url)
        {
            URL = url.Replace("http:", "https:");
        }

        public static void AddLastManga(MangaCard c)
        {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Manga\\last";
            if (File.Exists(path))
            {
                var ml = JsonConvert.DeserializeObject<List<MangaCard>>(File.ReadAllText(path));
                if(ml.Contains(c))
                    ml.Remove(c);
                ml.Insert(0, c);
                File.WriteAllText(path, JsonConvert.SerializeObject(ml));
            }
            else
            {
                File.WriteAllText(path, JsonConvert.SerializeObject(new List<MangaCard>() { c }));
            }
        }

        public IEnumerable<ImageModel> GetImages()
        {
            List<ImageModel> images = new List<ImageModel>();
            if (URL.EndsWith("/"))
                URL = URL.Substring(0, URL.Length - 1);

            string mangaid = URL.Split('/')[4];
            string charpterid = URL.Split('/')[5];
            if (File.Exists(FolderUrl + mangaid + charpterid))
            {
                string jsontext = File.ReadAllText(FolderUrl + mangaid + charpterid);
                foreach (var c in JsonConvert.DeserializeObject<List<ImageModel>>(jsontext))
                    yield return c;
            }
            else
            {
                int i = 0;
                while (true)
                {
                    i++;
                    var web = new HtmlWeb();
                    HtmlDocument document = null;
                    try
                    {
                        document = web.LoadFromWebAsync($"{URL}-{i}.html").Result;
                    }
                    catch (Exception)
                    {
                        break;
                    }
                    var img = document.DocumentNode.Descendants("img").Where(x => x.Attributes["id"]?.Value == "comicpic")?.FirstOrDefault();
                    if (img == null)
                        break;
                    else if (img.Attributes["src"]?.Value == null)
                        break;
                    else
                    {
                        string c = img.Attributes["src"].Value;
                        WebClient client = new WebClient();
                        Stream stream = client.OpenRead(c);
                        var bitmap = SKBitmap.Decode(stream);
                        if (bitmap != null)
                        {
                            var im = new ImageModel((float)Application.Current.MainPage.Width, bitmap.Height * ((float)Application.Current.MainPage.Width / bitmap.Width), c);
                            images.Add(im);
                            yield return im;
                        }
                        //imgs.Add(new ImageModel());
                        else
                        {
                            var im = new ImageModel(true);
                            images.Add(im);
                            yield return im;
                        }

                    }
                }
                File.WriteAllText(FolderUrl + mangaid + charpterid, JsonConvert.SerializeObject(images));
            }
        }

        public IEnumerable<ChapterCard> GetChaptersList()
        {

            var web = new HtmlWeb();
            var document = web.LoadFromWebAsync(URL).Result;

            foreach (var chapter in document.DocumentNode.Descendants("div")
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

            foreach (var dl in document.DocumentNode.Descendants("dl"))
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
        public List<MangaCard> GetLastMangaList()
        {
            if (File.Exists(FolderUrl + "last"))
            {
                var mangas = JsonConvert.DeserializeObject<List<MangaCard>>(File.ReadAllText(FolderUrl + "last"));
                return mangas;
            }
            else
            {
                return new List<MangaCard>();
            }
        }
        public IEnumerable<MangaCard> FindMangaList(string findString)
        {
            var web = new HtmlWeb();
            var document = web.LoadFromWebAsync($"http://www.mangarussia.com/search/?name_sel=contain&wd={findString}&author_sel=contain&author=&category_id=&out_category_id=&completed_series=either&type=").Result;

            foreach (var div in document.DocumentNode.Descendants("table").Skip(1).FirstOrDefault().Descendants("div"))
            {
                var card = new MangaCard();
                card.image = div.Descendants("img").FirstOrDefault().Attributes["src"]?.Value;
                card.title = div.Descendants("a").Skip(1).FirstOrDefault().InnerText.Replace("<br>", "");
                card.url = div.Descendants("a").FirstOrDefault().Attributes["href"]?.Value;
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
            foreach (var item in container)
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
