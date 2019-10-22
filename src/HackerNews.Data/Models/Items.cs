using System.Runtime.Serialization;

namespace HackerNews.Data.Models
{
    [DataContract]
    public class Items
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }
        
        [DataMember(Name = "title")]
        public string Title { get; set; }
        
        [DataMember(Name = "by")]
        public string Author { get; set; }
        
        [DataMember(Name = "url")]
        public string Url { get; set; }
    }
}