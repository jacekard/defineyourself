namespace YouDefine.Models
{

    /// <summary>
    /// LikesResult Model
    /// Passed as json result to client-side via provider
    /// </summary>
    public class LikesResult
    {
        public int IdeaLikes { get; set; }

        public int DefLikes { get; set; }

    }
}
