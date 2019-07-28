using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jushen.ChibiCms.ChibiContent;
using Microsoft.AspNetCore.Mvc;

namespace ChibiCmsWeb.Controllers
{
    public class IndexController: Controller
    {
        public IndexController(ContentManager contentManager)
        {
            ContentManager = contentManager;
        }

        public ContentManager ContentManager { get; }

        public IActionResult Index(int page=1,int pageSize=5,string path="",bool isRecursive=false,bool isIgnoreDirectory=false)
        {
            var metas = ContentManager.GetContentMeta(path, isRecursive, isIgnoreDirectory, page, pageSize);
            ViewData["Title"] = metas.rootMeta.Title;
            return View("index",metas.metas);
        }
    }
}