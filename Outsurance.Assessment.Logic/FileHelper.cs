using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualBasic.FileIO;
using Outsurance.Assessment.Logic.Models;

namespace Outsurance.Assessment.Logic
{
    public class FileHelper
    {
        #region ctor and private members
        public string csvfile { get; set; }
        private const string _Names = "Names";
        private const string _AddressInfo = "AddressInfo";
        private const string _txt = ".txt";
        private const string _dateformat = "yyyy-MM-dd-hh-mm-ss";
        private const string _csvfilenameempty = "CSV filename is empty!";
        private const string _csvfilenameinvalid = "CSV file does not exist!";
        private const string _nocontactsfound = "No contacts have been found!";
        private const string _invalidcolumnsinthecsvfile = "Invalid columns in the csv file!";


        public FileHelper(string pcsvfile)
        {
            this.csvfile = pcsvfile;
            this.ValidateCSVFile();
        }
        #endregion

        #region Properties
        public List<Contact> contactlist { get; set; }
        public string NamesFile { get; set; }
        public string AddressInfoFile { get; set; }
        #endregion

        #region Public Methods
        public void LoadContactsFromCsv()
        {
            this.ValidateCSVFile();

            if (this.contactlist == null)
                this.contactlist = new List<Contact>();

            string filedatastring = File.ReadAllText(this.csvfile);
            StringBuilder fileData = new StringBuilder(filedatastring);
            List<List<string>> dataColumns = new List<List<string>>();
            using (TextReader reader = new StringReader(fileData.ToString()))
            {

                bool isHeaderRow = true;
                bool isValidColumns = false;
                using (TextFieldParser parser = new TextFieldParser(reader))
                {
                    parser.TextFieldType = FieldType.Delimited;
                    parser.SetDelimiters(",");
                    while (!parser.EndOfData)
                    {
                        if (isHeaderRow)
                        {
                            isHeaderRow = false;
                            var headers = parser.ReadFields();
                            if (!(headers.Length == 4 
                                && headers[0] == "FirstName"
                                && headers[1] == "LastName"
                                && headers[2] == "Address"
                                && headers[3] == "PhoneNumber"))
                            {
                                throw new Exception(_invalidcolumnsinthecsvfile);
                            }


                        }
                        else
                        {
                            string[] fields = parser.ReadFields();
                            if (fields != null && fields.Length == 4)
                            {
                                contactlist.Add(new Contact(){
                                    FirstName = fields[0],
                                    LastName = fields[1],
                                    Address = fields[2],
                                    PhoneNumber = fields[3]
                                });
                            }
                        }
                    }
                }
            }
        }

        public void WriteNamesToTextFile()
        {
            this.ValidateContacts();

            //Get firstnames and lastnames in same list 
            List<string> namelist = new List<string>();
            foreach (var contact in this.contactlist)
            {
                namelist.Add(contact.FirstName);
                namelist.Add(contact.LastName);
            }

            //group, sort and then write to file 
            this.NamesFile = _Names + DateTime.Now.ToString(_dateformat) + _txt;
            using (StreamWriter file = new StreamWriter(this.NamesFile))
            {
                var groupedlist = namelist.GroupBy(x => x);
                foreach (var group in groupedlist.OrderByDescending(y => y.Count()).ThenBy(z => z.Key))
                {
                    file.WriteLine("{0}, {1}", group.Key, group.Count());
                }
            }
        }

        public void WriteAddressInfoToTextFile()
        {
            this.ValidateContacts();

            //Get adress info in same list with number seperated from street part
            List<string> addresslist = new List<string>();
            string tempstring;
            foreach (var contact in this.contactlist)
            {
                tempstring = contact.Address.Substring(contact.Address.IndexOf(' ') + 1);
                addresslist.Add(tempstring);
            }

            //sort and then write to file 
            this.AddressInfoFile = _AddressInfo + DateTime.Now.ToString(_dateformat) + _txt;
            Contact tempcontact;
            using (StreamWriter file = new StreamWriter(this.AddressInfoFile))
            {
                foreach (var address in addresslist.OrderBy(x => x))
                {
                    tempcontact = this.contactlist.FirstOrDefault(y => y.Address.Contains(address));
                    file.WriteLine(tempcontact.Address);
                }
            }
        }
        #endregion

        #region Validations
        private void ValidateContacts()
        {
            if (this.contactlist == null || this.contactlist.Count == 0)
                throw new Exception(_nocontactsfound);
        }

        private void ValidateCSVFile()
        {
            if (string.IsNullOrEmpty(this.csvfile))
                throw new Exception(_csvfilenameempty);
            else if (!File.Exists(this.csvfile))
                throw new Exception(_csvfilenameinvalid);
        }

        private void ValidateCSVStructure()
        {

        }
        #endregion
    }
}
