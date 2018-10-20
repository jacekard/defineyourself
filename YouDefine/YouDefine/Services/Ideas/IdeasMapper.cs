namespace YouDefine.Services
{
    using System;
    using System.Collections.Generic;
    using YouDefine.Models;

    /// <summary>
    /// Mapper Service maps entity models to result models
    /// </summary>
    public class IdeasMapper : IIdeasMapper
    {
        public IdeaResult Map(Idea idea)
        {
            var date = FormatLastModifiedDate(idea.LastModifiedDate);
            var definitions = new List<DefinitionResult>();
            foreach (var def in idea.Definitions)
            {
                definitions.Add(Map(def));
            }

            definitions.Sort((b, a) => a.Likes.CompareTo(b.Likes));

            var ideaResult = new IdeaResult()
            {
                Title = idea.Title,
                LastModifiedDate = date,
                Likes = idea.Likes,
                Definitions = definitions
            };

            return ideaResult;
        }

        public List<IdeaResult> Map(List<Idea> ideas)
        {
            var ideaResults = new List<IdeaResult>();
            foreach (var idea in ideas)
            {
                ideaResults.Add(Map(idea));
            }
            return ideaResults;
        }

        public DefinitionResult Map(Definition definition)
        {
            var date = FormatLastModifiedDate(definition.CreationDate);

            var definitionResult = new DefinitionResult()
            {
                Id = definition.DefinitionId,
                Text = definition.Text,
                Likes = definition.Likes,
                Date = date
            };

            return definitionResult;
        }

        public string FormatLastModifiedDate(DateTime date)
        {
            var time = DateTime.Now - date;
            var days = Math.Round(time.TotalDays);
            var hours = Math.Round(time.TotalHours % 24);
            var minutes = Math.Round(time.TotalMinutes % 60);
            var seconds = Math.Round(time.TotalSeconds % 60);
            string result = String.Empty;
            if (days > 0)
            {
                result += days;
                result += days == 1 ? " day " : " days ";

                if (hours > 0)
                {
                    result += hours;
                    result += hours == 1 ? " hour " : " hours ";
                }
                
                return result + "ago";
            }

            if (hours > 0)
            {
                result += hours;
                result += hours == 1 ? " hour " : " hours ";

                if (minutes > 0)
                {
                    result += minutes;
                    result += minutes == 1 ? " minute " : " minutes ";
                }

                return result + "ago";
            }

            if (minutes > 0)
            {
                result += minutes;
                result += minutes == 1 ? " minute " : " minutes ";
            }

            if (seconds == 0)
                result = "just now";
            else
            {
                result += seconds;
                result += seconds == 1 ? " second " : " seconds ";
                result += "ago";
            }

            return result;
        }
    }
}
