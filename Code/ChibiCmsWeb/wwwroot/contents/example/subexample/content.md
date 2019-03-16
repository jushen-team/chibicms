# this is a sub example
## hahaha hhh

this is a picture

![](2019-03-16-10-28-46.png) 
```csharp
namespace Jushen.ChibiCms.ChibiContent
{
    public class ContentMeta
    {
        public DateTime ChangeTime { get; set; }

        public DateTime CreatedTime { get; set; }

        public int ViewedTimes { get; set; }

        public string Template { get; set; }

        public ContentMeta(string path)
        {
            string metaJson = File.ReadAllText(path + @"\meta.json");
            JsonConvert.PopulateObject(metaJson, this); 
        }
    }
}
```