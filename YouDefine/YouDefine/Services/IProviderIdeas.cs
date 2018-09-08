﻿using System.Collections.Generic;
using YouDefine.Data;

namespace YouDefine.Services
{
    public interface IProviderIdeas
    {

        IEnumerable<IdeaResult> GetAll();

        IEnumerable<string> GetTitles();

        IdeaResult GetSpecified(string title);

        IdeaResult GetSpecified(long id);

        IdeaResult Add(string title, string text);

        IdeaResult Update(string title, string text);

        int LikeDefinition(string title, long id);
    }
}
