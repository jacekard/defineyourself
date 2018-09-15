namespace YouDefine.Models
{
    /// <summary>
    /// DefinitionResult Model
    /// Passed as json result to client-side via provider
    /// </summary>
    public class DefinitionResult
    {
        public long Id { get; set; }

        public string Text { get; set; }

        public int Likes { get; set; }

        public string Date { get; set; }
    }
}
