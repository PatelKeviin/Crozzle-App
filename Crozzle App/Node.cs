using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace CrozzleApplication
{
    class Node
    {
        #region constants
        private static readonly long RuntimeLimit = 385000L;     //in milliseconds
        #endregion region

        #region properties
        public Grid Grid { get; private set; }
        public WordDataList WordDataList { get; private set; }
        #endregion

        #region properties - greedy algorithm
        private static Node BestNode { get; set; }
        private static Node RootNode { get; set; }
        private static Stopwatch Stopwatch { get; set; }
        #endregion

        #region constructors
        public Node(int rows, int columns, Configuration configuration, List<String> originalWordsList)
        {
            Grid = new Grid(rows, columns, configuration);
            WordDataList = new WordDataList(rows, columns, originalWordsList);
        }
        #endregion

        #region copy constructor
        public Node(Node node)
        {
            Grid = node.Grid.Copy();
            WordDataList = node.WordDataList.Copy();
        }
        #endregion

        #region Greedy Algorithm
        public static Node GreedyAlgorithm(int rows, int columns, Configuration configuration, List<String> originalWordsList)
        {
            RootNode = new Node(rows, columns, configuration, originalWordsList);
            BestNode = RootNode.Copy();

            Stopwatch = new Stopwatch();
            Stopwatch.Start();
            InsertWords(RootNode, configuration);
            Stopwatch.Stop();

            Validate(BestNode, configuration);

            return (BestNode);
        }

        private static void InsertWords(Node node, Configuration configuration)
        {
            List<WordData> bestWordData = GetBestWordData(node.Grid, node.WordDataList, configuration);

            if(bestWordData.Count == 0)
            {
                if (node.Grid.GridScore() > BestNode.Grid.GridScore())
                    BestNode = node.Copy();
            }
            else
            {
                foreach(WordData wordData in bestWordData)
                {
                    if(Stopwatch.ElapsedMilliseconds >= RuntimeLimit)
                        break;

                    Node childNode = node.Copy();
                    childNode.Grid.Insert(wordData);
                    childNode.WordDataList.Remove(wordData);
                    InsertWords(childNode, configuration);
                }
            }
        }

        public static List<WordData> GetBestWordData(Grid grid, WordDataList wordDataList, Configuration configuration)
        {
            List<WordData> validWordDataList = new List<WordData>();

            foreach (WordData wordData in wordDataList.AllWordData)
                if (grid.IsValid(wordData))
                    validWordDataList.Add(wordData);

            foreach(WordData wordData in validWordDataList)
            {
                wordData.Score = 0;

                // Increase the score for the word.
                wordData.Score += configuration.PointsPerWord;

                // Increase the score for intersecting letters.
                List<Char> intersectingLetters = new List<Char>();
                if (wordData.IsHorizontal)
                    intersectingLetters = grid.GridSequences.GetVerticalIntersections(wordData);
                else
                    intersectingLetters = grid.GridSequences.GetHorizontalIntersections(wordData);
                foreach (Char letter in intersectingLetters)
                    wordData.Score += configuration.IntersectingPointsPerLetter[(int)letter - (int)'A'];

                // Get all letters from the word
                List<Char> allLetters = new List<Char>();
                foreach (Char letter in wordData.Letters)
                    allLetters.Add(letter);

                // Remove each intersecting letter from allLetters.
                List<Char> nonIntersectingLetters = allLetters;
                foreach (Char letter in intersectingLetters)
                    nonIntersectingLetters.Remove(letter);

                // Increase the score for non-intersecting letters.
                foreach (Char letter in nonIntersectingLetters)
                    wordData.Score += configuration.NonIntersectingPointsPerLetter[(int)letter - (int)'A'];
            }

            int maxScore = 0;

            foreach (WordData wordData in validWordDataList)
                if (wordData.Score > maxScore)
                    maxScore = wordData.Score;

            List<WordData> bestWordData = new List<WordData>();

            foreach (WordData wordData in validWordDataList)
                if (wordData.Score == maxScore)
                    bestWordData.Add(wordData);

            return bestWordData;
        }
        #endregion

        #region validate grid
        private static void Validate(Node bestNode, Configuration configuration)
        {
            List<Coordinate> intersectingLocations = new List<Coordinate>();
            List<WordData> toRemoveWordData = new List<WordData>();
            Boolean isInvalid = false;

            foreach(WordData wordData in bestNode.Grid.WordDataList)
            {
                if (wordData.IsHorizontal)
                {
                    intersectingLocations = bestNode.Grid.GridSequences.GetVerticalIntersectionLocations(wordData);
                    if (intersectingLocations.Count < configuration.MinimumIntersectionsInHorizontalWords ||
                        intersectingLocations.Count > configuration.MaximumIntersectionsInHorizontalWords)
                    {
                        isInvalid = true;
                        for(int column = wordData.Location.Column - 1; column < wordData.Location.Column + (wordData.Letters.Length - 1); column++)
                        {
                            bool removeLocation = true;
                            foreach (Coordinate coord in intersectingLocations)
                                if ((coord.Column - 1) == column)
                                    removeLocation = false;
                            if (removeLocation)
                            {
                                bestNode.Grid.GridRows[wordData.Location.Row - 1][column] = " ";
                                bestNode.Grid.GridColumns[column][wordData.Location.Row - 1] = " ";
                                bestNode.Grid.GridSequences = new CrozzleSequences(bestNode.Grid.GridRows, bestNode.Grid.GridColumns, configuration);
                                toRemoveWordData.Add(wordData);
                            }
                        }
                    }
                }
                else
                {
                    intersectingLocations = bestNode.Grid.GridSequences.GetHorizontalIntersectionLocations(wordData);
                    if (intersectingLocations.Count < configuration.MinimumIntersectionsInVerticalWords ||
                        intersectingLocations.Count > configuration.MaximumIntersectionsInVerticalWords)
                    {
                        isInvalid = true;
                        for (int row = wordData.Location.Row - 1; row < wordData.Location.Row + (wordData.Letters.Length - 1); row++)
                        {
                            bool removeLocation = true;
                            foreach (Coordinate coord in intersectingLocations)
                                if ((coord.Row - 1) == row)
                                    removeLocation = false;
                            if (removeLocation)
                            {
                                bestNode.Grid.GridRows[row][wordData.Location.Column - 1] = " ";
                                bestNode.Grid.GridColumns[wordData.Location.Column - 1][row] = " ";
                                bestNode.Grid.GridSequences = new CrozzleSequences(bestNode.Grid.GridRows, bestNode.Grid.GridColumns, configuration);
                                toRemoveWordData.Add(wordData);
                            }
                        }
                    }
                }
            }

            foreach (WordData wordData in toRemoveWordData)
                bestNode.Grid.WordDataList.Remove(wordData);

            if (isInvalid)
                Validate(bestNode, configuration);
        }
        #endregion

        #region copy node
        public Node Copy()
        {
            return new Node(this);
        }
        #endregion
    }
}
