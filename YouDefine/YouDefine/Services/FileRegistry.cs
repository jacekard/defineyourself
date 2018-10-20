using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YouDefine.Services
{
    public static class FileRegistry
    {
        private static readonly string FilePrefix = "wwwroot/statistics/data/";

        public static readonly string IdeasByDateDates = FilePrefix + "ideas_by_date_x.txt";
        
        public static readonly string IdeasByDateIdeas = FilePrefix + "ideas_by_date_y.txt";

        public static readonly string DefinitionByIdeasTitles = FilePrefix + "definitions_by_ideas_titles.txt";

        public static readonly string DefinitionByIdeasTexts = FilePrefix + "definitions_by_ideas_texts.txt";

        public static readonly string SwearingsFile = "Data/swearings.txt";
    }
}
