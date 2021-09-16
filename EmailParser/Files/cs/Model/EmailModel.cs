using EmailParser.API;
using System.Runtime.Serialization;

namespace EmailParser.Model
{
    [DataContract]
    public class EmailModel :IEmailModel
    {
        [DataMember(Name ="IsHtmlBody")]
        public bool IsHtmlBody { get; set; }

        [DataMember(Name ="Title")]
        public string Title { get; set; }
        
        [DataMember(Name ="Body")]
        public string Body { get; set; }
        
        [DataMember(Name ="Preview")]
        public string Preview { get; set; }
    }
}



