namespace YouDefine.Services
{
    using System.Collections.Generic;
    using YouDefine.Models;

    /// <summary>
    /// IProviderIdeas interface
    /// providing methods for ProviderIdeas Service
    /// </summary>
    public interface IProviderIdeas
    {

        IEnumerable<IdeaResult> GetAll();

        IEnumerable<string> GetTitles();

        IdeaResult GetSpecified(string title);

        IdeaResult GetSpecified(long id);

        IdeaResult Add(string title, string text);

        IdeaResult Update(string title, string text);

        LikesResult LikeDefinition(string title, long id);

        LikesResult UnlikeDefinition(string title, long id);

    }
}
