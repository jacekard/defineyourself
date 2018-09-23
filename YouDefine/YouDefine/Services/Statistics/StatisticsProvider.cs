using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using YouDefine.Data;

namespace YouDefine.Services
{
    public class StatisticsProvider : IStatisticsProvider
    {
        private readonly YouDefineContext _DBcontext;

        public StatisticsProvider(YouDefineContext DBcontext)
        {
            _DBcontext = DBcontext;
        }

        public Tuple<long, long> GetAllCharactersCount()
        {
            long ideasCharCount = 0;
            long definitionsCharCount = 0;

            var ideas = _DBcontext.Ideas;
            foreach (var idea in ideas)
            {
                ideasCharCount += idea.Title.Count();
                foreach (var definition in idea.Definitions)
                {
                    definitionsCharCount += definition.Text.Count();
                }
            }

            return new Tuple<long, long>(ideasCharCount, definitionsCharCount);
        }

        public List<Tuple<long, string>> GetAuthorsCountByDate()
        {
            throw new NotImplementedException();
        }

        public List<Tuple<string, long>> GetIdeasAndDefinitionsCount()
        {
            var result = new List<Tuple<string, long>>();
            var ideas = _DBcontext.Ideas.Include(x => x.Definitions);
            
            foreach(var idea in ideas)
            {
                result.Add(new Tuple<string, long>(idea.Title, idea.Definitions.Count()));
            }

            return result;
        }

        public List<Tuple<long, string>> GetIdeasCountByDate()
        {
            var result = new List<Tuple<long, string>>();
            var aggregated = from idea in _DBcontext.Ideas
                        group idea by idea.CreationDate.ToShortDateString() into g
                        select new { IdeasCount = g.Count(), Date = g.Key };
                        
            foreach (var item in aggregated)
            {
                result.Add(new Tuple<long, string>(item.IdeasCount, item.Date));
            }

            return result;
        }
    }
}
