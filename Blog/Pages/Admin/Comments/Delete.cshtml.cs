using System.Threading.Tasks;
using Blog.Models.PageContent.Comment;
using Blog.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Blog.Pages.Admin.Comments
{
    public class DeleteModel : PageModel
    {
        private readonly IArticleService _articleService;

        public DeleteModel(IArticleService articleService)
        {
            _articleService = articleService;
        }

        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        //[BindProperty]
        public DisplayCommentModel Model { get; set; }

        public void OnGet()
        {
            var comment = _articleService.GetCommentById(Id);

            Model = new DisplayCommentModel
            {
                Content = comment.Content,
                DatePublished = comment.DatePublished,
                Author = comment.Author
            };
        }
        
        public async Task<IActionResult> OnPost(string returnUrl = null)
        {
            await _articleService.DeleteComment(Id);

            return RedirectToPage("/Admin/Comments/Index");
        }
    }
}