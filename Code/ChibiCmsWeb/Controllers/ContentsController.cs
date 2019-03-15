using System;
using System.Collections.Generic;
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
            var content = ContentManager.GetConent(@"\"+path);
            var template= string.IsNullOrEmpty(content.Meta.Template)?"defaultContentView":content.Meta.Template;
            return View(template, content);
        }
    }
}