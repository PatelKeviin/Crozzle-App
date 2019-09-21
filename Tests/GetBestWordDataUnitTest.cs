using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CrozzleApplication;
using System.Collections.Generic;

namespace UnitTestProject
{
    [TestClass]
    public class GetBestWordDataUnitTest
    {
        [TestMethod]
        public void GetBestWordDataTestMethod1()
        {
            #region Arrange
            // Unit Test 1
            // Arrange test data.
            int testRows1 = 1;
            int testColumns1 = 5;

            String configFileName1 = @"../../../../Test Files/Test S2T1 Configuration.txt";
            Configuration config1;
            Configuration.TryParse(configFileName1, out config1);

            Grid grid1 = new Grid(testRows1, testColumns1, config1);

            String wordlistFileName1 = @"../../../../Test Files/Test S2T1 Wordlist.txt";
            WordList sequence1;
            WordList.TryParse(wordlistFileName1, config1, out sequence1);

            WordDataList wordDataList1 = new WordDataList(testRows1, testColumns1, sequence1.List);

            // Arrange expected results.
            int expectedBestWordDataCount1 = 1;
            String expectedBestWordDataOrientation1_1 = Orientation.Row;
            int expectedBestWordDataRow1_1 = 1;
            int expectedBestWordDataColumn1_1 = 1;
            String expectedBestWordDataLetters1_1 = "ZZZZZ";

            // Unit Test 2
            // Arrange test data.
            int testRows2 = 1;
            int testColumns2 = 5;

            String configFileName2 = @"../../../../Test Files/Test S2T2 Configuration.txt";
            Configuration config2;
            Configuration.TryParse(configFileName2, out config2);

            Grid grid2 = new Grid(testRows2, testColumns2, config2);

            String wordlistFileName2 = @"../../../../Test Files/Test S2T2 Wordlist.txt";
            WordList sequence2;
            WordList.TryParse(wordlistFileName2, config2, out sequence2);

            WordDataList wordDataList2 = new WordDataList(testRows2, testColumns2, sequence2.List);

            // Arrange expected results.
            int expectedBestWordDataCount2 = 2;

            String expectedBestWordDataOrientation1_2 = Orientation.Row;
            int expectedBestWordDataRow1_2 = 1;
            int expectedBestWordDataColumn1_2 = 1;
            String expectedBestWordDataLetters1_2 = "ZZZZ";

            String expectedBestWordDataOrientation2_2 = Orientation.Row;
            int expectedBestWordDataRow2_2 = 1;
            int expectedBestWordDataColumn2_2 = 2;
            String expectedBestWordDataLetters2_2 = "ZZZZ";

            // Unit Test 3
            // Arrange test data.
            int testRows3 = 1;
            int testColumns3 = 5;

            String configFileName3 = @"../../../../Test Files/Test S2T3 Configuration.txt";
            Configuration config3;
            Configuration.TryParse(configFileName3, out config3);

            Grid grid3 = new Grid(testRows3, testColumns3, config3);

            String wordlistFileName3 = @"../../../../Test Files/Test S2T3 Wordlist.txt";
            WordList sequence3;
            WordList.TryParse(wordlistFileName3, config3, out sequence3);

            WordDataList wordDataList3 = new WordDataList(testRows3, testColumns3, sequence3.List);

            // Arrange expected results.
            int expectedBestWordDataCount3 = 3;

            String expectedBestWordDataOrientation1_3 = Orientation.Row;
            int expectedBestWordDataRow1_3 = 1;
            int expectedBestWordDataColumn1_3 = 1;
            String expectedBestWordDataLetters1_3 = "ZZZZZ";

            String expectedBestWordDataOrientation2_3 = Orientation.Row;
            int expectedBestWordDataRow2_3 = 1;
            int expectedBestWordDataColumn2_3 = 1;
            String expectedBestWordDataLetters2_3 = "AAAAA";

            String expectedBestWordDataOrientation3_3 = Orientation.Row;
            int expectedBestWordDataRow3_3 = 1;
            int expectedBestWordDataColumn3_3 = 1;
            String expectedBestWordDataLetters3_3 = "BBBBB";

            // Unit Test 4
            // Arrange test data.
            int testRows4 = 2;
            int testColumns4 = 5;

            String configFileName4 = @"../../../../Test Files/Test S2T4 Configuration.txt";
            Configuration config4;
            Configuration.TryParse(configFileName4, out config4);

            Grid grid4 = new Grid(testRows4, testColumns4, config4);

            String wordlistFileName4 = @"../../../../Test Files/Test S2T4 Wordlist.txt";
            WordList sequence4;
            WordList.TryParse(wordlistFileName4, config4, out sequence4);

            WordDataList wordDataList4 = new WordDataList(testRows4, testColumns4, sequence4.List);

            // Arrange expected results.
            int expectedBestWordDataCount4 = 4;

            String expectedBestWordDataOrientation1_4 = Orientation.Row;
            int expectedBestWordDataRow1_4 = 1;
            int expectedBestWordDataColumn1_4 = 1;
            String expectedBestWordDataLetters1_4 = "ZZZZZ";

            String expectedBestWordDataOrientation2_4 = Orientation.Row;
            int expectedBestWordDataRow2_4 = 2;
            int expectedBestWordDataColumn2_4 = 1;
            String expectedBestWordDataLetters2_4 = "ZZZZZ";

            String expectedBestWordDataOrientation3_4 = Orientation.Row;
            int expectedBestWordDataRow3_4 = 1;
            int expectedBestWordDataColumn3_4 = 1;
            String expectedBestWordDataLetters3_4 = "AAAAA";

            String expectedBestWordDataOrientation4_4 = Orientation.Row;
            int expectedBestWordDataRow4_4 = 2;
            int expectedBestWordDataColumn4_4 = 1;
            String expectedBestWordDataLetters4_4 = "AAAAA";
            #endregion

            #region Act
            // Unit Test 1
            List<WordData> bestWordData1 = Node.GetBestWordData(grid1, wordDataList1, config1);

            // Unit Test 2
            List<WordData> bestWordData2 = Node.GetBestWordData(grid2, wordDataList2, config2);

            // Unit Test 3
            List<WordData> bestWordData3 = Node.GetBestWordData(grid3, wordDataList3, config3);

            // Unit Test 4
            List<WordData> bestWordData4 = Node.GetBestWordData(grid4, wordDataList4, config4);
            #endregion

            #region Assert
            // Unit Test 1
            Assert.AreEqual(expectedBestWordDataCount1, bestWordData1.Count, "the number of \"best\" words retured by GetBestWordData() is incorrect in bestWordData1");

            Assert.AreEqual(expectedBestWordDataOrientation1_1, bestWordData1[0].Orientation.Direction, "orientation in WordData 1 in bestWordData1 is incorrect");
            Assert.AreEqual(expectedBestWordDataRow1_1, bestWordData1[0].Location.Row, "row in WordData 1 in bestWordData1 is incorrect");
            Assert.AreEqual(expectedBestWordDataColumn1_1, bestWordData1[0].Location.Column, "column in WordData 1 in bestWordData1 is incorrect");
            Assert.AreEqual(expectedBestWordDataLetters1_1, bestWordData1[0].Letters, "letters in WordData 1 in bestWordData1 is incorrect");

            // Unit Test 2
            Assert.AreEqual(expectedBestWordDataCount2, bestWordData2.Count, "the number of \"best\" words retured by GetBestWordData() is incorrect in bestWordData2");

            Assert.AreEqual(expectedBestWordDataOrientation1_2, bestWordData2[0].Orientation.Direction, "orientation in WordData 1 in bestWordData2 is incorrect");
            Assert.AreEqual(expectedBestWordDataRow1_2, bestWordData2[0].Location.Row, "row in WordData 1 in bestWordData2 is incorrect");
            Assert.AreEqual(expectedBestWordDataColumn1_2, bestWordData2[0].Location.Column, "column in WordData 1 in bestWordData2 is incorrect");
            Assert.AreEqual(expectedBestWordDataLetters1_2, bestWordData2[0].Letters, "letters in WordData 1 in bestWordData2 is incorrect");

            Assert.AreEqual(expectedBestWordDataOrientation2_2, bestWordData2[1].Orientation.Direction, "orientation in WordData 2 in bestWordData2 is incorrect");
            Assert.AreEqual(expectedBestWordDataRow2_2, bestWordData2[1].Location.Row, "row in WordData 2 in bestWordData2 is incorrect");
            Assert.AreEqual(expectedBestWordDataColumn2_2, bestWordData2[1].Location.Column, "column in WordData 2 in bestWordData2 is incorrect");
            Assert.AreEqual(expectedBestWordDataLetters2_2, bestWordData2[1].Letters, "letters in WordData 2 in bestWordData2 is incorrect");

            // Unit Test 3
            Assert.AreEqual(expectedBestWordDataCount3, bestWordData3.Count, "the number of \"best\" words retured by GetBestWordData() is incorrect in bestWordData3");

            Assert.AreEqual(expectedBestWordDataOrientation1_3, bestWordData3[0].Orientation.Direction, "orientation in WordData 1 in bestWordData3 is incorrect");
            Assert.AreEqual(expectedBestWordDataRow1_3, bestWordData3[0].Location.Row, "row in WordData 1 in bestWordData3 is incorrect");
            Assert.AreEqual(expectedBestWordDataColumn1_3, bestWordData3[0].Location.Column, "column in WordData 1 in bestWordData3 is incorrect");
            Assert.AreEqual(expectedBestWordDataLetters1_3, bestWordData3[0].Letters, "letters in WordData 1 in bestWordData3 is incorrect");

            Assert.AreEqual(expectedBestWordDataOrientation2_3, bestWordData3[1].Orientation.Direction, "orientation in WordData 2 in bestWordData3 is incorrect");
            Assert.AreEqual(expectedBestWordDataRow2_3, bestWordData3[1].Location.Row, "row in WordData 2 in bestWordData3 is incorrect");
            Assert.AreEqual(expectedBestWordDataColumn2_3, bestWordData3[1].Location.Column, "column in WordData 2 in bestWordData3 is incorrect");
            Assert.AreEqual(expectedBestWordDataLetters2_3, bestWordData3[1].Letters, "letters in WordData 2 in bestWordData3 is incorrect");

            Assert.AreEqual(expectedBestWordDataOrientation3_3, bestWordData3[2].Orientation.Direction, "orientation in WordData 3 in bestWordData3 is incorrect");
            Assert.AreEqual(expectedBestWordDataRow3_3, bestWordData3[2].Location.Row, "row in WordData 3 in bestWordData3 is incorrect");
            Assert.AreEqual(expectedBestWordDataColumn3_3, bestWordData3[2].Location.Column, "column in WordData 3 in bestWordData3 is incorrect");
            Assert.AreEqual(expectedBestWordDataLetters3_3, bestWordData3[2].Letters, "letters in WordData 3 in bestWordData3 is incorrect");

            // Unit Test 4
            Assert.AreEqual(expectedBestWordDataCount4, bestWordData4.Count, "the number of \"best\" words retured by GetBestWordData() is incorrect in bestWordData4");

            Assert.AreEqual(expectedBestWordDataOrientation1_4, bestWordData4[0].Orientation.Direction, "orientation in WordData 1 in bestWordData4 is incorrect");
            Assert.AreEqual(expectedBestWordDataRow1_4, bestWordData4[0].Location.Row, "row in WordData 1 in bestWordData4 is incorrect");
            Assert.AreEqual(expectedBestWordDataColumn1_4, bestWordData4[0].Location.Column, "column in WordData 1 in bestWordData4 is incorrect");
            Assert.AreEqual(expectedBestWordDataLetters1_4, bestWordData4[0].Letters, "letters in WordData 1 in bestWordData4 is incorrect");

            Assert.AreEqual(expectedBestWordDataOrientation2_4, bestWordData4[1].Orientation.Direction, "orientation in WordData 2 in bestWordData4 is incorrect");
            Assert.AreEqual(expectedBestWordDataRow2_4, bestWordData4[1].Location.Row, "row in WordData 2 in bestWordData4 is incorrect");
            Assert.AreEqual(expectedBestWordDataColumn2_4, bestWordData4[1].Location.Column, "column in WordData 2 in bestWordData4 is incorrect");
            Assert.AreEqual(expectedBestWordDataLetters2_4, bestWordData4[1].Letters, "letters in WordData 2 in bestWordData4 is incorrect");

            Assert.AreEqual(expectedBestWordDataOrientation3_4, bestWordData4[2].Orientation.Direction, "orientation in WordData 3 in bestWordData4 is incorrect");
            Assert.AreEqual(expectedBestWordDataRow3_4, bestWordData4[2].Location.Row, "row in WordData 3 in bestWordData4 is incorrect");
            Assert.AreEqual(expectedBestWordDataColumn3_4, bestWordData4[2].Location.Column, "column in WordData 3 in bestWordData4 is incorrect");
            Assert.AreEqual(expectedBestWordDataLetters3_4, bestWordData4[2].Letters, "letters in WordData 3 in bestWordData4 is incorrect");

            Assert.AreEqual(expectedBestWordDataOrientation4_4, bestWordData4[3].Orientation.Direction, "orientation in WordData 4 in bestWordData4 is incorrect");
            Assert.AreEqual(expectedBestWordDataRow4_4, bestWordData4[3].Location.Row, "row in WordData 4 in bestWordData4 is incorrect");
            Assert.AreEqual(expectedBestWordDataColumn4_4, bestWordData4[3].Location.Column, "column in WordData 4 in bestWordData4 is incorrect");
            Assert.AreEqual(expectedBestWordDataLetters4_4, bestWordData4[3].Letters, "letters in WordData 4 in bestWordData4 is incorrect");
            #endregion
        }

        [TestMethod]
        public void GetBestWordDataTestMethod2()
        {
            #region Arrange
            // Unit Test 1
            // Arrange test data.
            int testRows1 = 2;
            int testColumns1 = 3;

            String configFileName1 = @"../../../../Test Files/Test S3T1 Configuration.txt";
            Configuration config1;
            Configuration.TryParse(configFileName1, out config1);

            Grid grid1 = new Grid(testRows1, testColumns1, config1);
            WordData wordData1_1 = new WordData(Orientation.Row, 1, 1, "ACC", 1);
            grid1.Insert(wordData1_1);

            String wordlistFileName1 = @"../../../../Test Files/Test S3T1 Wordlist.txt";
            WordList sequence1;
            WordList.TryParse(wordlistFileName1, config1, out sequence1);

            WordDataList wordDataList1 = new WordDataList(testRows1, testColumns1, sequence1.List);
            wordDataList1.Remove(wordData1_1);

            // Arrange expected results.
            int expectedBestWordDataCount1 = 1;
            String expectedBestWordDataOrientation1 = Orientation.Column;
            int expectedBestWordDataRow1 = 1;
            int expectedBestWordDataColumn1 = 1;
            String expectedBestWordDataLetters1 = "AB";

            // Unit Test 2
            // Arrange test data.
            int testRows2 = 3;
            int testColumns2 = 3;

            String configFileName2 = @"../../../../Test Files/Test S3T2 Configuration.txt";
            Configuration config2;
            Configuration.TryParse(configFileName2, out config2);

            Grid grid2 = new Grid(testRows2, testColumns2, config2);
            WordData wordData1_2 = new WordData(Orientation.Row, 1, 1, "ACC", 1);
            grid2.Insert(wordData1_2);

            String wordlistFileName2 = @"../../../../Test Files/Test S3T2 Wordlist.txt";
            WordList sequence2;
            WordList.TryParse(wordlistFileName2, config2, out sequence2);

            WordDataList wordDataList2 = new WordDataList(testRows2, testColumns2, sequence2.List);
            wordDataList2.Remove(wordData1_2);

            // Arrange expected results.
            int expectedBestWordDataCount2 = 1;
            String expectedBestWordDataOrientation2 = Orientation.Column;
            int expectedBestWordDataRow2 = 1;
            int expectedBestWordDataColumn2 = 1;
            String expectedBestWordDataLetters2 = "AB";

            // Unit Test 3
            // Arrange test data.
            int testRows3 = 3;
            int testColumns3 = 3;

            String configFileName3 = @"../../../../Test Files/Test S3T3 Configuration.txt";
            Configuration config3;
            Configuration.TryParse(configFileName3, out config3);

            Grid grid3 = new Grid(testRows3, testColumns3, config3);
            WordData wordData1_3 = new WordData(Orientation.Row, 1, 1, "ACC", 1);
            WordData wordData2_3 = new WordData(Orientation.Column, 1, 3, "CCZ", 3);
            WordData wordData3_3 = new WordData(Orientation.Row, 1, 1, "ZZZ", 2);
            grid3.Insert(wordData1_3);
            grid3.Insert(wordData2_3);
            grid3.Insert(wordData3_3);

            String wordlistFileName3 = @"../../../../Test Files/Test S3T3 Wordlist.txt";
            WordList sequence3;
            WordList.TryParse(wordlistFileName3, config3, out sequence3);

            WordDataList wordDataList3 = new WordDataList(testRows3, testColumns3, sequence3.List);
            wordDataList3.Remove(wordData1_3);
            wordDataList3.Remove(wordData2_3);
            wordDataList3.Remove(wordData3_3);

            // Arrange expected results.
            int expectedBestWordDataCount3 = 0;

            // Unit Test 4
            // Arrange test data.
            int testRows4 = 2;
            int testColumns4 = 4;

            String configFileName4 = @"../../../../Test Files/Test S3T4 Configuration.txt";
            Configuration config4;
            Configuration.TryParse(configFileName4, out config4);

            Grid grid4 = new Grid(testRows4, testColumns4, config4);
            WordData wordData1_4 = new WordData(Orientation.Row, 2, 1, "VAZA", 1);
            grid4.Insert(wordData1_4);

            String wordlistFileName4 = @"../../../../Test Files/Test S3T4 Wordlist.txt";
            WordList sequence4;
            WordList.TryParse(wordlistFileName4, config4, out sequence4);

            WordDataList wordDataList4 = new WordDataList(testRows4, testColumns4, sequence4.List);
            wordDataList4.Remove(wordData1_4);

            // Arrange expected results.
            int expectedBestWordDataCount4 = 1;
            String expectedBestWordDataOrientation4 = Orientation.Column;
            int expectedBestWordDataRow4 = 1;
            int expectedBestWordDataColumn4 = 3;
            String expectedBestWordDataLetters4 = "AZ";
            #endregion

            #region Act
            // Unit Test 1
            List<WordData> bestWordData1 = Node.GetBestWordData(grid1, wordDataList1, config1);

            // Unit Test 2
            List<WordData> bestWordData2 = Node.GetBestWordData(grid2, wordDataList2, config2);

            // Unit Test 3
            List<WordData> bestWordData3 = Node.GetBestWordData(grid3, wordDataList3, config3);

            // Unit Test 4
            List<WordData> bestWordData4 = Node.GetBestWordData(grid4, wordDataList4, config4);
            #endregion

            #region Assert
            // Unit Test 1
            Assert.AreEqual(expectedBestWordDataCount1, bestWordData1.Count, "the number of \"best\" words retured by GetBestWordData() is incorrect in bestWordData1");
            Assert.AreEqual(expectedBestWordDataOrientation1, bestWordData1[0].Orientation.Direction, "orientation in WordData 1 in bestWordData1 is incorrect");
            Assert.AreEqual(expectedBestWordDataRow1, bestWordData1[0].Location.Row, "row in WordData 1 in bestWordData1 is incorrect");
            Assert.AreEqual(expectedBestWordDataColumn1, bestWordData1[0].Location.Column, "column in WordData 1 in bestWordData1 is incorrect");
            Assert.AreEqual(expectedBestWordDataLetters1, bestWordData1[0].Letters, "letters in WordData 1 in bestWordData1 is incorrect");

            // Unit Test 2
            Assert.AreEqual(expectedBestWordDataCount2, bestWordData2.Count, "the number of \"best\" words retured by GetBestWordData() is incorrect in bestWordData2");
            Assert.AreEqual(expectedBestWordDataOrientation2, bestWordData2[0].Orientation.Direction, "orientation in WordData 1 in bestWordData2 is incorrect");
            Assert.AreEqual(expectedBestWordDataRow2, bestWordData2[0].Location.Row, "row in WordData 1 in bestWordData2 is incorrect");
            Assert.AreEqual(expectedBestWordDataColumn2, bestWordData2[0].Location.Column, "column in WordData 1 in bestWordData2 is incorrect");
            Assert.AreEqual(expectedBestWordDataLetters2, bestWordData2[0].Letters, "letters in WordData 1 in bestWordData2 is incorrect");

            // Unit Test 3
            Assert.AreEqual(expectedBestWordDataCount3, bestWordData3.Count, "the number of \"best\" words retured by GetBestWordData() is incorrect in bestWordData3");

            // Unit Test 4
            Assert.AreEqual(expectedBestWordDataCount4, bestWordData4.Count, "the number of \"best\" words retured by GetBestWordData() is incorrect in bestWordData4");
            Assert.AreEqual(expectedBestWordDataOrientation4, bestWordData4[0].Orientation.Direction, "orientation in WordData 1 in bestWordData4 is incorrect");
            Assert.AreEqual(expectedBestWordDataRow4, bestWordData4[0].Location.Row, "row in WordData 1 in bestWordData4 is incorrect");
            Assert.AreEqual(expectedBestWordDataColumn4, bestWordData4[0].Location.Column, "column in WordData 1 in bestWordData4 is incorrect");
            Assert.AreEqual(expectedBestWordDataLetters4, bestWordData4[0].Letters, "letters in WordData 1 in bestWordData4 is incorrect");
            #endregion
        }
    }
}
