namespace YouDefine.Services
{
    using System.Linq;
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;
    using YouDefine.Data;
    using YouDefine.Models;

    /// <summary>
    /// ProviderIdeas Service
    /// provides data using DBcontext
    /// </summary>
    public class ProviderIdeas : IProviderIdeas
    {
        private readonly YouDefineContext _DBcontext;

        private readonly IMapper _mapper;

        public ProviderIdeas(YouDefineContext DBcontext, IMapper mapper)
        {
            _DBcontext = DBcontext;
            _mapper = mapper;
        }

        public IEnumerable<IdeaResult> GetAll()
        {
            try
            {
                var ideas = _DBcontext.Ideas.Include(x => x.Definitions).ToList();
                return _mapper.Map(ideas);
            }
            catch
            {
                return null;
            }
        }

        public IEnumerable<string> GetTitles()
        {
            try
            {
                var ideas = _DBcontext.Ideas
                    .OrderBy(x => x.Title)
                    .GroupBy(x => x.Title)
                    .Select(x => x.Key)
                    .ToArray();

                return ideas;
            }
            catch
            {
                return null;
            }
        }


        public IdeaResult GetSpecified(string title)
        {
            try
            {
                var idea = _DBcontext.Ideas
                    .Where(m => m.Title == title)
                    .Include(x => x.Definitions)
                    .Single();

                return _mapper.Map(idea);
            }
            catch
            {
                return null;
            }
        }

        public IdeaResult GetSpecified(long id)
        {
            try
            {
                var idea = _DBcontext.Ideas
                    .Where(m => m.IdeaId == id)
                    .Include(x => x.Definitions)
                    .OrderBy(y => y.Likes)
                    .Single();

                return _mapper.Map(idea);
            }
            catch
            {
                return null;
            }

        }

        public IdeaResult Add(string title, string text)
        {
            var idea = new Idea(title)
            {
                Definitions = new List<Definition>()
            };
            var definition = new Definition(text)
            {
                IdeaId = idea.IdeaId
            };

            idea.Definitions.Add(definition);
            _DBcontext.Ideas.Add(idea);
            _DBcontext.SaveChanges();

            return _mapper.Map(idea);
        }


        public IdeaResult Update(string title, string text)
        {
            Idea idea = null;
            try
            {
                idea = _DBcontext.Ideas.Where(x => x.Title == title)
                        .Include(x => x.Definitions)
                        .Single();

                idea.UpdateLastModifiedDate();
            }
            catch
            {
                return Add(title, text);
            }

            var definition = new Definition(text)
            {
                IdeaId = idea.IdeaId
            };
            idea.Definitions.Add(definition);
            _DBcontext.SaveChanges();

            return _mapper.Map(idea);
        }

        public LikesResult LikeDefinition(string title, long id)
        {
            try
            {
                var idea = _DBcontext.Ideas
                    .Where(m => m.Title == title)
                    .Include(m => m.Definitions)
                    .Single();

                idea.Likes++;
                var likes = ++idea.Definitions.Where(x => x.DefinitionId == id).Single().Likes;
                _DBcontext.SaveChanges();

                return new LikesResult { IdeaLikes = idea.Likes, DefLikes = likes };
            }
            catch
            {

                return null;
            }
        }

        public LikesResult UnlikeDefinition(string title, long id)
        {
            try
            {
                var idea = _DBcontext.Ideas
                    .Where(m => m.Title == title)
                    .Include(m => m.Definitions)
                    .Single();

                idea.Likes--;
                var likes = --idea.Definitions.Where(x => x.DefinitionId == id).Single().Likes;
                _DBcontext.SaveChanges();

                return new LikesResult { IdeaLikes = idea.Likes, DefLikes = likes };
            }
            catch
            {

                return null;
            }
        }
    }
}
