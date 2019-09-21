using System;
using System.Windows.Forms;
using System.IO;
using System.Net;

namespace CrozzleApplication
{
    public partial class CrozzleViewerForm : Form
    {
        #region properties
        private Crozzle SIT323Crozzle { get; set; }
        private ErrorsViewer ErrorListViewer { get; set; }
        private AboutBox ApplicationAboutBox { get; set; }
        #endregion
        
        #region constructors
        public CrozzleViewerForm()
        {
            InitializeComponent();

            ApplicationAboutBox = new AboutBox();
            ErrorListViewer = new ErrorsViewer();
            ErrorListViewer.Text = ApplicationAboutBox.AssemblyTitle + " - " + ErrorListViewer.Text;
        }
        #endregion

        #region File menu event handlers
        private void openCrozzleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openCrozzleFile();
        }

        private void crozzleButton_Click(object sender, EventArgs e)
        {
            if (crozzleComboBox.Text == String.Empty)
            {
                MessageBox.Show("crozzle filename is missing in the combo box", ApplicationAboutBox.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Boolean itemPresent = false;
            foreach (String item in crozzleComboBox.Items)
                if (item == crozzleComboBox.Text)
                    itemPresent = true;

            if (!itemPresent)
                crozzleComboBox.Items.Add(crozzleComboBox.Text);

            openCrozzleFileRemotely();
        }

        private void openCrozzleFile()
        {
            DialogResult result;
            
            // As we are opening a crozzle file,
            // indicate crozzle file, and crozzle are not valid, and clear GUI.
            crozzleToolStripMenuItem.Enabled = false;
            crozzleWebBrowser.DocumentText = "";
            ErrorListViewer.WebBrowser.DocumentText = "";

            // Process crozzle file.
            result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                // Get configuration filename.
                String configurationFileName = GetConfigurationFileName(openFileDialog1.FileName);
                if (configurationFileName == null)
                {
                    MessageBox.Show("configuration filename is missing from the crozzle file", ApplicationAboutBox.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    String filename = configurationFileName.Trim();
                    if (Validator.IsDelimited(filename, Crozzle.StringDelimiters))
                        filename = filename.Trim(Crozzle.StringDelimiters);
                    configurationFileName = filename;

                    if (!Path.IsPathRooted(configurationFileName))
                        configurationFileName = Path.GetDirectoryName(openFileDialog1.FileName) + @"\" + configurationFileName;
                }

                // Parse configuration file.
                Configuration aConfiguration = null;
                Configuration.TryParse(configurationFileName, out aConfiguration);

                // Get wordlist filename.
                String wordListFileName = GetWordlistFileName(openFileDialog1.FileName);
                if (wordListFileName == null)
                {
                    MessageBox.Show("wordlist filename is missing from the crozzle file", ApplicationAboutBox.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    String filename = wordListFileName.Trim();
                    if (Validator.IsDelimited(filename, Crozzle.StringDelimiters))
                        filename = filename.Trim(Crozzle.StringDelimiters);
                    wordListFileName = filename;

                    if (!Path.IsPathRooted(wordListFileName))
                        wordListFileName = Path.GetDirectoryName(openFileDialog1.FileName) + @"\" + wordListFileName;
                }

                // Parse wordlist file.
                WordList wordList = null;
                WordList.TryParse(wordListFileName, aConfiguration, out wordList);

                // Parse crozzle file.
                Crozzle aCrozzle;
                Crozzle.TryParse(openFileDialog1.FileName, aConfiguration, wordList, out aCrozzle);
                SIT323Crozzle = aCrozzle;

                // Update GUI - menu enabled, display crozzle data (whether valid or invalid), and crozzle file errors.
                if (SIT323Crozzle.FileValid && SIT323Crozzle.Configuration.Valid && SIT323Crozzle.WordList.Valid)
                    crozzleToolStripMenuItem.Enabled = true;

                crozzleWebBrowser.DocumentText = SIT323Crozzle.ToStringHTML();
                ErrorListViewer.WebBrowser.DocumentText =
                    SIT323Crozzle.FileErrorsHTML +
                    SIT323Crozzle.Configuration.FileErrorsHTML +
                    SIT323Crozzle.WordList.FileErrorsHTML;

                // Log errors.
                SIT323Crozzle.LogFileErrors(SIT323Crozzle.FileErrorsTXT);
                SIT323Crozzle.LogFileErrors(SIT323Crozzle.Configuration.FileErrorsTXT);
                SIT323Crozzle.LogFileErrors(SIT323Crozzle.WordList.FileErrors);
            }
        }

        private void openCrozzleFileRemotely()
        {
            crozzleToolStripMenuItem.Enabled = false;

            // Process crozzle file to get configuration filename
            String configurationFileName = GetConfigurationFileNameRemotely(crozzleComboBox.Text);
            if (configurationFileName == null)
            {
                MessageBox.Show("configuration filename is missing from the crozzle file", ApplicationAboutBox.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                String filename = configurationFileName.Trim();
                if (Validator.IsDelimited(filename, Crozzle.StringDelimiters))
                    filename = filename.Trim(Crozzle.StringDelimiters);
                configurationFileName = filename;
            }

            // Parse configuration file.
            Configuration aConfiguration = null;
            Configuration.TryParse(configurationFileName, out aConfiguration, true);

            // Get wordlist filename.
            String wordListFileName = GetWordlistFileNameRemotely(crozzleComboBox.Text);
            if (wordListFileName == null)
            {
                MessageBox.Show("wordlist filename is missing from the crozzle file", ApplicationAboutBox.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                String filename = wordListFileName.Trim();
                if (Validator.IsDelimited(filename, Crozzle.StringDelimiters))
                    filename = filename.Trim(Crozzle.StringDelimiters);
                wordListFileName = filename;
            }

            // Parse wordlist file.
            WordList wordList = null;
            WordList.TryParse(wordListFileName, aConfiguration, out wordList, true);

            // Parse crozzle file.
            Crozzle aCrozzle;
            Crozzle.TryParse(crozzleComboBox.Text, aConfiguration, wordList, out aCrozzle, true);
            SIT323Crozzle = aCrozzle;

            // Update GUI - menu enabled, display crozzle data (whether valid or invalid), and crozzle file errors.
            if (SIT323Crozzle.FileValid && SIT323Crozzle.Configuration.Valid && SIT323Crozzle.WordList.Valid)
                crozzleToolStripMenuItem.Enabled = true;

            crozzleWebBrowser.DocumentText = SIT323Crozzle.ToStringHTML();
            ErrorListViewer.WebBrowser.DocumentText =
                SIT323Crozzle.FileErrorsHTML +
                SIT323Crozzle.Configuration.FileErrorsHTML +
                SIT323Crozzle.WordList.FileErrorsHTML;

            // Log errors.
            SIT323Crozzle.LogFileErrors(SIT323Crozzle.FileErrorsTXT, true);
            SIT323Crozzle.LogFileErrors(SIT323Crozzle.Configuration.FileErrorsTXT, true);
            SIT323Crozzle.LogFileErrors(SIT323Crozzle.WordList.FileErrors, true);
        }

        private String GetConfigurationFileName(String path)
        {
            CrozzleFileItem aCrozzleFileItem = null;
            StreamReader fileIn = new StreamReader(path);

            // Search for file name.
            while (!fileIn.EndOfStream)
            {
                if (CrozzleFileItem.TryParse(fileIn.ReadLine(), out aCrozzleFileItem))
                    if (aCrozzleFileItem.IsConfigurationFile)
                        break;
            }

            // Close files.
            fileIn.Close();

            // Return file name.
            if (aCrozzleFileItem == null)
                return (null);
            else
                return (aCrozzleFileItem.KeyValue.Value);
        }

        private String GetConfigurationFileNameRemotely(String url)
        {
            CrozzleFileItem aCrozzleFileItem = null;
            WebClient webClient = new WebClient();
            Stream stream = webClient.OpenRead(url);
            StreamReader fileIn = new StreamReader(stream);

            // Search for file name.
            while (!fileIn.EndOfStream)
            {
                if (CrozzleFileItem.TryParse(fileIn.ReadLine(), out aCrozzleFileItem))
                    if (aCrozzleFileItem.IsConfigurationFile)
                        break;
            }

            // Close files.
            fileIn.Close();

            // Return file name.
            if (aCrozzleFileItem == null)
                return (null);
            else
                return (aCrozzleFileItem.KeyValue.Value);
        }

        private String GetWordlistFileName(String path)
        {
            CrozzleFileItem aCrozzleFileItem = null;
            StreamReader fileIn = new StreamReader(path);

            // Search for file name.
            while (!fileIn.EndOfStream)
            {
                if (CrozzleFileItem.TryParse(fileIn.ReadLine(), out aCrozzleFileItem))
                    if (aCrozzleFileItem.IsWordListFile)
                        break;
            }

            // Close files.
            fileIn.Close();

            // Return file name.
            if (aCrozzleFileItem == null)
                return (null);
            else
                return (aCrozzleFileItem.KeyValue.Value);
        }

        private String GetWordlistFileNameRemotely(String url)
        {
            CrozzleFileItem aCrozzleFileItem = null;
            WebClient webClient = new WebClient();
            Stream stream = webClient.OpenRead(url);
            StreamReader fileIn = new StreamReader(stream);

            // Search for file name.
            while (!fileIn.EndOfStream)
            {
                if (CrozzleFileItem.TryParse(fileIn.ReadLine(), out aCrozzleFileItem))
                    if (aCrozzleFileItem.IsWordListFile)
                        break;
            }

            // Close files.
            fileIn.Close();

            // Return file name.
            if (aCrozzleFileItem == null)
                return (null);
            else
                return (aCrozzleFileItem.KeyValue.Value);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Validate menu event handlers
        private void crozzleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Check if the crozzle is valid.
            SIT323Crozzle.Validate();

            // Update GUI - display crozzle data (whether valid or invalid), 
            // crozle file errors, config file errors, word list file errors and crozzle errors.
            crozzleWebBrowser.DocumentText = SIT323Crozzle.ToStringHTML();
            ErrorListViewer.WebBrowser.DocumentText =
                SIT323Crozzle.FileErrorsHTML +
                SIT323Crozzle.Configuration.FileErrorsHTML +
                SIT323Crozzle.WordList.FileErrorsHTML +
                SIT323Crozzle.ErrorsHTML;

            // Log crozzle errors.
            SIT323Crozzle.LogFileErrors(SIT323Crozzle.ErrorsTXT, true);
        }
        #endregion

        #region View menu event handlers
        private void errorListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ErrorListViewer.WindowState = FormWindowState.Normal;
            ErrorListViewer.Show();
            ErrorListViewer.Activate();
        }
        #endregion

        #region Help menu event handlers
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplicationAboutBox.ShowDialog();
        }
        #endregion
    }
}
