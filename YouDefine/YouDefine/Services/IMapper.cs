namespace YouDefine.Services
{
    using System;
    using System.Collections.Generic;
    using YouDefine.Models;

    /// <summary>
    /// IMapper interface
    /// providing methods for Mapper Service
    /// </summary>
    public interface IMapper
    {

        IdeaResult Map(Idea idea);

        List<IdeaResult> Map(List<Idea> ideas);

        DefinitionResult Map(Definition definition);

        string FormatLastModifiedDate(DateTime date);
    }
}
