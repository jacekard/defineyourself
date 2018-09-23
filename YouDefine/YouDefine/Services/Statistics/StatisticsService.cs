namespace YouDefine.Services
{
    using System;
    using System.IO;
    using System.Collections.Generic;
    using System.Linq;
    using YouDefine.Models;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// StatisticsController - API controller with I/O file methods
    /// </summary>
    [Produces("application/json")]
    [Route("api/stats")]
    public class StatisticsService : Controller, IStatisticsService 
    {
        private readonly IStatisticsProvider _provider;

        private readonly IWebServiceProvider _webProvider;

        public StatisticsService(IStatisticsProvider provider, IWebServiceProvider webProvider)
        {
            _provider = provider;
            _webProvider = webProvider;
        }

        public void InitializeStatistics()
        {
            IdeasByDateStats();
            //that's only one method for now.
        }

        [HttpGet]
        [Route("charsCount")]
        public IActionResult AllCharactersCount()
        {
            var data = _provider.GetAllCharactersCount();
            long ideasChars = data.Item1;
            long definitionsChars = data.Item2;

            return Json(
                new { IdeasChars = ideasChars, DefinitionsChars = definitionsChars }
                ); 
        }

        [HttpGet]
        [Route("websiteInfo")]
        public IActionResult WebsiteInfo()
        {
            var timespan = _webProvider.GetWebsiteOnlineDateTime();
            var days = Math.Round(timespan.TotalDays);
            var hours = Math.Round(timespan.TotalHours % 24);
            var minutes = Math.Round(timespan.TotalMinutes % 60);
            var seconds = Math.Round(timespan.TotalSeconds % 60);

            return Json(new { days, hours, minutes, seconds });
        }

        public IActionResult HowManyAuthorsCount()
        {
            long count = 5;

            return Json(new { authorsCount = count });
        }

        public void AuthorsByDateStats()
        {
        }

        public void DefinitionsByIdeasStats()
        {
            var data = _provider.GetIdeasAndDefinitionsCount();
            using (StreamWriter file = new StreamWriter("teeest.txt"))
            {
                file.WriteLine("writing in text file");
            }
        }

        public void IdeasByDateStats()
        {
            var data = _provider.GetIdeasCountByDate();
            using (var dates = new StreamWriter(FileRegistry.IdeasByDateDates))
            {
                using (var ideas = new StreamWriter(FileRegistry.IdeasByDateIdeas))
                {
                    foreach (var e in data)
                    {
                        ideas.WriteLine(e.Item1);
                        dates.WriteLine(e.Item2);
                    }
                }
            }
        }

        public void YouDefinePageDurationStats()
        { 
        }
    }
}