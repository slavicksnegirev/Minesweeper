using System;

namespace Minesweeper
{
    public class GameLogic
    {
        private bool _isWon = false;
        private bool _isLost = false;
        private bool _isGame = false;
        private bool _isFirstStep = true;

        public bool IsWon
        {
            get => _isWon;
            set => _isWon = value;
        }

        public bool IsLost
        {
            get => _isLost;
            set => _isLost = value;
        }

        public bool IsGame
        {
            get => _isGame;
            set => _isGame = value;
        }

        public bool IsFirstStep
        {
            get => _isFirstStep;
            set => _isFirstStep = value;
        }

        public void FindMines(GameField gameField, GameMode gameMode, int y, int x)
        {
            if (gameMode == GameMode.ClassicModeEasy || gameMode == GameMode.ClassicModeMedium || gameMode == GameMode.ClassicModeHard)
            {
                if (gameField.Field[y, x] > 9 || IsFirstStep) // если впервые нажали на клетку, то задаем ей нулевое значение
                {
                    if (!IsFirstStep)
                        gameField.Field[y, x] -= 10;

                    IsFirstStep = false;
                    CheckMinesAround(gameField, gameMode, y, x);

                    if (gameField.Field[y, x] == 0)
                    {
                        FindMines(gameField, gameMode, y - 1, x - 1);    // левая верхня клетка
                        FindMines(gameField, gameMode, y - 1, x);        // центральная верхняя клетка
                        FindMines(gameField, gameMode, y - 1, x + 1);    // правая верхняя клетка
                        FindMines(gameField, gameMode, y, x - 1);        // клетка слева
                        FindMines(gameField, gameMode, y, x + 1);        // клетка справа
                        FindMines(gameField, gameMode, y + 1, x - 1);    // левая нижняя клетка
                        FindMines(gameField, gameMode, y + 1, x);        // центральная нижняя клетка
                        FindMines(gameField, gameMode, y + 1, x + 1);    // правая нижняя клетка
                    }
                }
            }
            else if (gameMode == GameMode.HexagonModeEasy || gameMode == GameMode.HexagonModeMedium || gameMode == GameMode.HexagonModeHard)
            {
                if (gameField.Field[y, x] > 9 || IsFirstStep) // если впервые нажали на клетку, то задаем ей нулевое значение
                {
                    if (!IsFirstStep)
                        gameField.Field[y, x] -= 10;

                    IsFirstStep = false;
                    CheckMinesAround(gameField, gameMode, y, x);

                    if (gameField.Field[y, x] == 0)
                    {
                        if (y % 2 == 0)
                        {
                            FindMines(gameField, gameMode, y - 1, x);        // левый верхний шестиугольник
                            FindMines(gameField, gameMode, y - 1, x + 1);    // правый верхний шестиугольник
                            FindMines(gameField, gameMode, y, x - 1);        // шестиугольник слева
                            FindMines(gameField, gameMode, y, x + 1);        // шестиугольник справа
                            FindMines(gameField, gameMode, y + 1, x);        // левый нижний шестиугольник
                            FindMines(gameField, gameMode, y + 1, x + 1);    // правый нижний шестиугольник
                        }
                        else
                        {
                            FindMines(gameField, gameMode, y - 1, x - 1);    // левый верхний шестиугольник
                            FindMines(gameField, gameMode, y - 1, x);        // правый верхний шестиугольник
                            FindMines(gameField, gameMode, y, x - 1);        // шестиугольник слева
                            FindMines(gameField, gameMode, y, x + 1);        // шестиугольник справа
                            FindMines(gameField, gameMode, y + 1, x - 1);    // левый нижний шестиугольник
                            FindMines(gameField, gameMode, y + 1, x);        // правый нижний шестиугольник
                        }
                    }
                }
            }
        }

        public void CheckMinesAround(GameField gameField, GameMode gameMode, int y, int x)
        {
            if (gameMode == GameMode.ClassicModeEasy || gameMode == GameMode.ClassicModeMedium || gameMode == GameMode.ClassicModeHard)
            {
                if (gameField.Field[y - 1, x - 1] == -1) { gameField.Field[y, x] += 1; }     // левая верхня клетка
                if (gameField.Field[y - 1, x] == -1)     { gameField.Field[y, x] += 1; }     // центральная верхняя клетка
                if (gameField.Field[y - 1, x + 1] == -1) { gameField.Field[y, x] += 1; }     // правая верхняя клетка
                if (gameField.Field[y, x - 1] == -1)     { gameField.Field[y, x] += 1; }     // клетка слева
                if (gameField.Field[y, x + 1] == -1)     { gameField.Field[y, x] += 1; }     // клетка справа
                if (gameField.Field[y + 1, x - 1] == -1) { gameField.Field[y, x] += 1; }     // левая нижняя клетка
                if (gameField.Field[y + 1, x] == -1)     { gameField.Field[y, x] += 1; }     // центральная нижняя клетка
                if (gameField.Field[y + 1, x + 1] == -1) { gameField.Field[y, x] += 1; }     // правая нижняя клетка
            }
            else if (gameMode == GameMode.HexagonModeEasy || gameMode == GameMode.HexagonModeMedium || gameMode == GameMode.HexagonModeHard)
            {
                if (y % 2 == 0)
                {
                    if (gameField.Field[y - 1, x] == -1)     { gameField.Field[y, x] += 1; }     // левый верхний шестиугольник
                    if (gameField.Field[y - 1, x + 1] == -1) { gameField.Field[y, x] += 1; }     // правый верхний шестиугольник
                    if (gameField.Field[y, x - 1] == -1)     { gameField.Field[y, x] += 1; }     // шестиугольник слева
                    if (gameField.Field[y, x + 1] == -1)     { gameField.Field[y, x] += 1; }     // шестиугольник справа
                    if (gameField.Field[y + 1, x] == -1)     { gameField.Field[y, x] += 1; }     // левый нижний шестиугольник
                    if (gameField.Field[y + 1, x + 1] == -1) { gameField.Field[y, x] += 1; }     // правый нижний шестиугольник
                }
                else
                {
                    if (gameField.Field[y - 1, x - 1] == -1) { gameField.Field[y, x] += 1; }     // левый верхний шестиугольник
                    if (gameField.Field[y - 1, x] == -1)     { gameField.Field[y, x] += 1; }     // правый верхний шестиугольник
                    if (gameField.Field[y, x - 1] == -1)     { gameField.Field[y, x] += 1; }     // шестиугольник слева
                    if (gameField.Field[y, x + 1] == -1)     { gameField.Field[y, x] += 1; }     // шестиугольник справа
                    if (gameField.Field[y + 1, x - 1] == -1) { gameField.Field[y, x] += 1; }     // левый нижний шестиугольник
                    if (gameField.Field[y + 1, x] == -1)     { gameField.Field[y, x] += 1; }     // правый нижний шестиугольник
                }
            }
        }

        public void IsGameWon(GameField gameField)
        {
            for (int i = 1; i < gameField.NumberOfCells + 1; i++)
            {
                for (int j = 0; j < gameField.NumberOfCells + 1; j++)
                {
                    if (gameField.Field[i, j] == 10)
                        return;
                }
                if (i == gameField.NumberOfCells)
                {                   
                    gameField.IsWon = true;
                    gameField.IsGame = false;
                }
            }
        }
    }
}
