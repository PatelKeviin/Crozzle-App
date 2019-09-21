using System;
using System.Collections.Generic;
using System.Linq;

namespace CrozzleApplication
{
    class CrozzleSequences
    {
        #region properties - errors
        public List<String> ErrorMessages { get; set; }
        private bool GridWordDataErrors { get; set; }
        #endregion

        #region properties
        private List<WordData> Sequences { get; set; }
        private List<WordData> HorizontalSequences { get; set; }
        private List<WordData> VerticalSequences { get; set; }
        public Configuration Configuration { get; set; }

        public int Count
        {
            get { return (HorizontalSequences.Count + VerticalSequences.Count); }
        }

        public Boolean ErrorsDetected
        {
            get { return (ErrorMessages.Count > 0); }
        }

        public Boolean GridWordDataErrorsDetected
        {
            get { return (GridWordDataErrors == true); }
            set { GridWordDataErrors = value; }
        }
        #endregion

        #region constructors
        public CrozzleSequences(List<String[]> crozzleRows, List<String[]> crozzleColumns, Configuration aConfiguration)
        {
            Sequences = new List<WordData>();
            HorizontalSequences = new List<WordData>();
            VerticalSequences = new List<WordData>();
            ErrorMessages = new List<string>();
            Configuration = aConfiguration;

            this.AddHorizontalSequences(crozzleRows);
            this.AddVerticalSequences(crozzleColumns);
        }
        #endregion

        #region copy constructor
        public CrozzleSequences(CrozzleSequences crozzleSequences)
        {
            // copy Sequences
            List<WordData> copySequences = new List<WordData>();
            // copy HorizontalSequences
            List<WordData> copyHorizontalSequences = new List<WordData>();
            // copy VerticalSequences
            List<WordData> copyVerticalSequences = new List<WordData>();

            foreach (WordData wordData in crozzleSequences.Sequences)
            {
                if(wordData.IsHorizontal)
                    copyHorizontalSequences.Add(new WordData(wordData));
                else
                    copyVerticalSequences.Add(new WordData(wordData));

                copySequences.Add(new WordData(wordData));
            }
            Sequences = copySequences;
            HorizontalSequences = copyHorizontalSequences;
            VerticalSequences = copyVerticalSequences;

            // copy ErrorMessages
            List<String> copyErrorMessages = new List<String>();
            foreach (String error in crozzleSequences.ErrorMessages)
                copyErrorMessages.Add(String.Copy(error));
            ErrorMessages = copyErrorMessages;

            // shallow copying Configuration (as it is only used for readOnly purposes)
            Configuration = crozzleSequences.Configuration;
        }
        #endregion

        #region identify sequences
        private void AddHorizontalSequences(List<String[]> crozzleRows)
        {
            int rowNumber = 0;
            int columnIndex;
            String row;
            foreach (String[] crozzleRow in crozzleRows)
            {
                rowNumber++;
                columnIndex = 0;

                // Place all letters into one string, so that we can split it later.
                row = "";
                foreach (String letter in crozzleRow)
                    row = row + letter;

                // Use split to collect all sequences of letters.
                String[] letterSequences = row.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                // Collect and store data about each letter sequence of length > 1, as a sequence of one letter is not a word.
                foreach (String sequence in letterSequences)
                {
                    if (sequence.Length > 1)
                    {
                        // Set column values.
                        int columnNumber = row.IndexOf(sequence, columnIndex) + 1;

                        //////// check for duplicate word
                        //////WordData duplicate = Sequences.Find(x => x.Letters.Equals(sequence));
                        //////if (duplicate != null)
                        //////    ErrorMessages.Add("\"" + sequence + "\" at (" + rowNumber + ", " + columnNumber + ") already exists in the crozzle at (" + duplicate.Location.Row + ", " + duplicate.Location.Column + ")");


                        //////// Check that duplicate words are within limits.
                        //////List<WordData> duplicates = Sequences.FindAll(x => x.Letters.Equals(sequence));
                        //////if (duplicates.Count < Configuration.MinimumNumberOfTheSameWord)
                        //////    ErrorMessages.Add("\"" + sequence + "\" at (" + rowNumber + ", " + columnNumber + ") exists in the crozzle " + duplicates.Count +
                        //////        " times, which is more than the limit of " + Configuration.MinimumNumberOfTheSameWord);
                        //////if (duplicates.Count > Configuration.MaximumNumberOfTheSameWord)
                        //////    ErrorMessages.Add("\"" + sequence + "\" at (" + rowNumber + ", " + columnNumber + ") exists in the crozzle " + duplicates.Count +
                        //////        " times, which is more than the limit of " + Configuration.MaximumNumberOfTheSameWord);

                        // Collect data about the word, and 
                        // update the index for the next substring search.
                        WordData word = new WordData(WordData.OrientationRow, rowNumber, row.IndexOf(sequence, columnIndex) + 1, sequence);
                        columnIndex = word.Location.Column - 1 + sequence.Length;

                        // Store data about the word.
                        Sequences.Add(word);
                        HorizontalSequences.Add(word);
                    }
                }
            }
        }

        private void AddVerticalSequences(List<String[]> crozzleColumns)
        {
            int columnNumber = 0;
            int rowIndex;
            String column;
            foreach (String[] crozzleColumn in crozzleColumns)
            {
                columnNumber++;
                rowIndex = 0;

                // Place all letters into one string, so that we can split it later.
                column = "";
                foreach (String letter in crozzleColumn)
                    column = column + letter;

                // Use split to collect all sequences of letters.
                String[] letterSequences = column.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                // Collect and store data about each letter sequence of length > 1, as a sequence of one letter is not a word.
                foreach (String sequence in letterSequences)
                {
                    if (sequence.Length > 1)
                    {
                        // Set row values.
                        int rowNumber = column.IndexOf(sequence, rowIndex) + 1;

                        ////////// Check that duplicate words are within limits.
                        ////////List<WordData> duplicates = Sequences.FindAll(x => x.Letters.Equals(sequence));
                        ////////if (duplicates.Count < Configuration.MinimumNumberOfTheSameWord)
                        ////////    ErrorMessages.Add("\"" + sequence + "\" at (" + rowNumber + ", " + columnNumber + ") exists in the crozzle " + duplicates.Count + 
                        ////////        " times, which is more than the limit of " + Configuration.MinimumNumberOfTheSameWord);
                        ////////if (duplicates.Count > Configuration.MaximumNumberOfTheSameWord)
                        ////////    ErrorMessages.Add("\"" + sequence + "\" at (" + rowNumber + ", " + columnNumber + ") exists in the crozzle " + duplicates.Count +
                        ////////        " times, which is more than the limit of " + Configuration.MaximumNumberOfTheSameWord);

                        // Collect data about the word, and 
                        // update the index for the next substring search.
                        WordData word = new WordData(WordData.OrientationColumn, rowNumber, columnNumber, sequence);
                        rowIndex = word.Location.Row - 1 + sequence.Length;

                        // Store data about the word.
                        Sequences.Add(word);
                        VerticalSequences.Add(word);
                    }
                }
            }
        }
        #endregion
        
        #region intersecting letters
        public List<Char> GetIntersectingLetters()
        {
            List<Char> intersectingLetters = new List<Char>();

            foreach (WordData horizontalSequence in HorizontalSequences)
                intersectingLetters.AddRange(GetIntersectingLetters(horizontalSequence));
            return (intersectingLetters);
        }

        private List<Char> GetIntersectingLetters(WordData horizontalWord)
        {
            List<Char> intersectingLetters = new List<Char>();

            foreach (WordData verticalSequence in VerticalSequences)
            {
                if (verticalSequence.Location.Row == horizontalWord.Location.Row)
                {
                    if (verticalSequence.Location.Column >= horizontalWord.Location.Column &&
                        verticalSequence.Location.Column < horizontalWord.Location.Column + horizontalWord.Letters.Length)
                        intersectingLetters.Add(verticalSequence.Letters[0]);
                }
                else if (verticalSequence.Location.Row < horizontalWord.Location.Row)
                {
                    if (verticalSequence.Location.Column >= horizontalWord.Location.Column &&
                        verticalSequence.Location.Column < horizontalWord.Location.Column + horizontalWord.Letters.Length &&
                        verticalSequence.Location.Row + verticalSequence.Letters.Length > horizontalWord.Location.Row)
                        intersectingLetters.Add(verticalSequence.Letters[horizontalWord.Location.Row - verticalSequence.Location.Row]);
                }
            }
            return (intersectingLetters);
        }

        public List<Char> GetVerticalIntersections(WordData horizontalWord)
        {
            List<Char> intersectingLetters = new List<Char>();

            foreach (WordData verticalSequence in VerticalSequences)
            {
                if (verticalSequence.Location.Row == horizontalWord.Location.Row)
                {
                    if (verticalSequence.Location.Column >= horizontalWord.Location.Column &&
                        verticalSequence.Location.Column < horizontalWord.Location.Column + horizontalWord.Letters.Length)
                        intersectingLetters.Add(verticalSequence.Letters[0]);
                }
                else if (verticalSequence.Location.Row < horizontalWord.Location.Row)
                {
                    if (verticalSequence.Location.Column >= horizontalWord.Location.Column &&
                        verticalSequence.Location.Column < horizontalWord.Location.Column + horizontalWord.Letters.Length &&
                        verticalSequence.Location.Row + verticalSequence.Letters.Length > horizontalWord.Location.Row)
                        intersectingLetters.Add(verticalSequence.Letters[horizontalWord.Location.Row - verticalSequence.Location.Row]);
                }
            }

            return (intersectingLetters);
        }

        public List<Coordinate> GetVerticalIntersectionLocations(WordData horizontalWord)
        {
            List<Coordinate> intersectingCoordinates = new List<Coordinate>();

            foreach (WordData verticalSequence in VerticalSequences)
            {
                if (verticalSequence.Location.Row == horizontalWord.Location.Row)
                {
                    if (verticalSequence.Location.Column >= horizontalWord.Location.Column &&
                        verticalSequence.Location.Column < horizontalWord.Location.Column + horizontalWord.Letters.Length)
                        intersectingCoordinates.Add(new Coordinate(horizontalWord.Location.Row, verticalSequence.Location.Column));
                }
                else if (verticalSequence.Location.Row < horizontalWord.Location.Row)
                {
                    if (verticalSequence.Location.Column >= horizontalWord.Location.Column &&
                        verticalSequence.Location.Column < horizontalWord.Location.Column + horizontalWord.Letters.Length &&
                        verticalSequence.Location.Row + verticalSequence.Letters.Length > horizontalWord.Location.Row)
                        intersectingCoordinates.Add(new Coordinate(horizontalWord.Location.Row, verticalSequence.Location.Column));
                }
            }

            return intersectingCoordinates;
        }

        public List<Char> GetHorizontalIntersections(WordData verticalWord)
        {
            List<Char> intersectingLetters = new List<Char>();

            foreach (WordData horizontalSequence in HorizontalSequences)
            {
                if (horizontalSequence.Location.Column == verticalWord.Location.Column)
                {
                    if (horizontalSequence.Location.Row >= verticalWord.Location.Row &&
                        horizontalSequence.Location.Row < verticalWord.Location.Row + verticalWord.Letters.Length)
                        intersectingLetters.Add(horizontalSequence.Letters[0]);
                }
                else if (horizontalSequence.Location.Column < verticalWord.Location.Column)
                {
                    if (horizontalSequence.Location.Row >= verticalWord.Location.Row &&
                        horizontalSequence.Location.Row < verticalWord.Location.Row + verticalWord.Letters.Length &&
                        horizontalSequence.Location.Column + horizontalSequence.Letters.Length > verticalWord.Location.Column)
                        intersectingLetters.Add(horizontalSequence.Letters[verticalWord.Location.Column - horizontalSequence.Location.Column]);
                }
            }

            return (intersectingLetters);
        }

        public List<Coordinate> GetHorizontalIntersectionLocations(WordData verticalWord)
        {
            List<Coordinate> intersectingLocations = new List<Coordinate>();

            foreach (WordData horizontalSequence in HorizontalSequences)
            {
                if (horizontalSequence.Location.Column == verticalWord.Location.Column)
                {
                    if (horizontalSequence.Location.Row >= verticalWord.Location.Row &&
                        horizontalSequence.Location.Row < verticalWord.Location.Row + verticalWord.Letters.Length)
                        intersectingLocations.Add(new Coordinate(horizontalSequence.Location.Row, verticalWord.Location.Column));
                }
                else if (horizontalSequence.Location.Column < verticalWord.Location.Column)
                {
                    if (horizontalSequence.Location.Row >= verticalWord.Location.Row &&
                        horizontalSequence.Location.Row < verticalWord.Location.Row + verticalWord.Letters.Length &&
                        horizontalSequence.Location.Column + horizontalSequence.Letters.Length > verticalWord.Location.Column)
                        intersectingLocations.Add(new Coordinate(horizontalSequence.Location.Row, verticalWord.Location.Column));
                }
            }

            return intersectingLocations;
        }
        #endregion

        #region missing words
        public void FindMissingWords(WordList wordList)
        {
            foreach (WordData sequence in Sequences)
                if (!wordList.Contains(sequence.Letters))
                    ErrorMessages.Add(String.Format(CrozzleErrors.MissingFromWordlistError, sequence.Letters, sequence.Location.Row, sequence.Location.Column));
        }
        #endregion

        #region check duplicate words
        public void CheckDuplicateWords(int lowerLimit, int upperLimit)
        {
            // Create unique sequences.
            List<string> uniqueSequences = new List<string>();
            foreach (WordData sequence in Sequences)
                if (!uniqueSequences.Contains(sequence.Letters))
                    uniqueSequences.Add(sequence.Letters);

            // Check the number of occurances.
            foreach (String letters in uniqueSequences)
            {
                List<WordData> duplicates = Sequences.FindAll(x => x.Letters.Equals(letters));

                if (duplicates.Count > 1)
                {
                    if (duplicates.Count < Configuration.MinimumNumberOfTheSameWord || duplicates.Count > Configuration.MaximumNumberOfTheSameWord)
                        ErrorMessages.Add(String.Format(CrozzleErrors.DuplicateWordCountError, 
                            letters, duplicates.Count, Configuration.MinimumNumberOfTheSameWord, Configuration.MaximumNumberOfTheSameWord));
                }
            }
        }
        #endregion

        #region check intersections
        public void CheckHorizontalIntersections(int lowerLimit, int upperLimit)
        {
            foreach (WordData sequence in Sequences)
            {
                if (sequence.IsHorizontal)
                {
                    int verticalIntersections = GetVerticalIntersectingWords(sequence).Count;
                    if (verticalIntersections < lowerLimit || verticalIntersections > upperLimit)
                        ErrorMessages.Add(String.Format(CrozzleErrors.VerticalIntersectionsError, 
                            sequence.Letters, verticalIntersections, lowerLimit, upperLimit));
                }
            }
        }

        public void CheckVerticalIntersections(int lowerLimit, int upperLimit)
        {
            foreach (WordData sequence in Sequences)
            {
                if (sequence.IsVertical)
                {
                    int horizontalIntersections = GetHorizontalIntersectingWords(sequence).Count;
                    if (horizontalIntersections < lowerLimit || horizontalIntersections > upperLimit)
                        ErrorMessages.Add(String.Format(CrozzleErrors.HorizontalIntersectionsError,
                            sequence.Letters, horizontalIntersections, lowerLimit, upperLimit));
                }
            }
        }

        private List<WordData> GetVerticalIntersectingWords(WordData horizontalWord)
        {
            List<WordData> verticalWords = new List<WordData>();

            foreach (WordData verticalSequence in VerticalSequences)
            {
                if (verticalSequence.Location.Row == horizontalWord.Location.Row)
                {
                    if (verticalSequence.Location.Column >= horizontalWord.Location.Column &&
                        verticalSequence.Location.Column < horizontalWord.Location.Column + horizontalWord.Letters.Length)
                        verticalWords.Add(horizontalWord);
                }
                else if (verticalSequence.Location.Row < horizontalWord.Location.Row)
                {
                    if (verticalSequence.Location.Column >= horizontalWord.Location.Column &&
                        verticalSequence.Location.Column < horizontalWord.Location.Column + horizontalWord.Letters.Length &&
                        verticalSequence.Location.Row + verticalSequence.Letters.Length > horizontalWord.Location.Row)
                            verticalWords.Add(horizontalWord);
                }
            }
            return (verticalWords);
        }

        private List<WordData> GetHorizontalIntersectingWords(WordData verticalWord)
        {
            List<WordData> horizontalWords = new List<WordData>();

            foreach (WordData horizontalSequence in HorizontalSequences)
            {
                if (horizontalSequence.Location.Column == verticalWord.Location.Column)
                {
                    if (horizontalSequence.Location.Row >= verticalWord.Location.Row &&
                        horizontalSequence.Location.Row < verticalWord.Location.Row + verticalWord.Letters.Length)
                        horizontalWords.Add(horizontalSequence);
                }
                else if (horizontalSequence.Location.Column < verticalWord.Location.Column)
                {
                    if (horizontalSequence.Location.Row >= verticalWord.Location.Row &&
                        horizontalSequence.Location.Row < verticalWord.Location.Row + verticalWord.Letters.Length &&
                        horizontalSequence.Location.Column + horizontalSequence.Letters.Length > verticalWord.Location.Column)
                        horizontalWords.Add(horizontalSequence);
                }
            }
            return (horizontalWords);
        }

        public void CheckVerticalIntersectionOverlapping(WordData horizontalWord)
        {
            foreach (WordData verticalSequence in VerticalSequences)
            {
                if (verticalSequence.Location.Row == horizontalWord.Location.Row)
                {
                    if (verticalSequence.Location.Column >= horizontalWord.Location.Column &&
                        verticalSequence.Location.Column < horizontalWord.Location.Column + horizontalWord.Letters.Length)
                        if (!(verticalSequence.Letters[0] == horizontalWord.Letters[verticalSequence.Location.Column - horizontalWord.Location.Column]))
                            GridWordDataErrors = true;
                }
                else if (verticalSequence.Location.Row < horizontalWord.Location.Row)
                {
                    if (verticalSequence.Location.Column >= horizontalWord.Location.Column &&
                        verticalSequence.Location.Column < horizontalWord.Location.Column + horizontalWord.Letters.Length &&
                        verticalSequence.Location.Row + verticalSequence.Letters.Length > horizontalWord.Location.Row)
                        if (!(verticalSequence.Letters[horizontalWord.Location.Row - verticalSequence.Location.Row] == horizontalWord.Letters[verticalSequence.Location.Column - horizontalWord.Location.Column]))
                            GridWordDataErrors = true;
                }
            }
        }

        public void CheckHorizontalIntersectionOverlapping(WordData verticalWord)
        {
            foreach (WordData horizontalSequence in HorizontalSequences)
            {
                if (horizontalSequence.Location.Column == verticalWord.Location.Column)
                {
                    if (horizontalSequence.Location.Row >= verticalWord.Location.Row &&
                        horizontalSequence.Location.Row < verticalWord.Location.Row + verticalWord.Letters.Length)
                        if (!(horizontalSequence.Letters[0] == verticalWord.Letters[horizontalSequence.Location.Row - verticalWord.Location.Row]))
                            GridWordDataErrors = true;
                }
                else if (horizontalSequence.Location.Column < verticalWord.Location.Column)
                {
                    if (horizontalSequence.Location.Row >= verticalWord.Location.Row &&
                        horizontalSequence.Location.Row < verticalWord.Location.Row + verticalWord.Letters.Length &&
                        horizontalSequence.Location.Column + horizontalSequence.Letters.Length > verticalWord.Location.Column)
                        if (!(horizontalSequence.Letters[verticalWord.Location.Column - horizontalSequence.Location.Column] == verticalWord.Letters[horizontalSequence.Location.Row - verticalWord.Location.Row]))
                            GridWordDataErrors = true;
                }
            }
        }
        #endregion

        #region check touching words
        public void CheckTouchingWords(WordData wordData)
        {
            if(wordData.IsHorizontal)
                CheckTouchingHorizontalWordData(wordData);
            else
                CheckTouchingVerticalWordData(wordData);
        }

        private void CheckTouchingHorizontalWordData(WordData wordData)
        {
            foreach(WordData hwordData in HorizontalSequences)
            {
                if(hwordData.Location.Row == wordData.Location.Row)
                {
                    if (hwordData.Location.Column < wordData.Location.Column - 1 && hwordData.Location.Column + hwordData.Letters.Length >= wordData.Location.Column)
                        GridWordDataErrors = true;
                    else if (hwordData.Location.Column >= wordData.Location.Column - 1 && hwordData.Location.Column <= wordData.Location.Column + wordData.Letters.Length)
                        GridWordDataErrors = true;
                }
                else if (hwordData.Location.Row == wordData.Location.Row - 1 || hwordData.Location.Row == wordData.Location.Row + 1)
                {
                    if (hwordData.Location.Column < wordData.Location.Column && hwordData.Location.Column + (hwordData.Letters.Length - 1) >= wordData.Location.Column)
                    {
                        if (hwordData.Location.Column + (hwordData.Letters.Length - 1) == wordData.Location.Column)
                        {
                            Boolean valid = false;

                            foreach (WordData vWordData in VerticalSequences)
                                if (vWordData.Location.Column == wordData.Location.Column)
                                    if (Math.Min(hwordData.Location.Row, wordData.Location.Row) >= vWordData.Location.Row &&
                                        Math.Max(hwordData.Location.Row, wordData.Location.Row) <= vWordData.Location.Row + (vWordData.Letters.Length - 1))
                                        valid = true;

                            if (!valid)
                                GridWordDataErrors = true;
                        }
                        else
                            GridWordDataErrors = true;
                    }
                    else if (hwordData.Location.Column >= wordData.Location.Column && hwordData.Location.Column <= wordData.Location.Column + (wordData.Letters.Length - 1))
                    {
                        if (wordData.Location.Column + (wordData.Letters.Length - 1) == hwordData.Location.Column)
                        {
                            Boolean valid = false;

                            foreach (WordData vWordData in VerticalSequences)
                                if (vWordData.Location.Column == hwordData.Location.Column)
                                    if (Math.Min(hwordData.Location.Row, wordData.Location.Row) >= vWordData.Location.Row &&
                                        Math.Max(hwordData.Location.Row, wordData.Location.Row) <= vWordData.Location.Row + (vWordData.Letters.Length - 1))
                                        valid = true;

                            if (!valid)
                                GridWordDataErrors = true;
                        }
                        else
                            GridWordDataErrors = true;
                    }
                }
            }

            foreach(WordData vwordData in VerticalSequences)
            {
                if (wordData.Location.Row >= vwordData.Location.Row && wordData.Location.Row <= vwordData.Location.Row + (vwordData.Letters.Length - 1))
                {
                    if (wordData.Location.Column + (wordData.Letters.Length - 1) == vwordData.Location.Column - 1 || wordData.Location.Column == vwordData.Location.Column + 1)
                        GridWordDataErrors = true;
                }
                else if (wordData.Location.Row == vwordData.Location.Row - 1 || wordData.Location.Row == vwordData.Location.Row + vwordData.Letters.Length)
                {
                    if (vwordData.Location.Column >= wordData.Location.Column && vwordData.Location.Column <= wordData.Location.Column + (wordData.Letters.Length - 1))
                        GridWordDataErrors = true;
                }
            }
        }

        private void CheckTouchingVerticalWordData(WordData wordData)
        {
            foreach(WordData vwordData in VerticalSequences)
            {
                if (vwordData.Location.Column == wordData.Location.Column)
                {
                    if (vwordData.Location.Row < wordData.Location.Row - 1 && vwordData.Location.Row + vwordData.Letters.Length >= wordData.Location.Row)
                        GridWordDataErrors = true;
                    else if (vwordData.Location.Row >= wordData.Location.Row - 1 && vwordData.Location.Row <= wordData.Location.Row + wordData.Letters.Length)
                        GridWordDataErrors = true;
                }
                else if (vwordData.Location.Column == wordData.Location.Column - 1 || vwordData.Location.Column == wordData.Location.Column + 1)
                {
                    if (vwordData.Location.Row < wordData.Location.Row && vwordData.Location.Row + (vwordData.Letters.Length - 1) >= wordData.Location.Row)
                    {
                        if (vwordData.Location.Row + (vwordData.Letters.Length - 1) == wordData.Location.Row)
                        {
                            Boolean valid = false;

                            foreach (WordData hWordData in HorizontalSequences)
                                if (hWordData.Location.Row == wordData.Location.Row)
                                    if (Math.Min(vwordData.Location.Column, wordData.Location.Column) >= hWordData.Location.Column &&
                                        Math.Max(vwordData.Location.Column, wordData.Location.Column) <= hWordData.Location.Column + (hWordData.Letters.Length - 1))
                                        valid = true;

                            if (!valid)
                                GridWordDataErrors = true;
                        }
                        else
                            GridWordDataErrors = true;
                    }
                    else if (vwordData.Location.Row >= wordData.Location.Row && vwordData.Location.Row <= wordData.Location.Row + (wordData.Letters.Length - 1))
                    {
                        if (wordData.Location.Row + (wordData.Letters.Length - 1) == vwordData.Location.Row)
                        {
                            Boolean valid = false;

                            foreach (WordData hWordData in HorizontalSequences)
                                if (hWordData.Location.Row == vwordData.Location.Row)
                                    if (Math.Min(vwordData.Location.Column, wordData.Location.Column) >= hWordData.Location.Column &&
                                        Math.Max(vwordData.Location.Column, wordData.Location.Column) <= hWordData.Location.Column + (hWordData.Letters.Length - 1))
                                        valid = true;

                            if (!valid)
                                GridWordDataErrors = true;
                        }
                        else
                            GridWordDataErrors = true;
                    }
                }
            }

            foreach (WordData hwordData in HorizontalSequences)
            {
                if (wordData.Location.Column >= hwordData.Location.Column && wordData.Location.Column <= hwordData.Location.Column + (hwordData.Letters.Length - 1))
                {
                    if (wordData.Location.Row + (wordData.Letters.Length - 1) == hwordData.Location.Row - 1 || wordData.Location.Row == hwordData.Location.Row + 1)
                        GridWordDataErrors = true;
                }
                else if(wordData.Location.Column == hwordData.Location.Column - 1 || wordData.Location.Column == hwordData.Location.Column + hwordData.Letters.Length)
                {
                    if (hwordData.Location.Row >= wordData.Location.Row && hwordData.Location.Row <= wordData.Location.Row + (wordData.Letters.Length - 1))
                        GridWordDataErrors = true;
                }
            }
        }

        public void CheckTouchingWords()
        {
            CheckTouchingHorizontalWords();
            CheckTouchingVerticalWords();          
        }
        
        private void CheckTouchingHorizontalWords()
        {
            WordData sequence1;
            WordData sequence2;

            for (int i = 0; i < HorizontalSequences.Count; i++)
            {
                sequence1 = HorizontalSequences[i];

                for (int j = i + 1; j < HorizontalSequences.Count; j++)
                {
                    sequence2 = HorizontalSequences[j];

                    if (sequence1.Letters.Equals(sequence2.Letters, StringComparison.Ordinal))
                        continue;

                    if (sequence2.Location.Row >= sequence1.Location.Row - 1 && sequence2.Location.Row <= sequence1.Location.Row + 1)
                    {
                        if (sequence2.Location.Column < sequence1.Location.Column - 1 && sequence2.Location.Column + sequence2.Letters.Length >= sequence1.Location.Column)
                            ErrorMessages.Add("the horizontal word \"" + sequence1.Letters + "\" on row " + sequence1.Location.Row + " is touching the horizontal word \"" + sequence2.Letters + "\" on row " + sequence2.Location.Row);
                        else if (sequence2.Location.Column >= sequence1.Location.Column - 1 && sequence2.Location.Column <= sequence1.Location.Column + sequence1.Letters.Length)
                            ErrorMessages.Add("the horizontal word \"" + sequence1.Letters + "\" on row " + sequence1.Location.Row + " is touching the horizontal word \"" + sequence2.Letters + "\" on row " + sequence2.Location.Row);
                    }
                }
            }
        }

        private void CheckTouchingVerticalWords()
        {
            WordData sequence1;
            WordData sequence2;

            for (int i = 0; i < VerticalSequences.Count; i++)
            {
                sequence1 = VerticalSequences[i];

                for (int j = i + 1; j < VerticalSequences.Count; j++)
                {
                    sequence2 = VerticalSequences[j];

                    if (sequence1.Letters.Equals(sequence2.Letters, StringComparison.Ordinal))
                        continue;

                    if (sequence2.Location.Column >= sequence1.Location.Column - 1 && sequence2.Location.Column <= sequence1.Location.Column + 1)
                    {
                        if (sequence2.Location.Row < sequence1.Location.Row - 1 && sequence2.Location.Row + sequence2.Letters.Length >= sequence1.Location.Row)
                            ErrorMessages.Add("the vertical word \"" + sequence1.Letters + "\" on column " + sequence1.Location.Column + " is touching the vertical word \"" + sequence2.Letters + "\" on column " + sequence2.Location.Column);
                        else if (sequence2.Location.Row >= sequence1.Location.Row - 1 && sequence2.Location.Row <= sequence1.Location.Row + sequence1.Letters.Length)
                            ErrorMessages.Add("the vertical word \"" + sequence1.Letters + "\" on column " + sequence1.Location.Column + " is touching the vertical word \"" + sequence2.Letters + "\" on column " + sequence2.Location.Column);
                    }
                }
            }
        }
        #endregion

        #region check overlapping words
        public void CheckOverlappingWords(WordData wordData)
        {
            if(wordData.IsHorizontal)
                CheckOverlappingHorizontalWords(wordData);
            else
                CheckOverlappingVerticalWords(wordData);
        }

        private void CheckOverlappingHorizontalWords(WordData wordData)
        {
            foreach(WordData hwordData in HorizontalSequences)
            {
                if (wordData.Location.Row == hwordData.Location.Row)
                {
                    if (wordData.Location.Column + (wordData.Letters.Length - 1) >= hwordData.Location.Column || hwordData.Location.Column + (hwordData.Letters.Length - 1) >= wordData.Location.Column)
                        GridWordDataErrors = true;
                }
            }
        }

        private void CheckOverlappingVerticalWords(WordData wordData)
        {
            foreach (WordData vwordData in VerticalSequences)
            {
                if (wordData.Location.Column == vwordData.Location.Column)
                {
                    if (wordData.Location.Row + (wordData.Letters.Length - 1) >= vwordData.Location.Row)
                        GridWordDataErrors = true;
                    else if (vwordData.Location.Row + (vwordData.Letters.Length - 1) >= wordData.Location.Row)
                        GridWordDataErrors = true;
                }
            }
        }
        #endregion

        #region word-group connectivity
        public void CheckConnectivity(int lowerLimit, int upperLimit, List<String[]> crozzleRows, List<String[]> crozzleColumns)
        {
            CrozzleMap map = new CrozzleMap(crozzleRows, crozzleColumns);
            int count = map.GroupCount();

            // Check whether the number of groups is within the limit.
            if (count < lowerLimit || count > upperLimit)
                ErrorMessages.Add(String.Format(CrozzleErrors.ConnectivityError, count, lowerLimit, upperLimit));
        }

        public void CheckGroupCount(List<String[]> gridRows, List<String[]> gridColumns, WordData wordData)
        {
            CrozzleMap map = new CrozzleMap(gridRows, gridColumns, wordData);
            int count = map.GroupCount();

            if (count < Configuration.MinimumNumberOfGroups || count > Configuration.MaximumNumberOfGroups)
                GridWordDataErrors = true;
        }
        #endregion
    }
}