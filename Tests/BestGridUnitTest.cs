using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CrozzleApplication;
using System.Collections.Generic;

namespace UnitTestProject
{
    [TestClass]
    public class BestGridUnitTest
    {
        [TestMethod]
        public void GreedyAlgorithmTestMethod()
        {
            #region Arrange
            // Unit Test 1
            // Arrange test data.
            String configFileName1 = @"../../../../Test Files/Test S1T1 Configuration.txt";
            Configuration config1;
            Configuration.TryParse(configFileName1, out config1);

            String wordlistFileName1 = @"../../../../Test Files/Test S1T1 Wordlist.txt";
            WordList sequence1;
            WordList.TryParse(wordlistFileName1, config1, out sequence1);

            int testRows1 = 6;
            int testColumns1 = 8;

            // Arrange expected results.
            int expectedRows1 = 6;
            int expectedColumns1 = 8;
            String expectedRow1_1 = "TAAYAAAA";
            String expectedRow2_1 = "P  A    ";
            String expectedRow3_1 = "Y  A    ";
            String expectedRow4_1 = "   ZAPAJ";
            String expectedRow5_1 = " JAS    ";
            String expectedRow6_1 = "   K    ";

            // Unit Test 2
            // Arrange test data.
            String configFileName2 = @"../../../../Test Files/Test S1T2 Configuration.txt";
            Configuration config2;
            Configuration.TryParse(configFileName2, out config2);

            String wordlistFileName2 = @"../../../../Test Files/Test S1T2 Wordlist.txt";
            WordList sequence2;
            WordList.TryParse(wordlistFileName2, config2, out sequence2);

            int testRows2 = 9;
            int testColumns2 = 9;

            // Arrange expected results.
            int expectedRows2 = 9;
            int expectedColumns2 = 9;
            String expectedRow1_2 = "ZAAAAAAPA";
            String expectedRow2_2 = "A        ";
            String expectedRow3_2 = "A GHAT  A";
            String expectedRow4_2 = "A  A A  A";
            String expectedRow5_2 = "A   KL  A";
            String expectedRow6_2 = "A   B   A";
            String expectedRow7_2 = "XAAAMSJAQ";
            String expectedRow8_2 = "A    A   ";
            String expectedRow9_2 = "A    A   ";

            // Unit Test 3
            // Arrange test data.
            String configFileName3 = @"../../../../Test Files/Test S1T3 Configuration.txt";
            Configuration config3;
            Configuration.TryParse(configFileName3, out config3);

            String wordlistFileName3 = @"../../../../Test Files/Test S1T3 Wordlist.txt";
            WordList sequence3;
            WordList.TryParse(wordlistFileName3, config3, out sequence3);

            int testRows3 = 9;
            int testColumns3 = 14;

            // Arrange expected results.
            int expectedRows3 = 9;
            int expectedColumns3 = 14;
            String expectedRow1_3 = "AQAAAAAAAACAXA";
            String expectedRow2_3 = " A        A A ";
            String expectedRow3_3 = " A  LAAAI DAA ";
            String expectedRow4_3 = " A  A   A   A ";
            String expectedRow5_3 = " NR MS  A FAA ";
            String expectedRow6_3 = "  A  A  A A EA";
            String expectedRow7_3 = "  UAAT PH GAA ";
            String expectedRow8_3 = "       A    A ";
            String expectedRow9_3 = "       JAAAAK ";

            // Unit Test 4
            // Arrange test data.
            String configFileName4 = @"../../../../Test Files/Test S1T4 Configuration.txt";
            Configuration config4;
            Configuration.TryParse(configFileName4, out config4);

            String wordlistFileName4 = @"../../../../Test Files/Test S1T4 Wordlist.txt";
            WordList sequence4;
            WordList.TryParse(wordlistFileName4, config4, out sequence4);

            int testRows4 = 4;
            int testColumns4 = 4;

            // Arrange expected results.
            int expectedRows4 = 4;
            int expectedColumns4 = 4;
            String expectedRow1_4 = "ABCD";
            String expectedRow2_4 = "   A";
            String expectedRow3_4 = " L E";
            String expectedRow4_4 = "AVZZ";
            #endregion

            #region Act
            // Unit Test 1
            Grid grid1 = Node.GreedyAlgorithm(testRows1, testColumns1, config1, sequence1.List).Grid;

            // Unit Test 2
            Grid grid2 = Node.GreedyAlgorithm(testRows2, testColumns2, config2, sequence2.List).Grid;

            // Unit Test 3
            Grid grid3 = Node.GreedyAlgorithm(testRows3, testColumns3, config3, sequence3.List).Grid;

            // Unit Test 4
            Grid grid4 = Node.GreedyAlgorithm(testRows4, testColumns4, config4, sequence4.List).Grid;
            #endregion

            #region Assert
            // Unit Test 1
            Assert.AreEqual(expectedRows1, grid1.Rows, "the number of rows is incorrect in grid1");
            Assert.AreEqual(expectedColumns1, grid1.Columns, "the number of columns is incorrect in grid1");
            Assert.AreEqual(expectedRow1_1, GetRow(1, grid1.GridRows), "row 1 is incorrect in grid1");
            Assert.AreEqual(expectedRow2_1, GetRow(2, grid1.GridRows), "row 2 is incorrect in grid1");
            Assert.AreEqual(expectedRow3_1, GetRow(3, grid1.GridRows), "row 3 is incorrect in grid1");
            Assert.AreEqual(expectedRow4_1, GetRow(4, grid1.GridRows), "row 4 is incorrect in grid1");
            Assert.AreEqual(expectedRow5_1, GetRow(5, grid1.GridRows), "row 5 is incorrect in grid1");
            Assert.AreEqual(expectedRow6_1, GetRow(6, grid1.GridRows), "row 6 is incorrect in grid1");

            // Unit Test 1
            Assert.AreEqual(expectedRows2, grid2.Rows, "the number of rows is incorrect in grid2");
            Assert.AreEqual(expectedColumns2, grid2.Columns, "the number of columns is incorrect in grid2");
            Assert.AreEqual(expectedRow1_2, GetRow(1, grid2.GridRows), "row 1 is incorrect in grid2");
            Assert.AreEqual(expectedRow2_2, GetRow(2, grid2.GridRows), "row 2 is incorrect in grid2");
            Assert.AreEqual(expectedRow3_2, GetRow(3, grid2.GridRows), "row 3 is incorrect in grid2");
            Assert.AreEqual(expectedRow4_2, GetRow(4, grid2.GridRows), "row 4 is incorrect in grid2");
            Assert.AreEqual(expectedRow5_2, GetRow(5, grid2.GridRows), "row 5 is incorrect in grid2");
            Assert.AreEqual(expectedRow6_2, GetRow(6, grid2.GridRows), "row 6 is incorrect in grid2");
            Assert.AreEqual(expectedRow7_2, GetRow(7, grid2.GridRows), "row 7 is incorrect in grid2");
            Assert.AreEqual(expectedRow8_2, GetRow(8, grid2.GridRows), "row 8 is incorrect in grid2");
            Assert.AreEqual(expectedRow9_2, GetRow(9, grid2.GridRows), "row 9 is incorrect in grid2");

            // Unit Test 3
            Assert.AreEqual(expectedRows3, grid3.Rows, "the number of rows is incorrect in grid3");
            Assert.AreEqual(expectedColumns3, grid3.Columns, "the number of columns is incorrect in grid3");
            Assert.AreEqual(expectedRow1_3, GetRow(1, grid3.GridRows), "row 1 is incorrect in grid3");
            Assert.AreEqual(expectedRow2_3, GetRow(2, grid3.GridRows), "row 2 is incorrect in grid3");
            Assert.AreEqual(expectedRow3_3, GetRow(3, grid3.GridRows), "row 3 is incorrect in grid3");
            Assert.AreEqual(expectedRow4_3, GetRow(4, grid3.GridRows), "row 4 is incorrect in grid3");
            Assert.AreEqual(expectedRow5_3, GetRow(5, grid3.GridRows), "row 5 is incorrect in grid3");
            Assert.AreEqual(expectedRow6_3, GetRow(6, grid3.GridRows), "row 6 is incorrect in grid3");
            Assert.AreEqual(expectedRow7_3, GetRow(7, grid3.GridRows), "row 7 is incorrect in grid3");
            Assert.AreEqual(expectedRow8_3, GetRow(8, grid3.GridRows), "row 8 is incorrect in grid3");
            Assert.AreEqual(expectedRow9_3, GetRow(9, grid3.GridRows), "row 9 is incorrect in grid3");

            // Unit Test 4
            Assert.AreEqual(expectedRows4, grid4.Rows, "the number of rows is incorrect in grid4");
            Assert.AreEqual(expectedColumns4, grid4.Columns, "the number of columns is incorrect in grid4");
            Assert.AreEqual(expectedRow1_4, GetRow(1, grid4.GridRows), "row 1 is incorrect in grid4");
            Assert.AreEqual(expectedRow2_4, GetRow(2, grid4.GridRows), "row 2 is incorrect in grid4");
            Assert.AreEqual(expectedRow3_4, GetRow(3, grid4.GridRows), "row 3 is incorrect in grid4");
            Assert.AreEqual(expectedRow4_4, GetRow(4, grid4.GridRows), "row 4 is incorrect in grid4");
            #endregion
        }

        #region Auxiliary method
        private String GetRow(int rowNum, List<String[]> gridRows)
        {
            String row = "";
            foreach (String letter in gridRows[rowNum - 1])
                row += letter;

            return row;
        }
        #endregion
    }
}