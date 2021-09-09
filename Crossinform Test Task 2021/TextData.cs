using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace MainDen.Crossinform.TestTask2021
{
    public class TextData
    {
        private readonly string m_text;

        public TextData(string text)
        {
            if (text is null)
                throw new ArgumentNullException(nameof(text));

            m_text = text;
        }

        public IDictionary<string, int> GetOccurrencesOfTokens(int tokenLength)
        {
            if (tokenLength <= 0)
                throw new ArgumentOutOfRangeException(nameof(tokenLength));

            var occurrences = new ConcurrentDictionary<string, int>();

            Parallel.For(0, m_text.Length - tokenLength + 1, tokenStart =>
            {
                var tokenEnd = tokenStart + tokenLength;
                
                for (var i = tokenStart; i < tokenEnd; ++i)
                    if (!Char.IsLetter(m_text[i]))
                        return;

                var token = m_text.Substring(tokenStart, tokenLength).ToLower();

                occurrences.AddOrUpdate(token, 1, (_, count) => count + 1);
            });

            return occurrences;
        }

        public static TextData FromFile(string path)
        {
            if (path is null)
                throw new ArgumentNullException(nameof(path));

            var text = File.ReadAllText(path);

            return new TextData(text);
        }
    }
}
