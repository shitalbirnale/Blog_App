
using BlogApplication.Constants;
using BlogApplication.Data;
using BlogApplication.IBlogServices;
using BlogApplication.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Metrics;

namespace BlogApplication.Controllers
{
    [ExcludeFromCodeCoverage]
    public class BlogsController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IBlog _blogService;

        public BlogsController(ApplicationDbContext _context, IBlog blogService)
        {
            context = _context;
            _blogService = blogService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Blog BlogModel)
        {
            var status = _blogService.CreateBlog(BlogModel);
            return RedirectToAction(BlogConstant.LIST);
        }
        [HttpGet]
        public IActionResult List()
        {
            var List = _blogService.BlogList();
            return View(List);
        }
        [HttpGet]
        public IActionResult Edit(int blogId)
        {
            var ids = TempData[BlogConstant.ID];
            Blog blogDetails = new Blog();
            if (blogId != 0)
            {
                var data = _blogService.Edit(blogId);
                blogDetails.Id = data.Id;
                blogDetails.Title = data.Title;
                blogDetails.AutherName = data.AutherName;
                blogDetails.PublicationDate = data.PublicationDate;
                blogDetails.Contents = data.Contents;
            }
            return View(blogDetails);
        }
        [HttpPost]
        public IActionResult Edit(Blog Model)
        {
            var isSuccessStatus = _blogService.Edit(Model);
            if (isSuccessStatus == BlogConstant.SUCCESSMESSAGE)
            {
                return RedirectToAction(BlogConstant.LIST);
            }
            return RedirectToAction(BlogConstant.LIST);
        }
        public IActionResult Delete(int Id)
        {
            if (Id != 0)
            {
                var status = _blogService.Delete(Id);
            }
            return RedirectToAction(BlogConstant.LIST);
        }
        public IActionResult View(int Id)
        {
            Blog modelData = new Blog();
            if (Id != 0)
            {
                var data = _blogService.Edit(Id);
                System.Text.RegularExpressions.Regex rx = new System.Text.RegularExpressions.Regex("<[^>]*>");
                modelData.Id = data.Id;
                modelData.Title = data.Title;
                modelData.AutherName = data.AutherName;
                modelData.PublicationDate = data.PublicationDate;
                modelData.Contents = rx.Replace(data.Contents, "");
            }
            ViewBag.ReadMore = modelData;
            return View(modelData);
        }
    }
}

