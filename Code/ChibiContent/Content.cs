using Markdig;
using Markdig.Renderers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Jushen.ChibiCms.ChibiContent
{
    public class Content
    {
        public const string  ContentFileName= @"content.md";
        private string topPath;

        /// <summary>
        /// load content from a folder
        /// </summary>
        /// <param name="path">folder containing the content</param>
        public Content(string path)
        {
            topPath = path;
            try
            {
                RawMarkdown = File.ReadAllText(Path.Combine(path, ContentFileName));
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


        /// <summary>
        /// this renders the markdown to html, with a base url to make image path in the markdown a absolute path,
        /// </summary>
        /// <param name="baseUrl">the base url for image in the markdown</param>
        /// <returns></returns>
        public string RenderHtml(string baseUrl)
        {
            var pipeline = new MarkdownPipelineBuilder().UseBootstrap().UseAutoLinks().Build();
           
            var writer = new StringWriter();
            var renderer = new HtmlRenderer(writer);
            if (baseUrl != null)
                renderer.BaseUrl = new Uri(baseUrl);
            pipeline.Setup(renderer);

            var doc = Markdown.Parse(RawMarkdown, pipeline);
            renderer.Render(doc);
            writer.Flush();

            var result = writer.ToString();
            return result;
        }




    }
}
