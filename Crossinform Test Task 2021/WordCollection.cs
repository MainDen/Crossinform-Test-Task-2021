using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MainDen.Crossinform.TestTask2021
{
    public class WordCollection
    {
        public IEnumerable<string> Words { get; }

        public WordCollection(string text)
        {
            if (text is null)
                throw new ArgumentNullException(nameof(text));

            Words = Regex.Matches(text.ToLower(), @"\w+", RegexOptions.Compiled).Select(match => match.Value);
        }

        public IDictionary<string, int> GetOccurrencesOfTokens(int tokenLength)
        {
            if (tokenLength < 0)
                throw new ArgumentOutOfRangeException(nameof(tokenLength));

            var occurrences = new ConcurrentDictionary<string, int>();
            
            Parallel.ForEach(Words, word =>
                Parallel.For(0, word.Length - tokenLength + 1, i =>
                    occurrences.AddOrUpdate(word.Substring(i, tokenLength), 1, (_, count) => count + 1)));

            return occurrences;
        }

        public IDictionary<string, int> GetOccurrencesOfWords(int minWordLength = 0, int maxWordLength = Int32.MaxValue)
        {
            if (minWordLength < 0)
                throw new ArgumentOutOfRangeException(nameof(minWordLength));
            if (maxWordLength < 0)
                throw new ArgumentOutOfRangeException(nameof(maxWordLength));

            var occurrences = new ConcurrentDictionary<string, int>();

            Parallel.ForEach(Words, word =>
            {
                if (word.Length >= minWordLength && word.Length <= maxWordLength)
                    occurrences.AddOrUpdate(word, 1, (_, count) => count + 1);
            });

            return occurrences;
        }
        
        public static WordCollection FromFile(string path)
        {
            if (path is null)
                throw new ArgumentNullException(nameof(path));
            
            var text = File.ReadAllText(path);

            return new WordCollection(text);
        }

        public static WordCollection FromFile(string path, Encoding encoding)
        {
            if (path is null)
                throw new ArgumentNullException(nameof(path));
            if (encoding is null)
                throw new ArgumentNullException(nameof(encoding));

            var text = File.ReadAllText(path, encoding);
            
            return new WordCollection(text);
        }
    }
}
