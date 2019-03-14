using Markdig;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Jushen.ChibiCms.ChibiContent
{
    public class Content
    {
        /// <summary>
        /// load content from a folder
        /// </summary>
        /// <param name="path">folder containing the content</param>
        public Content(string path)
        {
            try
            {
                string Markdown = File.ReadAllText(path+@"\content.md");
                Meta = new ContentMeta(path);
            }
            catch (Exception)
            {

                throw new Exception("create content failed");
            }
            
        }

        /// <summary>
        /// the metadata of this content
        /// </summary>
        public ContentMeta Meta { get; set; }

        /// <summary>
        /// the raw markdown in this content
        /// </summary>
        public string RawMarkdown { get; set; }

        /// <summary>
        /// render thet markdown to html
        /// </summary>
        /// <returns></returns>
        public string RenderHtml()
        {
            var pipeline = new MarkdownPipelineBuilder().UseBootstrap().UseAutoLinks().Build();
            var result = Markdown.ToHtml(RawMarkdown, pipeline);
            return result;
        }
        


    }
}
