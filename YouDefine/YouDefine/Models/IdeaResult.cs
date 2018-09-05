using System.Collections.Generic;
using YouDefine.Models;

namespace YouDefine.Data
{
    public class IdeaResult
    {
        public string Title { get; set; }

        public string LastModifiedDate { get; set; }

        public int Likes { get; set; }

        public List<DefinitionResult> Definitions { get; set; }
    }
}
