using Gtk;
using Gdk;
using Cairo;
using System;
using Minesweeper;

public partial class MainWindow : Gtk.Window
{
    private const int TIMER_FREQUENCY = 100;

    private int choosenX = -1;
    private int choosenY = -1;
    
    private long currentTime = 0;

    private bool isChoosen = false;
    private bool _OK_Clicked = false;

    public bool OK_Clicked
    {
        get => _OK_Clicked;
        set => _OK_Clicked = value;
    }

    Records records;
    GameField gameField;
    DefeatWindow defeatWindow;
    VictoryWindow victoryWindow;    
    GameMode gameMode = GameMode.ClassicModeEasy;   

    public MainWindow() : base(Gtk.WindowType.Toplevel)
    {
        Build();
        StartGame();

        drawingArea.AddEvents((int)EventMask.ButtonPressMask);
        drawingArea.ButtonPressEvent += OnFieldClick;
        
        GLib.Timeout.Add(TIMER_FREQUENCY, new GLib.TimeoutHandler(OnTimer));
    }

    protected bool OnTimer()
    {
        UpdateTime();
        drawingArea.QueueDraw();
        ShowOtherWindows();

        return true;
    }

    protected void UpdateTime()
    {
        if (gameField.IsGame)
        {
            time.Text = "Время: ";
            numberOfFlags.Text = "Количество мин: " + gameField.NumberOfFlags;
            currentTime += TIMER_FREQUENCY;

            long hours = currentTime / (3600 * 1000);
            long minutes = currentTime / (60 * 1000) - 60 * hours;
            long seconds = currentTime / 1000 - 60 * minutes - 3600 * hours;

            if (hours < 10)
                time.Text += "0" + hours;
            else
                time.Text += hours;

            time.Text += ":";

            if (minutes < 10)
                time.Text += "0" + minutes;
            else
                time.Text += minutes;

            time.Text += ":";

            if (seconds < 10)
                time.Text += "0" + seconds;
            else
                time.Text += seconds;
        }
        else if (gameMode == GameMode.None)
        {
            time.Text = "Время: 00:00:00";
            numberOfFlags.Text = "Количество мин: 0";
        }
    }

    protected void PutUpFlagsState()
    {
        if (putUpFlags.Active)
            gameField.PutUpFlagsActive = true;
        else
            gameField.PutUpFlagsActive = false;
    }

    protected void StartGame()
    {
        gameField = new GameField(gameMode);
        defeatWindow = new DefeatWindow();       
        victoryWindow = new VictoryWindow();
        records = new Records();

        currentTime = 0;
        gameField.IsFirstStep = gameField.IsGame = true;
    }

    protected void ShowOtherWindows()
    {
        if (gameField.IsLost)
        {
            defeatWindow.ShowDefeatWindow(gameField);

            if (defeatWindow.OK_Clicked)
            {
                defeatWindow.Destroy();
                StartGame();
            }
        }
        else if (gameField.IsWon)
        {
            victoryWindow.ShowVictoryWindow(gameField);

            if (victoryWindow.OK_Clicked)
            {
                victoryWindow.WriteScoreInFile(gameMode, currentTime, time.Text);
                victoryWindow.Destroy();
                StartGame();
            }
        }
        if (records.isExit)
        {
            records.Destroy();
            records = new Records();
        }
    }

    protected void OnClassicModeEasy(object sender, EventArgs e)
    {
        gameMode = GameMode.ClassicModeEasy;
        StartGame();
    }

    protected void OnClassicModeMedium(object sender, EventArgs e)
    {
        gameMode = GameMode.ClassicModeMedium;
        StartGame();
    }

    protected void OnClassicModeHard(object sender, EventArgs e)
    {              
        gameMode = GameMode.ClassicModeHard;
        StartGame();
    }

    protected void OnHexagonModeEasy(object sender, EventArgs e)
    {
        gameMode = GameMode.HexagonModeEasy;
        StartGame();
    }

    protected void OnHexagonModeMedium(object sender, EventArgs e)
    {
        gameMode = GameMode.HexagonModeMedium;
        StartGame();
    }

    protected void OnHexagonModeHard(object sender, EventArgs e)
    {
        gameMode = GameMode.HexagonModeHard;
        StartGame();
    }

    protected void OnRecords(object sender, EventArgs e)
    {       
        records.Show();
    }

    protected void OnRules(object sender, EventArgs e)
    {
        RulesWindow rule = new RulesWindow();
        rule.Show();
    }

    protected void OnAboutAutor(object sender, EventArgs e)
    {
        AboutDialog aboutDialog = new AboutDialog();
        aboutDialog.SetSizeRequest(400, 250);
        aboutDialog.SetPosition(WindowPosition.Center);
        aboutDialog.ProgramName = "Сапёр";
        aboutDialog.Version = "0.1";
        aboutDialog.Copyright = "(c) Святослав Снегирев";
        aboutDialog.Comments = @"Объектно-ориентированная разработка программы с графическим пользовательским интерфейсом «сверху-вниз» на тему Сапёр (англ. Minesweeper), выполнена студентом группы И508Б.";
        aboutDialog.Website = "https://github.com/slavicksnegirev";
        aboutDialog.Logo = new Pixbuf("iconBomb.png");
        aboutDialog.Run();
        aboutDialog.Destroy();
    }

    protected void OnFieldClick(object o, ButtonPressEventArgs args)
    {
        if (!isChoosen && gameField.IsGame)
        {
            if (gameMode == GameMode.ClassicModeEasy || gameMode == GameMode.ClassicModeMedium || gameMode == GameMode.ClassicModeHard)
            {
                choosenX = (int)((args.Event.X - gameField.ScaleCoordX) / (gameField.CellSize + 1));
                choosenY = (int)((args.Event.Y - 17) / (gameField.CellSize + 1));
            }
            else if (gameMode == GameMode.HexagonModeEasy || gameMode == GameMode.HexagonModeMedium || gameMode == GameMode.HexagonModeHard)
            {               
                choosenY = (int)((args.Event.Y - gameField.ScaleCoordY) / gameField.CellSize / 1.75);
                if (choosenY % 2 == 0)
                    choosenX = (int)((args.Event.X - gameField.ScaleCoordX) / gameField.CellSize / 2);
                else
                    choosenX = (int)((args.Event.X - gameField.ScaleCoordX) / gameField.CellSize / 2 - 0.5);
            }

            if (choosenX < 0 || choosenY < 0 || choosenX >= gameField.NumberOfCells || choosenY >= gameField.NumberOfCells)
            {
                choosenX = choosenY = -1;
                isChoosen = false;
            }
            else
            {
                if (!putUpFlags.Active)
                {
                    if (gameField.Field[choosenY + 1, choosenX + 1] == -1)
                    {
                        currentTime = 0;
                        gameField.IsLost = true;
                        gameField.IsGame = false;
                    }
                    else
                    {
                        if (gameField.IsFirstStep)
                        {
                            if (gameMode == GameMode.ClassicModeEasy || gameMode == GameMode.ClassicModeMedium || gameMode == GameMode.ClassicModeHard)
                            {
                                for (int col = 0; col < 3; col++)
                                {
                                    for (int row = 0; row < 3; row++)
                                    {
                                        gameField.Field[choosenY + col, choosenX + row] = 0;
                                    }
                                }
                                gameField.GenerateMines();

                                for (int col = 0; col < 3; col++)
                                {
                                    for (int row = 0; row < 3; row++)
                                    {
                                        if (choosenX + row > 0 && choosenY + col > 0 && choosenX + row < gameField.NumberOfCells + 1 && choosenY + col < gameField.NumberOfCells + 1)
                                        {
                                            gameField.IsFirstStep = true;
                                            gameField.FindMines(gameField, gameMode, choosenY + col, choosenX + row);
                                        }
                                    }
                                }
                            }
                            else if (gameMode == GameMode.HexagonModeEasy || gameMode == GameMode.HexagonModeMedium || gameMode == GameMode.HexagonModeHard)
                            {
                                for (int col = 0; col < 3; col++)
                                {
                                    for (int row = 1; row < 3; row++)
                                    {
                                        if (col == 1 && row == 1)
                                            gameField.Field[choosenY + 1, choosenX] = 0;

                                        gameField.Field[choosenY + col, choosenX + row] = 0;
                                    }
                                }
                                gameField.GenerateMines();

                                for (int col = 0; col < 3; col++)
                                {
                                    for (int row = 1; row < 3; row++)
                                    {
                                        if (col == 1 && row == 1)
                                        {
                                            if (choosenX > 0 && choosenY + 1 > 0 && choosenX < gameField.NumberOfCells + 1 && choosenY + 1 < gameField.NumberOfCells + 1)
                                            {
                                                gameField.IsFirstStep = true;
                                                gameField.FindMines(gameField, gameMode, choosenY + 1, choosenX);
                                            }
                                        }
                                        if (choosenX + row > 0 && choosenY + col > 0 && choosenX + row < gameField.NumberOfCells + 1 && choosenY + col < gameField.NumberOfCells + 1)
                                        {
                                            gameField.IsFirstStep = true;
                                            gameField.FindMines(gameField, gameMode, choosenY + col, choosenX + row);
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            gameField.FindMines(gameField, gameMode, choosenY + 1, choosenX + 1);
                            gameField.IsGameWon(gameField);
                        }
                    }
                    isChoosen = true;
                }
                else
                {
                    if (gameField.FlagMap[choosenY + 1, choosenX + 1] > 0 && (gameField.Field[choosenY + 1, choosenX + 1] == -1 || gameField.Field[choosenY + 1, choosenX + 1] == 10) && gameField.NumberOfFlags > 0)
                        gameField.FlagMap[choosenY + 1, choosenX + 1] = 0;
                    else
                        gameField.FlagMap[choosenY + 1, choosenX + 1] = 10;             
                }               
            }
        }
    }

    protected void OnDrawingExposeEvent(object o, ExposeEventArgs args)
    {
        DrawingArea area = (DrawingArea)o;
        Context cc = CairoHelper.Create(area.GdkWindow);

        gameField.CellSize = (Allocation.Height - (0.1 * Allocation.Height)) / gameField.NumberOfCells - 1;
        gameField.CellSizeY = (Allocation.Height - (0.1 * Allocation.Height)) / gameField.NumberOfCells - 1;
        gameField.SetScaleForCoordX(gameField, gameField.CellSize, Allocation.Width);
        gameField.SetScaleForCoordY(gameField, gameField.CellSize, Allocation.Height);

        cc.LineWidth = 2;
        cc.SetSourceRGB(0.1, 0.2, 0.3);
        cc.Paint();
        cc.SelectFontFace("Courier", FontSlant.Normal, FontWeight.Bold);
        cc.SetFontSize((int)gameField.CellSize);

        PutUpFlagsState();
        gameField.ChangeCellSizeForHexagonMode(cc, gameMode);
        gameField.DrawField(cc, gameField, Allocation.Width);
        gameField.CountFlags();
        gameField.DrawSelectionFrame(cc, gameField, choosenX, choosenY, ref isChoosen);
        gameField.DrawGameOverAnimation(cc, gameField, Allocation.Width, Allocation.Height);

        ((IDisposable)cc.GetTarget()).Dispose();
        ((IDisposable)cc).Dispose();
    }

    protected void OnDeleteEvent(object sender, DeleteEventArgs a)
    {      
        Application.Quit();
        a.RetVal = true;
    }
}
