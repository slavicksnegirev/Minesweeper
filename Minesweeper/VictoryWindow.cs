using Gtk;
using System;
using System.IO;

namespace Minesweeper
{
    public partial class VictoryWindow : Window
    {
        const string recordsClassicModeEasy = "recordsClassicModeEasy.txt";
        const string recordsClassicModeMedium = "recordsClassicModeMedium.txt";
        const string recordsClassicModeHard = "recordsClassicModeHard.txt";
        const string recordsHexagonModeEasy = "recordsHexagonModeEasy.txt";
        const string recordsHexagonModeMedium = "recordsHexagonModeMedium.txt";
        const string recordsHexagonModeHard = "recordsHexagonModeHard.txt";

        bool isNotFound = true;

        int index = 0;
        int hours = 0;
        int minutes = 0;
        int seconds = 0;

        string allText = @"";
        string textLine = @"";
        string stringHours = "";
        string stringMinutes = "";
        string stringSeconds = "";

        private string _playerName = "No name";
        private bool _OK_Clicked = false;

        public string PlayerName
        {
            get => _playerName;
            set => _playerName = value;
        }

        public bool OK_Clicked
        {
            get => _OK_Clicked;
            set => _OK_Clicked = value;
        }

        public VictoryWindow() : base(WindowType.Toplevel)
        {
            this.Build();
            this.Hide();

            this.entryField.TextInserted += EntryField_TextInserted;
        }

        protected void EntryField_TextInserted(object o, TextInsertedArgs args)
        {
            this.PlayerName = entryField.Text;
        }

        public void ShowVictoryWindow(GameField gameField)
        {
            if (gameField.Size < 0)
                this.Show();
        }

        public void WriteScoreInFile(GameMode gameMode, long time, string currentTime)
        {
            switch (gameMode)
            {
                case GameMode.ClassicModeEasy:
                    {
                        FileStream file = new FileStream(recordsClassicModeEasy, FileMode.Open);
                        StreamReader fileReader = new StreamReader(file);

                        this.WriteScore(fileReader, time, currentTime);
                        fileReader.Close();

                        file = new FileStream(recordsClassicModeEasy, FileMode.Open, FileAccess.ReadWrite);
                        StreamWriter fileWriter = new StreamWriter(file);

                        fileWriter.Write(this.allText);
                        fileWriter.Close();

                        break;
                    }
                case GameMode.ClassicModeMedium:
                    {
                        FileStream file = new FileStream(recordsClassicModeMedium, FileMode.Open);
                        StreamReader fileReader = new StreamReader(file);

                        this.WriteScore(fileReader, time, currentTime);
                        fileReader.Close();

                        file = new FileStream(recordsClassicModeMedium, FileMode.Open, FileAccess.ReadWrite);
                        StreamWriter fileWriter = new StreamWriter(file);

                        fileWriter.Write(this.allText);
                        fileWriter.Close();

                        break;
                    }
                case GameMode.ClassicModeHard:
                    {
                        FileStream file = new FileStream(recordsClassicModeHard, FileMode.Open);
                        StreamReader fileReader = new StreamReader(file);

                        this.WriteScore(fileReader, time, currentTime);
                        fileReader.Close();

                        file = new FileStream(recordsClassicModeHard, FileMode.Open, FileAccess.ReadWrite);
                        StreamWriter fileWriter = new StreamWriter(file);

                        fileWriter.Write(this.allText);
                        fileWriter.Close();

                        break;
                    }
                case GameMode.HexagonModeEasy:
                    {
                        FileStream file = new FileStream(recordsHexagonModeEasy, FileMode.Open);
                        StreamReader fileReader = new StreamReader(file);

                        this.WriteScore(fileReader, time, currentTime);
                        fileReader.Close();

                        file = new FileStream(recordsHexagonModeEasy, FileMode.Open, FileAccess.ReadWrite);
                        StreamWriter fileWriter = new StreamWriter(file);

                        fileWriter.Write(this.allText);
                        fileWriter.Close();

                        break;
                    }
                case GameMode.HexagonModeMedium:
                    {
                        FileStream file = new FileStream(recordsHexagonModeMedium, FileMode.Open);
                        StreamReader fileReader = new StreamReader(file);

                        this.WriteScore(fileReader, time, currentTime);
                        fileReader.Close();

                        file = new FileStream(recordsHexagonModeMedium, FileMode.Open, FileAccess.ReadWrite);
                        StreamWriter fileWriter = new StreamWriter(file);

                        fileWriter.Write(this.allText);
                        fileWriter.Close();

                        break;
                    }
                case GameMode.HexagonModeHard:
                    {
                        FileStream file = new FileStream(recordsHexagonModeHard, FileMode.Open);
                        StreamReader fileReader = new StreamReader(file);

                        this.WriteScore(fileReader, time, currentTime);
                        fileReader.Close();

                        file = new FileStream(recordsHexagonModeHard, FileMode.Open, FileAccess.ReadWrite);
                        StreamWriter fileWriter = new StreamWriter(file);

                        fileWriter.Write(this.allText);
                        fileWriter.Close();

                        break;
                    }
                default:
                    break;
            }
        }

        protected void WriteScore(StreamReader fileReader, long time, string currentTime)
        {
            while (!fileReader.EndOfStream)
            {
                this.index++;
                this.stringHours = "";
                this.stringMinutes = "";
                this.stringSeconds = "";
                this.textLine = fileReader.ReadLine();

                for (int j = 0; j < textLine.Length && isNotFound; j++)
                {
                    if (textLine[j] == '(')
                    {
                        this.stringHours += textLine[j + 1];
                        this.stringHours += textLine[j + 2];
                        this.stringMinutes += textLine[j + 4];
                        this.stringMinutes += textLine[j + 5];
                        this.stringSeconds += textLine[j + 7];
                        this.stringSeconds += textLine[j + 8];

                        this.hours = Convert.ToInt32(this.stringHours);
                        this.minutes = Convert.ToInt32(this.stringMinutes);
                        this.seconds = Convert.ToInt32(this.stringSeconds);

                        if ((this.hours * 3600 + this.minutes * 60 + this.seconds > time / 1000) && this.isNotFound)
                        {
                            currentTime = currentTime.Replace("Время: ", "");
                            if (index == 1)
                                this.allText += index + ". " + this.PlayerName + " (" + currentTime + ")";
                            else
                                this.allText += "\n" + index + ". " + this.PlayerName + " (" + currentTime + ")";
                            this.isNotFound = false;
                        }
                        j = this.textLine.Length;
                    }
                }
                if (this.isNotFound)
                {
                    if (index == 1)
                        this.allText += this.textLine;
                    else
                        this.allText += "\n" + this.textLine;
                }
                else
                {
                    this.textLine = this.textLine.Substring(1);
                    this.textLine = this.textLine.Insert(0, Convert.ToString(index + 1));
                    this.allText += "\n" + this.textLine;
                }
            }
            if (index == 0)
            {
                currentTime = currentTime.Replace("Время: ", "");
                this.allText += (index + 1) + ". " + this.PlayerName + " (" + currentTime + ")";
            }
            else if (this.isNotFound)
            {
                currentTime = currentTime.Replace("Время: ", "");
                this.allText += "\n" + (index + 1) + ". " + this.PlayerName + " (" + currentTime + ")";
            }
        }

        protected void OnClicked(object sender, EventArgs e)
        {
            this.OK_Clicked = true;

            if (this.PlayerName.Equals("Minesweeper.VictoryWindow") || this.PlayerName.Length == 0)
            {
                this.PlayerName = "No name";
            }
        }

        protected void OnDeleteEvent(object o, DeleteEventArgs args)
        {
            this.OK_Clicked = true;

            if (this.PlayerName.Equals("Minesweeper.VictoryWindow") || this.PlayerName.Length == 0)
            {
                this.PlayerName = "No name";
            }
        }
    }
}
