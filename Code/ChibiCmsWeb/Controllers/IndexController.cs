using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jushen.ChibiCms.ChibiContent;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace ChibiCmsWeb.Controllers
{
    public class IndexController: Controller
    {
        public IndexController(ContentManager contentManager, IConfiguration config)
        {
            ContentManager = contentManager;
            Config = config;
        }

        public ContentManager ContentManager { get; }
        public IConfiguration Config { get; }

        public IActionResult Index(int page=1,int pageSize=0,string path="",bool isRecursive=false,bool isIgnoreDirectory=false)
        {
            if (string.IsNullOrEmpty(path))
            {
                path = Config["StartPath"];
            }
            if (path == Config["RootPath"])
            {
                path = "";
            }
            var metas = ContentManager.GetContentMeta(path, isRecursive, isIgnoreDirectory, page, pageSize);
            ViewData["Title"] = metas.rootMeta.Title;
            return View("index",metas.metas);
        }
    }
}