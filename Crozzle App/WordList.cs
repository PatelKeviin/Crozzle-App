using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;
using System.Net;

namespace CrozzleApplication
{
    class WordList
    {
        #region constants
        private static readonly Char[] WordSeparators = new Char[] { ',' };
        #endregion

        #region properties - errors
        public static List<String> Errors { get; set; }

        public String FileErrors
        {
            get
            {
                int errorNumber = 1;
                String errors = "START PROCESSING FILE: " + WordlistFileName + "\r\n";

                foreach (String error in WordList.Errors)
                    errors += "error " + errorNumber++ + ": " + error + "\r\n";
                errors += "END PROCESSING FILE: " + WordlistFileName + "\r\n";

                return (errors);
            }
        }

        public String FileErrorsHTML
        {
            get
            {
                int errorNumber = 1;
                String errors = "<p style=\"font-weight:bold\">START PROCESSING FILE: " + WordlistFileName + "</p>";

                foreach (String error in WordList.Errors)
                    errors += "<p>error " + errorNumber++ + ": " + error + "</p>";
                errors += "<p style=\"font-weight:bold\">END PROCESSING FILE: " + WordlistFileName + "</p>";

                return (errors);
            }
        }
        #endregion

        #region properties - filenames
        public String WordlistPath { get; set; }
        public String WordlistFileName { get; set; }
        public String WordlistDirectoryName { get; set; }
        #endregion

        #region properties
        public String[] OriginalList { get; set; }
        public Boolean Valid { get; set; } = false;
        public List<String> List { get; set; }

        public int Count
        {
            get { return (List.Count); }
        }
        #endregion
      
        #region constructors
        public WordList(String path, Configuration aConfiguration, Boolean isRemote)
        {
            WordlistPath = path;
            if(!isRemote)
            {
                WordlistFileName = Path.GetFileName(path);
                WordlistDirectoryName = Path.GetDirectoryName(path);
            }
            List = new List<String>();
        }
        #endregion

        #region parsing
        public static Boolean TryParse(String path, Configuration aConfiguration, out WordList aWordList, Boolean isRemote = false)
        {
            StreamReader fileIn = null;
            if (!isRemote)
                fileIn = new StreamReader(path);
            else
            {
                WebClient webClient = new WebClient();
                Stream stream = webClient.OpenRead(path);
                fileIn = new StreamReader(stream);
            }

            Errors = new List<String>();
            aWordList = new WordList(path, aConfiguration, isRemote);

            // Split the original wordlist from the file.
            aWordList.OriginalList = fileIn.ReadLine().Split(WordSeparators);

            // Check each field in the wordlist.
            int fieldNumber = 0;
            foreach (String potentialWord in aWordList.OriginalList)
            {
                // Check that the field is not empty.
                if (potentialWord.Length > 0)
                {
                    // Check that the field is alphabetic.
                    if (Regex.IsMatch(potentialWord, Configuration.allowedCharacters))
                        aWordList.Add(potentialWord);
                    else
                        Errors.Add(String.Format(WordListErrors.AlphabeticError, potentialWord, fieldNumber));
                }
                else
                    Errors.Add(String.Format(WordListErrors.MissingWordError, fieldNumber));

                fieldNumber++;
            }

            // Check the minimmum word limit.
            if (aWordList.Count < aConfiguration.MinimumNumberOfUniqueWords)
                Errors.Add(String.Format(WordListErrors.MinimumSizeError, aWordList.Count, aConfiguration.MinimumNumberOfUniqueWords));
            
            // Check the maximum word limit.
            if (aWordList.Count > aConfiguration.MaximumNumberOfUniqueWords)
                Errors.Add(String.Format(WordListErrors.MaximumSizeError, aWordList.Count, aConfiguration.MaximumNumberOfUniqueWords));

            aWordList.Valid = Errors.Count == 0;
            return (aWordList.Valid);
        }
        #endregion

        #region list functions
        public void Add(String letters)
        {
            List.Add(letters);
        }

        public Boolean Contains(String letters)
        {
            return (List.Contains(letters));
        }
        #endregion
    }
}
