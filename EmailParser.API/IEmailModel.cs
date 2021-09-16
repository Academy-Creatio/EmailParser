namespace EmailParser.API
{

    /// <summary>
    /// Represents Email from db
    /// </summary>
    public interface IEmailModel
    {
        /// <value>True if body of the email is in HTML</value>
        bool IsHtmlBody { get;}

        /// <value>Email Subject</value>
        string Title { get;}

        /// <value>Email Body</value>
        string Body { get;}

        /// <value>HTML Sanitized text</value>
        string Preview { get;}
    }
}
