using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace CrozzleApplication
{
    class WordData
    {
        #region constants
        public const String OrientationRow = "ROW";
        public const String OrientationColumn = "COLUMN";
        #endregion

        #region properties
        private String[] OriginalWordData { get; set; }
        public Orientation Orientation { get; set; }
        public Coordinate Location { get; set; }
        public String Letters { get; set; }

        public int Score { get; set; }
        public int WordID { get; set; }     // required to find which sequence it belongs in the sequence txt file (useful for removing operations)
        #endregion

        #region properties - testing
        public Boolean IsHorizontal
        {
            get { return (Orientation.IsHorizontal); }
        }

        public Boolean IsVertical
        {
            get { return (Orientation.IsVertical); }
        }
        #endregion

        #region constructors
        public WordData(String direction, int row, int column, String sequence, int wordID = 0)
        {
            OriginalWordData = new String[] { direction, row.ToString(), column.ToString(), sequence};
            Orientation anOrientation;
            Orientation.TryParse(direction, out anOrientation);
            Orientation = anOrientation;
            Location = new Coordinate(row, column);
            Letters = sequence;
            Score = 0;
            WordID = wordID;
        }
        #endregion

        #region copy constructor
        public WordData(WordData wordData)
        {
            // copy OriginalWordData
            String[] copyOriginalWordData = new String[wordData.OriginalWordData.Length];
            Array.Copy(wordData.OriginalWordData, copyOriginalWordData, wordData.OriginalWordData.Length);
            OriginalWordData = copyOriginalWordData;
            // copy Orientation
            Orientation = new Orientation(wordData.Orientation);
            // copy Location
            Location = new Coordinate(wordData.Location);
            // copy Letters
            Letters = String.Copy(wordData.Letters);
            // copy Score
            Score = wordData.Score;
            // copy WordID
            WordID = wordData.WordID;
        }
        #endregion
    }
}