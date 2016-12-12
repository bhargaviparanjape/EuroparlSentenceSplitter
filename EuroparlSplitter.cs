using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.IO;

namespace SentenceSplitter 
{
    public class EuroparlSplitter : ISplitter
    {
        protected List<string> nobreak;
        protected List<string> optbreak;
        protected string language;

        /// <summary>
        /// static lists of abberviations and start of sentence terms for various langauges
        /// <exception cref="FileNotFoundException">Make sure the file ending in that language abbreviation exists</exception>
        /// </summary>
        protected void CreateListofNonBreakWords()
        {
            string FullPath = Path.GetFullPath("nonbreaking_prefixes\nobreaking_prefix." + language);
            if (FullPath != null)
                foreach (string line in File.ReadAllLines(FullPath))
                {
                    if (line[0] != '#')
                    {
                        if (line.Contains("#")) optbreak.Add(line.Split()[0]);
                        else nobreak.Add(line);
                    }
                }
        }

        public EuroparlSplitter(string lang)
        {
            this.language = lang;
            nobreak = new List<string>();
            optbreak = new List<string>();
            CreateListofNonBreakWords();
        }

        public IList<string> Split(string Text)
        {
            string result = Regex.Replace(Text, " +", " ");
            result = Regex.Replace(result, " \n|\n ", "\n");
            result = Regex.Replace(result, "^ | $", "");
            result = Regex.Replace(result, "([?!]) +([\'\"([¿¡`]*[A-Z])", "$1\n$2");
            result = Regex.Replace(result, "([.][.]+) +([\'\"([¿¡`]*[A-Z])", "$1\n$2");
            result = Regex.Replace(result, "([?!.][ ]*[\'\"]+) +([\'\"([¿¡`]*[ ]*[A-Z])", "$1\n$2");
            result = Regex.Replace(result, "([?!.]) +([\'\"([¿¡`]+[ ]*[A-Z])", "$1\n$2");
            string[] words = result.Split(' ');
            string final = "";
            int i = 0;
            for (i = 0; i < words.Length - 1; i++)
            {
                var match = Regex.Match(words[i], "([A-Za-z0-9-]*)([\'\"\\u005d\\u0029\\u0025]*)([.]+)$"); //full match will result in 4 group matches only since the last group is a essential
                if (match.Groups.Count == 4 && nobreak.Contains(match.Groups[1].Value) && match.Groups[2].Value == "") ; //there should be a honorific but no starting punctuation but a . in the end
                else
                {
                    var match1 = Regex.Match(words[i], "([.])([A-Z-]+)([.]+)$"); //abbreaviations of the form .A-B.G.
                    if (match1.Groups.Count > 1) ; //multiple matched, no possibiity of 1, 2 matches
                    else
                    {
                        var match2 = Regex.Match(words[i + 1], "^([ ]*[\'\"`([¿¡]*[ ]*[A-Z0-9])");  //single match
                        var match3 = Regex.Match(words[i + 1], "^[0-9]+"); //single match
                        if (match.Groups.Count == 4 && optbreak.Contains(match.Groups[1].Value) && match.Groups[2].Value == "" && match3.Length > 0) ;
                        else if (match.Groups.Count == 4 && match2.Length > 0) { words[i] = words[i] + "\n"; Console.WriteLine("ENTERED"); }
                    }
                }
                final = final + words[i] + " ";
            }
            final = final + words[i];

            final = Regex.Replace(final, " +", " ");
            final = Regex.Replace(final, " \n|\n ", "\n");
            final = Regex.Replace(final, "^ | $", "");
            List<string> list = final.Split('\n').ToList();
            return list;
        }

        /// <summary>
        /// <exception cref="FileNotFoundException">Enter input and output files properly</exception>
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            string language = args[0];
            string FullPath = Path.GetFullPath(args[1]);
            string testString = File.ReadAllText(FullPath);
            ISplitter splitter = new EuroparlSplitter(language);
            IList<string> sentences = splitter.Split(testString);
            using (StreamWriter file =
            new StreamWriter(args[2]))
            {
                foreach (string line in sentences)
                    file.WriteLine(line);
            }

        }

    }
}
