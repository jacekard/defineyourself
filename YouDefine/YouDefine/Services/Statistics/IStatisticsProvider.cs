using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YouDefine.Models;

namespace YouDefine.Services
{
    public interface IStatisticsProvider
    {
        List<Tuple<string, long>> GetIdeasAndDefinitionsCount();

        List<Tuple<long, string>> GetIdeasCountByDate();

        List<Tuple<long, string>> GetAuthorsCountByDate();

        Tuple<long, long> GetAllCharactersCount();
    }
}
