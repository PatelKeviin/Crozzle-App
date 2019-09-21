using System;
using System.Collections.Generic;

namespace CrozzleApplication
{
    class WordDataList
    {
        #region properties
        public List<String> OriginalWordList { get; set; }
        public List<WordData> AllWordData { get; set; }
        public List<WordData> HorizontalWordData { get; set; }
        public List<WordData> VerticalWordData { get; set; }
        #endregion

        #region properties - counting
        public int Count
        {
            get
            {
                return (AllWordData.Count);
            }
        }

        public int HorizontalCount
        {
            get
            {
                return (HorizontalWordData.Count);
            }
        }

        public int VerticalCount
        {
            get
            {
                return (VerticalWordData.Count);
            }
        }
        #endregion

        #region constructor
        public WordDataList(int rows, int columns, List<String> originalWordList)
        {
            OriginalWordList = originalWordList;
            AllWordData = new List<WordData>();
            HorizontalWordData = new List<WordData>();
            VerticalWordData = new List<WordData>();

            int wordID = 0;
            foreach (String word in originalWordList)
            {
                // unique for each word in the sequence txt file
                ++wordID;

                // All horizontal word possibilities
                for(int row = 1; row < rows + 1; row++)
                    for(int column = 1; column < columns + 1; column++)
                        if(column + (word.Length - 1) <= columns)
                        {
                            WordData wordData = new WordData(Orientation.Row, row, column, word, wordID);
                            this.AllWordData.Add(wordData);
                            this.HorizontalWordData.Add(wordData);
                        }

                // All vertical word possibilities
                for (int column = 1; column < columns + 1; column++)
                    for (int row = 1; row < rows + 1; row++)
                        if (row + (word.Length - 1) <= rows)
                        {
                            WordData wordData = new WordData(Orientation.Column, row, column, word, wordID);
                            this.AllWordData.Add(wordData);
                            this.VerticalWordData.Add(wordData);
                        }
            }
        }
        #endregion

        #region copy constructor
        public WordDataList(WordDataList wordDataList)
        {
            // copy OriginalWordList
            List<String> copyOriginalWordList = new List<String>();
            foreach (String word in wordDataList.OriginalWordList)
                copyOriginalWordList.Add(String.Copy(word));
            OriginalWordList = copyOriginalWordList;

            // copy AllWordData
            List<WordData> copyAllWordData = new List<WordData>();
            // copy HorizontalWordData
            List<WordData> copyHorizontalWordData = new List<WordData>();
            // copy VerticalWordData
            List<WordData> copyVerticalWordData = new List<WordData>();

            foreach (WordData wordData in wordDataList.AllWordData)
            {
                if(wordData.IsHorizontal)
                    copyHorizontalWordData.Add(new WordData(wordData));
                else
                    copyVerticalWordData.Add(new WordData(wordData));

                copyAllWordData.Add(new WordData(wordData));
            }
            AllWordData = copyAllWordData;
            HorizontalWordData = copyHorizontalWordData;
            VerticalWordData = copyVerticalWordData;
        }
        #endregion

        #region copy word data list
        public WordDataList Copy()
        {
            return new WordDataList(this);
        }
        #endregion

        #region remove all word data belonging to the given sequence
        public void Remove(WordData wordData)
        {
            int startIndexAllWordData = 0;
            bool assigned1 = false;
            int countAllWordData = 0;

            for (int i = 0; i < AllWordData.Count; i++)
            {
                if(AllWordData[i].WordID == wordData.WordID)
                {
                    if (!assigned1)
                    {
                        startIndexAllWordData = i;
                        assigned1 = true;
                    }
                    countAllWordData++;
                }
            }

            int startIndexHorizontalWordData = 0;
            bool assigned2 = false;
            int countHorizontalWordData = 0;

            for (int i = 0; i < HorizontalWordData.Count; i++)
            {
                if (HorizontalWordData[i].WordID == wordData.WordID)
                {
                    if (!assigned2)
                    {
                        startIndexHorizontalWordData = i;
                        assigned2 = true;
                    }
                    countHorizontalWordData++;
                }
            }

            int startIndexVerticalWordData = 0;
            bool assigned3 = false;
            int countVerticalWordData = 0;

            for (int i = 0; i < VerticalWordData.Count; i++)
            {
                if (VerticalWordData[i].WordID == wordData.WordID)
                {
                    if (!assigned3)
                    {
                        startIndexVerticalWordData = i;
                        assigned3 = true;
                    }
                    countVerticalWordData++;
                }
            }

            AllWordData.RemoveRange(startIndexAllWordData, countAllWordData);
            HorizontalWordData.RemoveRange(startIndexHorizontalWordData, countHorizontalWordData);
            VerticalWordData.RemoveRange(startIndexVerticalWordData, countVerticalWordData);
        }
        #endregion
    }
}