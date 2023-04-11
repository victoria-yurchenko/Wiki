using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Wiki.Data;
using Wiki.Models;
using Wiki.Models.Helpers;

namespace Wiki.Controllers
{
    public class ArticlesController : Controller
    {
        private readonly WikiDb _context;
        private static List<Paragraph> _currentArticleParagraphs = new List<Paragraph>();
        private string _currentArticleTitle;
        private string _currentArticleDescription;

        public ArticlesController(WikiDb context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return _context.Article != null ?
                        View(await _context.Article.ToListAsync()) :
                        Problem("Entity set 'WikiDb.Article'  is null.");
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Article == null)
                return NotFound();

            var article = await _context.Article
                                        .FirstOrDefaultAsync(m => m.Id == id);
            if (article == null)
                return NotFound();

            return View(GetArticleViewModel(article));
        }

        [HttpGet]
        public IActionResult Create()
        {
            var list = new List<string>();

            for (int i = 0; i < _currentArticleParagraphs.Count; i++)
            {
                var p = _currentArticleParagraphs[i].Text;
                list.Add(p);
            }

            ViewBag.Par = list;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,ShortDescription")] Article article)
        {
            _currentArticleParagraphs = new List<Paragraph>();

            if (ModelState.IsValid)
            {
                _context.Add(article);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(article);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Article == null)
                return NotFound();

            // getting the paragraphs for representation 
            var article = await _context.Article.FindAsync(id);
            ViewBag.Paragraphs = GetParagraphs(id);

            if (article == null)
                return NotFound();

            return View(article);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,ShortDescription")] Article article)
        {
            if (id != article.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    _context.Update(article);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArticleExists(article.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(article);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Article == null)
            {
                return NotFound();
            }

            var article = await _context.Article
                .FirstOrDefaultAsync(m => m.Id == id);
            if (article == null)
            {
                return NotFound();
            }

            return View(article);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Article == null)
            {
                return Problem("Entity set 'WikiDb.Article'  is null.");
            }
            var article = await _context.Article.FindAsync(id);
            if (article != null)
            {
                _context.Article.Remove(article);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArticleExists(int id)
        {
            return (_context.Article?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        [HttpPost]
        public IActionResult AddParagraph(string Text)
        {
            if (_currentArticleParagraphs == null)
                _currentArticleParagraphs = new List<Paragraph>();

            var paragraph = new Paragraph()
            {
                Text = Text
            };

            // add the current paragraph to the sequence
            _currentArticleParagraphs.Add(paragraph);

            // reload this page with added element
            return RedirectToAction(nameof(Create));
        }

        [HttpGet]
        public IActionResult AddArticle(string Title, string ShortDescription)
        {
            // new article is generated here
            var article = new Article()
            {
                Title = Title,
                ShortDescription = ShortDescription
            };

            _context.Article.Add(article);

            // export data after every Add to create the autoincrease Id
            _context.SaveChanges();


            foreach (var p in _currentArticleParagraphs)
            {
                // paragraph to export to database
                var paraghraph = new Paragraph()
                {
                    Text = p.Text
                };
                
                _context.Paragraph.Add(paraghraph);
                _context.SaveChanges();

                // articleParagraph to export to database
                var articleParagraph = new ArticleParagraph()
                {
                    ParagraphId = paraghraph.Id,
                    ArticleId = article.Id
                };

                _context.ArticleParagraph.Add(articleParagraph);
                _context.SaveChanges();
            }

            // make the list of paragraphs empty
            _currentArticleParagraphs = new List<Paragraph>();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult SearchByTitle(string Search)
        {
            try
            {
                if (string.IsNullOrEmpty(Search))
                    return View(nameof(Index), _context.Article.ToList());

                var articles = _context.Article
                                        .Where(a => a.Title.Contains(Search) || a.ShortDescription.Contains(Search))
                                        .ToList();

                return View(nameof(Index), articles);
            }
            catch
            {
                return View(nameof(Index), new List<Article>());
            }
        }

        [HttpGet]
        public IActionResult ClearSearchBar(string Search)
        {
            return View(nameof(Index), _context.Article.ToList());
        }


        // private methods
        private ArticleViewModel GetArticleViewModel(Article article)
        {
            return new ArticleViewModel() { Article = article, Paragraphs = GetParagraphs(article.Id) };
        }

        private List<Paragraph> GetParagraphs(int? id)
        {
            var articleParagraphs = _context.ArticleParagraph
                                            .Where(ap => ap.ArticleId == id)
                                            .ToList();

            var list = new List<Paragraph>();

            if (articleParagraphs != null)
                foreach (var ap in articleParagraphs)
                    list.Add(_context.Paragraph.Find(ap.ParagraphId));

            return list;
        }
    }
}
