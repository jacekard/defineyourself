namespace YouDefine.Models
{
    using System.Collections.Generic;
    
    /// <summary>
    /// IdeaResult Model
    /// Passed as json result to client-side via provider
    /// </summary>
    public class IdeaResult
    {
        public string Title { get; set; }

        public string LastModifiedDate { get; set; }

        public int Likes { get; set; }

        public List<DefinitionResult> Definitions { get; set; }
    }
}
