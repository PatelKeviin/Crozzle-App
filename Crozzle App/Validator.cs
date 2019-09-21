using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;

namespace CrozzleApplication
{
    class Validator
    {
        #region properties - errors
        public static List<String> Errors { get; set; }
        #endregion

        #region pattern checking
        // Check that a string matches a pattern.
        public static Boolean IsMatch(String input, String pattern)
        {
            Errors = new List<String>();

            if (!Regex.IsMatch(input, pattern))
                Errors.Add(String.Format(ValidatorErrors.PatternError, pattern));
            
            return (Errors.Count == 0);
        }
        #endregion

        #region type checking
        // Check that a numeric field is an integer.
        public static Boolean IsInt32(String field, out int anInteger)
        {
            int n = -1;
            Errors = new List<String>();

            if (!Int32.TryParse(field, out n))
                Errors.Add(ValidatorErrors.IntegerError);

            anInteger = n;
            return (Errors.Count == 0);
        }

        // Check that a string field is a Boolean.
        public static Boolean IsBoolean(String field, out Boolean aBoolean)
        {
            Boolean b = true;
            Errors = new List<String>();

            if (!Boolean.TryParse(field, out b))
                Errors.Add(ValidatorErrors.BooleanError);

            aBoolean = b;
            return (Errors.Count == 0);
        }

        // Check that a string field is a hex colour code such as #56ab7f.
        public static Boolean IsHexColourCode(String hexColour)
        {
            Errors = new List<String>();

            if (!Regex.IsMatch(hexColour, @"^#[0-9a-fA-F]{6}$"))
                Errors.Add(ValidatorErrors.HexColourCodeError);

            return (Errors.Count == 0);
        }

        // Check that a string field is a hex colour code such as #56ab7f.
        public static Boolean IsFilename(String name)
        {
            Errors = new List<String>();

            if (name.IndexOfAny(Path.GetInvalidPathChars()) != -1)
                    Errors.Add(ValidatorErrors.FilenameError);

            return (Errors.Count == 0);
        }

        // Check that a string field is delimited by double quotes.
        public static Boolean IsDelimited(String field, Char[] delimiters)
        {
            Boolean delimited = false;

            foreach (Char delimiter in delimiters)
                if (Regex.IsMatch(field, @"^" + delimiter + ".*" + delimiter + "$"))
                    delimited = true;

            Errors = new List<String>();
            if (!delimited)
                Errors.Add(String.Format(ValidatorErrors.DelimiterError, new String(delimiters)));

            return (delimited);
        }

        // Check that a string field is an HTML style.
        public static Boolean IsStyleTag(String style)
        {
            Errors = new List<String>();

            if (!Regex.IsMatch(style, @"^<[sS][tT][yY][lL][eE]>.*</[sS][tT][yY][lL][eE]>$"))
                Errors.Add(ValidatorErrors.StyleError);

            return (Errors.Count == 0);
        }
        #endregion

        #region range checking
        // Check that a numeric field is in range.
        public static Boolean TryRange(int n, int lowerLimit, int upperLimit)
        {
            Errors = new List<String>();

            if (n < lowerLimit || n > upperLimit)
                Errors.Add(String.Format(ValidatorErrors.RangeError, lowerLimit, upperLimit));

            return (Errors.Count == 0);
        }
        #endregion
    }
}
