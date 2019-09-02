using Jushen.ChibiCms.ChibiContent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ChibiCmsWeb.Helpers
{
    public static class ContentHtmlHelper
    {
        public static string GetLinkToContent(ContentMeta meta)
        {
            switch (meta.ContentType)
            {
                case ContentMeta.TypeContent:
                case ContentMeta.TypeLink:
                    return string.Format("/contents{0}", meta.WebPath);
                case ContentMeta.TypeDirectory:
                    return string.Format("/index{0}", meta.WebPath);

                default:
                    return "#";
            }
            
        }

        public static string RemoveScript(this string input)
        {
            Regex rRemScript = new Regex(@"<script[^>]*>[\s\S]*?</script>");
            return rRemScript.Replace(input, "");
        }
    }
}
