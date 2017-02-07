using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Outsurance.Assessment.Logic;

namespace Outsurance.Assessment.UnitTests
{
    [TestClass]
    public class FileHelperTests
    {
        #region Test new instance
        [TestMethod]
        public void Instanciate_FileHelper_WithEmptyFile()
        {
            bool errorthrown = false;
            try
            {
                FileHelper helper = new FileHelper("");
            }
            catch (Exception ex)
            {
                errorthrown = true;
                Assert.AreEqual("CSV filename is empty!", ex.Message);
            }

            if (!errorthrown)
            { 
                Assert.Fail("A error must be thrown if class is instaciated with an invalid file");   
            }            
        }

        [TestMethod]
        public void Instanciate_FileHelper_WithInvalidFile()
        {
            bool errorthrown = false;
            try
            {
                FileHelper helper = new FileHelper("123456.bob");
            }
            catch (Exception ex)
            {
                errorthrown = true;
                Assert.AreEqual("CSV file does not exist!", ex.Message);
            }

            if (!errorthrown)
            {
                Assert.Fail("A error must be thrown if class is instaciated with an invalid file");
            }
        }
        #endregion

        #region Test reading from csv
        [TestMethod]
        public void LoadCSV_With_ValidColumns()
        {
            try
            {
                FileHelper helper = new FileHelper(@"..\..\testdata.csv");
                helper.LoadContactsFromCsv();
            }
            catch (Exception)
            {
                //Invalid columns in the csv file!
                Assert.Fail("Contacts could not load from csv!");
            }
        }

        [TestMethod]
        public void LoadCSV_With_InvalidColumns()
        {
            bool errorthrown = false;
            try
            {
                FileHelper helper = new FileHelper(@"..\..\wrongtestdata.csv");
                helper.LoadContactsFromCsv();
            }
            catch (Exception ex)
            {
                errorthrown = true;
                Assert.AreEqual("Invalid columns in the csv file!", ex.Message);
            }

            if (!errorthrown)
            {
                Assert.Fail("A error must be thrown if invalid columns are found");
            }
        }
        #endregion

        #region Writing names and address info to text file
        [TestMethod]
        public void Write_Names_With_ValidData()
        {
            try
            {
                FileHelper helper = new FileHelper(@"..\..\testdata.csv");
                helper.LoadContactsFromCsv();
                helper.WriteNamesToTextFile();
            }
            catch (Exception)
            {
                Assert.Fail("Contacts could not load from csv!");
            }
        }

        [TestMethod]
        public void Write_AddressInfo_With_ValidData()
        {
            try
            {
                FileHelper helper = new FileHelper(@"..\..\testdata.csv");
                helper.LoadContactsFromCsv();
                helper.WriteAddressInfoToTextFile();
            }
            catch (Exception)
            {
                Assert.Fail("Contacts could not load from csv!");
            }
        }

        [TestMethod]
        public void Write_Names_With_EmptyData()
        {
            bool errorthrown = false;
            try
            {
                FileHelper helper = new FileHelper(@"..\..\testdataempty.csv");
                helper.LoadContactsFromCsv();
                helper.WriteNamesToTextFile();
            }
            catch (Exception ex)
            {
                errorthrown = true;
                Assert.AreEqual("No contacts have been found!", ex.Message);
            }

            if (!errorthrown)
            {
                Assert.Fail("A error must be thrown if no contacts are found");
            }
        }

        [TestMethod]
        public void Write_AddressInfo_With_EmptyData()
        {
            bool errorthrown = false;
            try
            {
                FileHelper helper = new FileHelper(@"..\..\testdataempty.csv");
                helper.LoadContactsFromCsv();
                helper.WriteAddressInfoToTextFile();
            }
            catch (Exception ex)
            {
                errorthrown = true;
                Assert.AreEqual("No contacts have been found!", ex.Message);
            }

            if (!errorthrown)
            {
                Assert.Fail("A error must be thrown if no contacts are found");
            }
        }
        #endregion
    }
}
