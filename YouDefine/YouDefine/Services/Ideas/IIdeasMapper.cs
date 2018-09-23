namespace YouDefine.Services
{
    using System;
    using System.Collections.Generic;
    using YouDefine.Models;

    /// <summary>
    /// IIdeasMapper interface
    /// providing methods for Ideas Mapper Service
    /// </summary>
    public interface IIdeasMapper
    {

        IdeaResult Map(Idea idea);

        List<IdeaResult> Map(List<Idea> ideas);

        DefinitionResult Map(Definition definition);

        string FormatLastModifiedDate(DateTime date);
    }
}
