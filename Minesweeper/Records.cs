using Gtk;
using System;
using System.IO;

namespace Minesweeper
{
    public partial class Records : Window
    {
        const string recordsClassicModeEasy = @"recordsClassicModeEasy.txt";
        const string recordsClassicModeMedium = "recordsClassicModeMedium.txt";
        const string recordsClassicModeHard = "recordsClassicModeHard.txt";
        const string recordsHexagonModeEasy = "recordsHexagonModeEasy.txt";
        const string recordsHexagonModeMedium = "recordsHexagonModeMedium.txt";
        const string recordsHexagonModeHard = "recordsHexagonModeHard.txt";

        public bool isExit = false;

        public Records() : base(WindowType.Toplevel)
        {
            this.Build();
            this.Hide();
            
            this.FileReading(recordsClassicModeEasy);
            this.FileReading(recordsClassicModeMedium);
            this.FileReading(recordsClassicModeHard);
            this.FileReading(recordsHexagonModeEasy);
            this.FileReading(recordsHexagonModeMedium);
            this.FileReading(recordsHexagonModeHard);
        }

        protected void FileReading(string fileName)
        {
            FileInfo fileRecordsMode = new FileInfo(fileName);

            if (fileRecordsMode.Exists)
            {
                string text = @"";

                FileStream file = new FileStream(fileName, FileMode.Open, FileAccess.ReadWrite, FileShare.None); 
                StreamReader fileReader = new StreamReader(file); 
                text = fileReader.ReadToEnd();

                switch (fileName)
                {
                    case "recordsClassicModeEasy.txt":
                        {
                            if (text != "")
                                this.labelClassicModeEasy.Text = text;
                            break;
                        }
                    case "recordsClassicModeMedium.txt":
                        {
                            if (text != "")
                                this.labelClassicModeMedium.Text = text;
                            break;
                        }
                    case "recordsClassicModeHard.txt":
                        {
                            if (text != "")
                                this.labelClassicModeHard.Text = text;
                            break;
                        }
                    case "recordsHexagonModeEasy.txt":
                        {
                            if (text != "")
                                this.labelHexagonModeEasy.Text = text;
                            break;
                        }
                    case "recordsHexagonModeMedium.txt":
                        {
                            if (text != "")
                                this.labelHexagonModeMedium.Text = text;
                            break;
                        }
                    case "recordsHexagonModeHard.txt":
                        {
                            if (text != "")
                                this.labelHexagonModeHard.Text = text;
                            break;
                        }
                    default:
                        break;
                }               
                fileReader.Close(); 
            }
            else
            {
                File.Create(fileName).Dispose();               
                Console.WriteLine("Файл '" + fileName + "' создан.");
            }
        }
       
        protected void OnDeleteEvent(object o, DeleteEventArgs args)
        {
            this.isExit = true;
        }
    }
}
