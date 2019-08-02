using Jushen.ChibiCms.ChibiContent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChibiCmsWeb.Helpers
{
    public class ContentHtmlHelper
    {
        public static string GetLinkToContent(ContentMeta meta)
        {
            switch (meta.ContentType)
            {
                case ContentMeta.TypeContent:
                    return string.Format("/contents{0}", meta.WebPath);
                case ContentMeta.TypeDirectory:
                    return string.Format("/index{0}", meta.WebPath);

                default:
                    return "#";
            }
            
        }
    }
}
