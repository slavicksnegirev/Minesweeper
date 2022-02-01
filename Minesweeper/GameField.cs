using System;

namespace Minesweeper
{
    public class GameField : GameRendering, IGameplay
    {
        const int BORDER_CELLS = 2;

        private int[,] _field;
        private int[,] _flagMap;
        private int _numberOfCells = -1;
        private int _numberOfBombs = -1;
        private int _numberOfFlags = -1;

        private double[,] _hexagonCoords = new double[7, 2]
        {
            // ( y; x)
            { 0, Math.Sqrt(3) / 2},
            { 0.5, Math.Sqrt(3)},
            { 1.5, Math.Sqrt(3)},
            { 2, Math.Sqrt(3) / 2},
            { 1.5, 0},
            { 0.5, 0},
            { 0, Math.Sqrt(3) / 2}
        };

        private GameMode _gameMode = GameMode.None;

        // свойства
        public int[,] Field
        {
            get => _field;
            set => _field = value;
        }

        public int[,] FlagMap
        {
            get => _flagMap;
            set => _flagMap = value;
        }

        public int NumberOfCells
        {
            get => _numberOfCells;
            set
            {
                _numberOfCells = value;
                if (_numberOfCells < 0)
                {
                    _numberOfCells = -1;
                }
            }
        }

        public int NumberOfBombs
        {
            get => _numberOfBombs;
            set
            {
                _numberOfBombs = value;
                if (_numberOfBombs < 0)
                {
                    _numberOfBombs = -1;
                }
            }
        }

        public int NumberOfFlags
        {
            get => _numberOfFlags;
            set => _numberOfFlags = value;
        }

        public double[,] HexagonCoords
        {
            get => _hexagonCoords;
        }

        public GameMode gameMode
        {
            get => _gameMode;
            set => _gameMode = value;
        }

        // конструктор
        public GameField(GameMode gameMode)
        {
            this.gameMode = gameMode;

            switch (gameMode)
            {
                case GameMode.ClassicModeEasy:
                    {
                        NumberOfCells = 10;
                        NumberOfBombs = NumberOfFlags = 20;

                        // дополнительные боковые ячейки необходимы для упрощения обхода в ширину
                        Field = new int[NumberOfCells + BORDER_CELLS, NumberOfCells + BORDER_CELLS];
                        FlagMap = new int[NumberOfCells + BORDER_CELLS, NumberOfCells + BORDER_CELLS];

                        GenerateField(Field);
                        GenerateField(FlagMap);

                        break;
                    }
                case GameMode.ClassicModeMedium:
                    {
                        NumberOfCells = 20;
                        NumberOfBombs = NumberOfFlags = 80;

                        Field = new int[NumberOfCells + BORDER_CELLS, NumberOfCells + BORDER_CELLS];
                        FlagMap = new int[NumberOfCells + BORDER_CELLS, NumberOfCells + BORDER_CELLS];

                        GenerateField(Field);
                        GenerateField(FlagMap);

                        break;
                    }
                case GameMode.ClassicModeHard:
                    {
                        NumberOfCells = 30;
                        NumberOfBombs = NumberOfFlags = 180;

                        Field = new int[NumberOfCells + BORDER_CELLS, NumberOfCells + BORDER_CELLS];
                        FlagMap = new int[NumberOfCells + BORDER_CELLS, NumberOfCells + BORDER_CELLS];

                        GenerateField(Field);
                        GenerateField(FlagMap);

                        break;
                    }
                case GameMode.HexagonModeEasy:
                    {
                        NumberOfCells = 10;
                        NumberOfBombs = NumberOfFlags = 20;

                        Field = new int[NumberOfCells + BORDER_CELLS, NumberOfCells + BORDER_CELLS];
                        FlagMap = new int[NumberOfCells + BORDER_CELLS, NumberOfCells + BORDER_CELLS];

                        GenerateField(Field);
                        GenerateField(FlagMap);

                        break;
                    }
                case GameMode.HexagonModeMedium:
                    {
                        NumberOfCells = 20;
                        NumberOfBombs = NumberOfFlags = 80;

                        Field = new int[NumberOfCells + BORDER_CELLS, NumberOfCells + BORDER_CELLS];
                        FlagMap = new int[NumberOfCells + BORDER_CELLS, NumberOfCells + BORDER_CELLS];

                        GenerateField(Field);
                        GenerateField(FlagMap);

                        break;
                    }
                case GameMode.HexagonModeHard:
                    {
                        NumberOfCells = 30;
                        NumberOfBombs = NumberOfFlags = 180;

                        Field = new int[NumberOfCells + BORDER_CELLS, NumberOfCells + BORDER_CELLS];
                        FlagMap = new int[NumberOfCells + BORDER_CELLS, NumberOfCells + BORDER_CELLS];

                        GenerateField(Field);
                        GenerateField(FlagMap);

                        break;
                    }
                default:
                    break;
            }
        }

        public void GenerateField(int[,] array)
        {
            for (int i = 1; i < NumberOfCells + 1; i++)
            {
                for (int j = 1; j < NumberOfCells + 1; j++)
                {
                    array[i, j] = 10;
                }
            }
        }

        public void GenerateMines()
        {
            Random randomX = new Random();
            Random randomY = new Random();

            for (int i = 1; i < NumberOfBombs + 1; i++)
            {
                int x = randomX.Next(1, NumberOfCells + 1);
                int y = randomY.Next(1, NumberOfCells + 1);

                if (Field[y, x] != -1 && Field[y, x] != 0)
                    Field[y, x] = -1;
                else
                    i--;
            }
        }

        public void CountFlags()
        {
            int count = 0;
            for (int i = 1; i < NumberOfCells + 1; i++)
            {
                for (int j = 1; j < NumberOfCells + 1; j++)
                {
                    if (FlagMap[i, j] == 0)
                    {
                        count++;
                    }
                }
            }
            NumberOfFlags = NumberOfBombs - count;
        }

        public void OutputField()
        {
            for (int i = 0; i < NumberOfCells + BORDER_CELLS; i++)
            {
                for (int j = 0; j < NumberOfCells + BORDER_CELLS; j++)
                {
                    //if (i % 2 != 0)
                    //    Console.Write("    " + Field[i, j] + "\t");
                    //else
                    //    Console.Write(Field[i, j] + "\t");
                    Console.Write(FlagMap[i, j] + "\t");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
