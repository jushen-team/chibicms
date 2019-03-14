using System;
using System.Collections.Generic;

namespace Jushen.ChibiCms.ChibiContent
{
    public class ContentManager
    {
        public string TopPath { get; }

        public ContentManager(string topPath)
        {
            TopPath = topPath;
        }

        /// <summary>
        /// get a list of content by using a filter
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public List<Content> GetContents(Dictionary<string,string> filter)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// get a lis tof metadata of content filtered by fileter
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public List<ContentMeta> GetContentMeta(Dictionary<string, string> filter)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// get a sigle content using a path
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public Content GetConent(string path)
        {
            return new Content(TopPath + path);
        }


    }
}
