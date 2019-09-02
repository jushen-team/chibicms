using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Jushen.ChibiCms.ChibiContent;
using Microsoft.AspNetCore.Mvc;

namespace ChibiCmsWeb.Controllers
{
    public class ContentsController : Controller
    {
        public ContentsController(ContentManager contentManager)
        {
            ContentManager = contentManager;
        }

        public ContentManager ContentManager { get; }

        [HttpGet]
        public IActionResult GetOneContent(string path)
        {
            var content = ContentManager.GetConent(path);
            if (content.Meta.ContentType == ContentMeta.TypeLink)
            {
                return Redirect(content.Meta.Link);
            }

            ViewData["baseUrl"]  = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.Path}/";
            var template= string.IsNullOrEmpty(content.Meta.Template)?"defaultContentView":content.Meta.Template;
            return View(template, content);
        }
    }
}