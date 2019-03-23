using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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
        public List<Content> GetContents(Dictionary<string, string> filter)
        {
            throw new NotImplementedException();
        }

        public List<ContentMeta> GetContentMeta(int page = 1, int pageSize = 10)
        {
            return GetContentMeta("",page,pageSize);
        }

        /// <summary>
        /// list lasted content with pagination
        /// </summary>
        /// <param name="page">the page start from 1</param>
        /// <param name="pageSize">page size</param>
        /// <returns></returns>
        public List<ContentMeta> GetContentMeta(string path, int page = 1, int pageSize = 10)
        {
            //get all content top directories
            var contentTops = Directory.GetDirectories(Path.Combine(TopPath, path), "*", SearchOption.AllDirectories);

            var metas = new List<ContentMeta>();
            //load meta
            foreach (var contentTop in contentTops)
            {
                var webpath = contentTop.Substring(TopPath.Length);
                //todo: validata the top path before creating meta
                var tMeta = new ContentMeta(contentTop, webpath);
                metas.Add(tMeta);
                //update the update time
                //this is very ineffecient must revise
                var updateTime = File.GetLastWriteTime(Path.Combine(contentTop, Content.ContentFileName));
                if (tMeta.ChangeTime != updateTime)
                {
                    tMeta.ChangeTime = updateTime;
                    tMeta.Update();
                }
            }
            //sort and return
            return metas.OrderByDescending(mt => mt.ChangeTime).Skip((page - 1) * pageSize).Take(pageSize).ToList();

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
            return new Content(Path.Combine(TopPath, path), path);
        }


    }
}
