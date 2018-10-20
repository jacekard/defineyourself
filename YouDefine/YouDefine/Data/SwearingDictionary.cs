namespace YouDefine.Data
{
    using System;
    using System.IO;
    using System.Collections.Generic;
    using Services;

    public static class SwearingDictionary
    {
        public static Dictionary<string, long> Data = new Dictionary<string, long>();

        static SwearingDictionary()
        {
            using (var reader = new StreamReader(FileRegistry.SwearingsFile))
            {
                long id = 0;
                string word;
                while (!reader.EndOfStream)
                {
                    word = reader.ReadLine();
                    Data.Add(word, id++);

                }
            }
        }

        public static bool Has(string text)
        {
            string[] array = text.Split(' ');
            foreach (string item in array)
            {
                if (Data.ContainsKey(item))
                    return true;
            }

            return false;
        }
    }
}
