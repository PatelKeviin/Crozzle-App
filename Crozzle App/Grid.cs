using System;
using System.Collections.Generic;

namespace CrozzleApplication
{
    class Grid
    {
        #region properties
        public int Rows { get; private set; }
        public int Columns { get; private set; }

        private Configuration Configuration { get; set; }
        public List<String[]> GridRows { get; set; }
        public List<String[]> GridColumns { get; set; }
        public CrozzleSequences GridSequences { get; set; }

        public List<WordData> WordDataList { get; set; }
        public List<WordData> HorizontalWordDataList { get; set; }
        public List<WordData> VerticalWordDataList { get; set; }
        #endregion

        #region constructors
        public Grid(int rows, int columns, Configuration configuration)
        {
            Rows = rows;
            Columns = columns;
            EmptyGrid(configuration);

            Configuration = configuration;
        }
        #endregion

        #region copy constructor
        public Grid(Grid grid)
        {
            Rows = grid.Rows;
            Columns = grid.Columns;

            // copy grid rows
            List<String[]> copyGridRows = new List<String[]>();
            for(int i = 0; i < grid.GridRows.Count; i++)
            {
                String[] copyStringArray = new String[grid.GridRows[i].Length];
                Array.Copy(grid.GridRows[i], copyStringArray, grid.GridRows[i].Length);
                copyGridRows.Add(copyStringArray);
            }
            GridRows = copyGridRows;

            // copy grid columns
            List<String[]> copyGridColumns = new List<String[]>();
            for (int i = 0; i < grid.GridColumns.Count; i++)
            {
                String[] copyStringArray = new String[grid.GridColumns[i].Length];
                Array.Copy(grid.GridColumns[i], copyStringArray, grid.GridColumns[i].Length);
                copyGridColumns.Add(copyStringArray);
            }
            GridColumns = copyGridColumns;

            // copy grid sequences
            GridSequences = new CrozzleSequences(grid.GridSequences);

            // copy WordDataList
            List<WordData> copyWordDataList = new List<WordData>();
            foreach (WordData wordData in grid.WordDataList)
                copyWordDataList.Add(new WordData(wordData));
            WordDataList = copyWordDataList;

            // copy HorizontalWordDataList
            List<WordData> copyHorizontalWordDataList = new List<WordData>();
            foreach (WordData wordData in grid.HorizontalWordDataList)
                copyHorizontalWordDataList.Add(new WordData(wordData));
            HorizontalWordDataList = copyHorizontalWordDataList;

            // copy VerticalWordDataList
            List<WordData> copyVerticalWordDataList = new List<WordData>();
            foreach (WordData wordData in grid.VerticalWordDataList)
                copyVerticalWordDataList.Add(new WordData(wordData));
            VerticalWordDataList = copyVerticalWordDataList;

            // shallow copying Configuration (as it is only used for readOnly purposes)
            Configuration = grid.Configuration;
        }
        #endregion

        #region create an empty Grid
        private void EmptyGrid(Configuration configuration)
        {
            // Create a List to store String arrays, one String[] for each row, one String for each letter.
            GridRows = new List<String[]>();
            // Create and store empty rows into the list.
            for (int i = 0; i < Rows; i++)
            {
                String[] row = new String[Columns];
                for (int j = 0; j < row.Length; j++)
                    row[j] = " ";
                GridRows.Add(row);
            }

            // Create a List to store String arrays, one String[] for each column, one String for each letter.
            GridColumns = new List<String[]>();
            // Create and store empty columns into the list.
            for (int i = 0; i < Columns; i++)
            {
                String[] column = new String[Rows];
                for (int j = 0; j < column.Length; j++)
                    column[j] = " ";
                GridColumns.Add(column);
            }

            WordDataList = new List<WordData>();
            GridSequences = new CrozzleSequences(GridRows, GridColumns, configuration);
            HorizontalWordDataList = new List<WordData>();
            VerticalWordDataList = new List<WordData>();
        }
        #endregion

        #region insert wordData into Grid
        public void Insert(WordData wordData)
        {
            if (wordData.Location.Row >= 1 && wordData.Location.Row <= Rows &&
                    wordData.Location.Column >= 1 && wordData.Location.Column <= Columns)
            {
                if (wordData.Orientation.Direction == Orientation.Row)
                {
                    // Store the letter into the approriate row.
                    String[] row = GridRows[wordData.Location.Row - 1];
                    int col = wordData.Location.Column - 1;
                    foreach (Char c in wordData.Letters)
                        if (col < Columns)
                            row[col++] = new String(c, 1);

                    // Store each letter into the ith column, but the same row location.
                    int j = wordData.Location.Column - 1;
                    foreach (Char c in wordData.Letters)
                        if (j < Columns)
                        {
                            String[] column = GridColumns[j];
                            column[wordData.Location.Row - 1] = new String(c, 1);
                            j++;
                        }

                    HorizontalWordDataList.Add(wordData);
                }
                else
                {
                    // Store the letter into the ith row, but the same column location.
                    int j = wordData.Location.Row - 1;
                    foreach (Char c in wordData.Letters)
                        if (j < Rows)
                        {
                            String[] currentRow = GridRows[j];
                            currentRow[wordData.Location.Column - 1] = new String(c, 1);
                            j++;
                        }

                    // Store each letter into the approriate column.
                    String[] column = GridColumns[wordData.Location.Column - 1];
                    int row = wordData.Location.Row - 1;
                    foreach (Char c in wordData.Letters)
                        if (row < Rows)
                            column[row++] = new String(c, 1);

                    VerticalWordDataList.Add(wordData);
                }

                GridSequences = new CrozzleSequences(GridRows, GridColumns, Configuration);
                WordDataList.Add(wordData);
            }
        }
        #endregion

        #region check for the validity of the Grid, after inserting the word data
        public Boolean IsValid(WordData wordData)
        {
            GridSequences.GridWordDataErrorsDetected = false;

            // Check that if the word data is not overlapping with opposite oriented words
            if (wordData.Orientation.IsHorizontal)
                GridSequences.CheckVerticalIntersectionOverlapping(wordData);
            else
                GridSequences.CheckHorizontalIntersectionOverlapping(wordData);

            // Check that if the word data is not overlapping with words having same orientation AND
            // Check that if the word data is not touching other words havings same/opposite orientation
            if(GridSequences.GridWordDataErrorsDetected == false)
                GridSequences.CheckTouchingWords(wordData);

            // Check word group count
            if (GridSequences.GridWordDataErrorsDetected == false)
                GridSequences.CheckGroupCount(GridRows, GridColumns, wordData);

            // If the grid is valid?
            if (GridSequences.GridWordDataErrorsDetected)
                return false;
            else
                return true;
        }
        #endregion

        #region score
        public int GridScore()
        {
            int score = 0;

            if(GridSequences != null)
            {
                // Increase the score for each word.
                score += GridSequences.Count * Configuration.PointsPerWord;

                // Increase the score for intersecting letters.
                List<Char> intersectingLetters = GridSequences.GetIntersectingLetters();
                foreach (Char letter in intersectingLetters)
                    score += Configuration.IntersectingPointsPerLetter[(int)letter - (int)'A'];

                // Get all letters.
                List<Char> allLetters = new List<Char>();

                foreach (String[] letters in GridRows)
                    foreach (String letter in letters)
                        if (letter[0] != ' ')
                            allLetters.Add(letter[0]);

                // Remove each intersecting letter from allLetters.
                List<Char> nonIntersectingLetters = allLetters;
                foreach (Char letter in intersectingLetters)
                    nonIntersectingLetters.Remove(letter);

                // Increase the score for non-intersecting letters.
                foreach (Char letter in nonIntersectingLetters)
                    score += Configuration.NonIntersectingPointsPerLetter[(int)letter - (int)'A'];
            }

            return score;
        }
        #endregion

        #region copy grid
        public Grid Copy()
        {
            return new Grid(this);
        }
        #endregion
    }
}
