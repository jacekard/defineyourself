namespace YouDefine.Services
{
    using System.Linq;
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;
    using YouDefine.Data;
    using YouDefine.Models;
    using System;

    /// <summary>
    /// ProviderIdeas Service
    /// provides data using DBcontext
    /// </summary>
    public class IdeasProvider : IProviderIdeas
    {
        private readonly YouDefineContext _DBcontext;

        private readonly IIdeasMapper _mapper;

        public IdeasProvider(YouDefineContext DBcontext, IIdeasMapper mapper)
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
            bool ideaExist = true;
            try
            {
                var ideaPromise = _DBcontext.Ideas
                    .Where(m => m.Title == title)
                    .Single();
            }
            catch
            {
                ideaExist = false;
            }

            try
            {
                if (ideaExist == true)
                {

                    return null;
                }

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
            catch
            {
                return null;
            }
        }

        public DefinitionResult Update(string title, string text)
        {
            bool definitionPromise = true;
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
                return null;
            }

            try
            {
                var def = idea.Definitions.Where(x => x.Text.Equals(text)).Single();
            }
            catch
            {
                definitionPromise = false;
            }

            if (definitionPromise == true)
            {

                return null;
            }

            var definition = new Definition(text)
            {
                IdeaId = idea.IdeaId
            };

            idea.Definitions.Add(definition);
            _DBcontext.SaveChanges();

            return _mapper.Map(definition);
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
