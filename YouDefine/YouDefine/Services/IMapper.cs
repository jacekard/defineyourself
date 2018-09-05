using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YouDefine.Data;
using YouDefine.Models;

namespace YouDefine.Services
{
    public interface IMapper
    {

        IdeaResult Map(Idea idea);

        List<IdeaResult> Map(List<Idea> ideas);

        DefinitionResult Map(Definition definition);

        string FormatLastModifiedDate(DateTime date);
    }
}
