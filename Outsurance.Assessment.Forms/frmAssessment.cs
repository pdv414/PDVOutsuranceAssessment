using System;
using System.Diagnostics;
using System.Windows.Forms;
using Outsurance.Assessment.Logic;

namespace Outsurance.Assessment.Forms
{
    public partial class frmAssessment : Form
    {
        private const string _csvfilenotfound = "The CSV file could not be found.";
        private const string _notepad = "notepad.exe";

        public frmAssessment()
        {
            InitializeComponent();
        }

        private void btnLoadCSV_Click(object sender, EventArgs e)
        {
            if (opencsvfiledialog.ShowDialog() == DialogResult.OK)
            {
                if (!string.IsNullOrEmpty(opencsvfiledialog.FileName))
                {
                    this.ProcessTheCSVFile();
                }
                else
                    MessageBox.Show(_csvfilenotfound);
            }            
        }

        private void ProcessTheCSVFile()
        {
            try
            {
                //Call my class to read from csv and write new files.
                FileHelper helper = new FileHelper(opencsvfiledialog.FileName);
                helper.LoadContactsFromCsv();
                helper.WriteNamesToTextFile();
                helper.WriteAddressInfoToTextFile();
                Process.Start(_notepad, helper.NamesFile);
                Process.Start(_notepad, helper.AddressInfoFile);
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                if (ex.InnerException != null)
                    msg += ex.InnerException.Message;
                MessageBox.Show(msg);
            }
        }
    }
}
