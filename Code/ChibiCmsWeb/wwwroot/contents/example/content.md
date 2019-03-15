# this is an example
## hahaha

this is a picture


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