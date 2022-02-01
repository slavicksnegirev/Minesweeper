using Cairo;

namespace Minesweeper
{
    public class GameRendering : ImageProcessing
    {
        private double _size = 150;
        private double _alpha = 1.0;
        private double _scaleCoordX = -1;
        private double _scaleCoordY = -1;       

        private bool _putUpFlagsActive = false;

        // свойства
        public double Size
        {
            get => _size;
            set => _size = value;
        }

        public double Alpha
        {
            get => _alpha;
            set
            {
                _alpha = value;
                if (_alpha < 0)
                {
                    _alpha = 1.0;
                }
            }
        }

        public double ScaleCoordX
        {
            get => _scaleCoordX;
            set
            {
                _scaleCoordX = value;
                if (_scaleCoordX < 0)
                {
                    _scaleCoordX = -1;
                }
            }
        }

        public double ScaleCoordY
        {
            get => _scaleCoordY;
            set
            {
                _scaleCoordY = value;
                if (_scaleCoordY < 0)
                {
                    _scaleCoordY = -1;
                }
            }
        }

        public bool PutUpFlagsActive
        {
            get => _putUpFlagsActive;
            set
            { _putUpFlagsActive = value; }
        }

        public void SetScaleForCoordX(GameField gameField, double cellSize, int SCREEN_WIDTH)
        {
            ScaleCoordX = (SCREEN_WIDTH - gameField.NumberOfCells * (cellSize + 1)) / 2;
        }

        public void SetScaleForCoordY(GameField gameField, double cellSize, int SCREEN_HEIGHT)
        {
            ScaleCoordY = (SCREEN_HEIGHT - gameField.NumberOfCells * cellSize) / 2;
        }

        public void ChangeCellSizeForHexagonMode(Context cc, GameMode gameMode)
        {
            if (gameMode == GameMode.HexagonModeEasy || gameMode == GameMode.HexagonModeMedium || gameMode == GameMode.HexagonModeHard)
            {
                CellSize /= 1.8;
                cc.SetFontSize((int)(CellSize * 1.5));
            }
        }

        public void DrawField(Context cc, GameField gameField, int SCREEN_WIDTH)
        {
            if (gameField.gameMode == GameMode.ClassicModeEasy || gameField.gameMode == GameMode.ClassicModeMedium || gameField.gameMode == GameMode.ClassicModeHard)
            {
                for (int i = 1; i < gameField.NumberOfCells + 1; i++)
                {
                    for (int j = 1; j < gameField.NumberOfCells + 1; j++)
                    {
                        cc.Rectangle(new PointD((j - 1) * (CellSize + 1) + ScaleCoordX, (i - 1) * (CellSize + 1) + 17), CellSize, CellSize);

                        if (gameField.Field[i, j] == 10)
                        {
                            if (PutUpFlagsActive && gameField.IsGame)
                                cc.SetSourceRGB(0, 0.5, 0.4); 
                            else
                                cc.SetSourceRGBA(0.5, 0.5, 1, 0.8);

                            cc.Fill();
                        }
                        else if (gameField.Field[i, j] > -1)
                        {
                            cc.SetSourceRGBA(1, 1, 1, 1);
                            cc.Fill();
                            cc.MoveTo((j - 1) * (CellSize + 1) + ScaleCoordX + CellSize / 5, (i - 1) * (CellSize + 1) + 17 + CellSize * 4 / 5);

                            gameField.FlagMap[i, j] = 10;

                            if (gameField.Field[i, j] == 1)
                            {
                                cc.SetSourceRGB(0, 0, 1);
                                cc.ShowText("1");
                            }
                            else if (gameField.Field[i, j] == 2)
                            {
                                cc.SetSourceRGB(0, 0.4, 0);
                                cc.ShowText("2");
                            }
                            else if (gameField.Field[i, j] == 3)
                            {
                                cc.SetSourceRGB(1, 0, 0);
                                cc.ShowText("3");
                            }
                            else if (gameField.Field[i, j] == 4)
                            {
                                cc.SetSourceRGB(0, 0, 0.5);
                                cc.ShowText("4");
                            }
                            else if (gameField.Field[i, j] == 5)
                            {
                                cc.SetSourceRGB(0.5, 0, 0);
                                cc.ShowText("5");
                            }
                            else if (gameField.Field[i, j] == 6)
                            {
                                cc.SetSourceRGB(0.38, 0.29, 0.49);
                                cc.ShowText("6");
                            }
                            else if (gameField.Field[i, j] == 7)
                            {
                                cc.SetSourceRGB(0, 0.69, 0.95);
                                cc.ShowText("7");
                            }
                            else if (gameField.Field[i, j] == 8)
                            {
                                cc.SetSourceRGB(0, 0, 0);
                                cc.ShowText("8");
                            }
                        }
                        else if (gameField.Field[i, j] == -1 && !gameField.IsGame)
                        {
                            if (gameField.IsLost)
                                cc.SetSourceRGB(1, 0.5, 0.84);
                            else
                                cc.SetSourceRGBA(0.5, 0.5, 1, 0.8);

                            cc.Fill();

                            PutBombImage(cc, gameField, SCREEN_WIDTH, (j - 1) * (CellSize + 1) + ScaleCoordX, (i - 1) * (CellSize + 1) + 17);
                        }
                        else if (gameField.Field[i, j] == -1 && gameField.IsGame)
                        {
                            if (PutUpFlagsActive)
                                cc.SetSourceRGB(0, 0.5, 0.4); /*======*/
                            else
                                cc.SetSourceRGBA(0.5, 0.5, 1, 0.8);
                            cc.Fill();
                        }
                        if (gameField.FlagMap[i, j] == 0 && gameField.IsGame)
                        {
                            PutFlagImage(cc, gameField, SCREEN_WIDTH, (j - 1) * (CellSize + 1) + ScaleCoordX, (i - 1) * (CellSize + 1) + 17);
                        }
                    }
                }
            }
            else if (gameField.gameMode == GameMode.HexagonModeEasy || gameField.gameMode == GameMode.HexagonModeMedium || gameField.gameMode == GameMode.HexagonModeHard)
            {
                for (int i = 1; i < gameField.NumberOfCells + 1; i++)
                {
                    for (int j = 1; j < gameField.NumberOfCells + 1; j++)
                    {
                        for (int k = 0; k < 7; k++)
                        {
                            if (i % 2 != 0)
                                cc.LineTo(CellSize * (gameField.HexagonCoords[k, 1] + (j - 1) * 2) + ScaleCoordX, CellSize * (gameField.HexagonCoords[k, 0] + 1.75 * (i - 1)) + ScaleCoordY);
                            else
                                cc.LineTo(CellSize * (gameField.HexagonCoords[k, 1] + (j - 1) * 2 + 1) + ScaleCoordX, CellSize * (gameField.HexagonCoords[k, 0] + 1.75 * (i - 1)) + ScaleCoordY);
                        }
                        if (gameField.Field[i, j] == 10)
                        {
                            if (PutUpFlagsActive && gameField.IsGame)
                                cc.SetSourceRGB(0, 0.5, 0.4);
                            else
                                cc.SetSourceRGBA(0.5, 0.5, 1, 0.8);

                            cc.Fill();
                        }
                        else if (gameField.Field[i, j] > -1)
                        {
                            cc.SetSourceRGBA(1, 1, 1, 1);
                            cc.Fill();

                            if (i % 2 != 0)
                                cc.MoveTo(CellSize * ((j - 1) * 2 + 0.43) + ScaleCoordX, CellSize * ((i - 1) * 1.75 + 1.35) +  ScaleCoordY);
                            else
                                cc.MoveTo(CellSize * ((j - 1) * 2 + 1.43) + ScaleCoordX, CellSize * ((i - 1) * 1.75 + 1.35) + ScaleCoordY);

                            gameField.FlagMap[i, j] = 10;

                            if (gameField.Field[i, j] == 1)
                            {
                                cc.SetSourceRGB(0, 0, 1);
                                cc.ShowText("1");
                            }
                            else if (gameField.Field[i, j] == 2)
                            {
                                cc.SetSourceRGB(0, 0.4, 0);
                                cc.ShowText("2");
                            }
                            else if (gameField.Field[i, j] == 3)
                            {
                                cc.SetSourceRGB(1, 0, 0);
                                cc.ShowText("3");
                            }
                            else if (gameField.Field[i, j] == 4)
                            {
                                cc.SetSourceRGB(0, 0, 0.5);
                                cc.ShowText("4");
                            }
                            else if (gameField.Field[i, j] == 5)
                            {
                                cc.SetSourceRGB(0.5, 0, 0);
                                cc.ShowText("5");
                            }
                            else if (gameField.Field[i, j] == 6)
                            {
                                cc.SetSourceRGB(0.38, 0.29, 0.49);
                                cc.ShowText("6");
                            }
                        }
                        else if (gameField.Field[i, j] == -1 && !gameField.IsGame)
                        {
                            if (gameField.IsLost)
                                cc.SetSourceRGB(1, 0.5, 0.84);
                            else
                                cc.SetSourceRGBA(0.5, 0.5, 1, 0.8);

                            cc.Fill();

                            if (i % 2 != 0)
                                PutBombImage(cc, gameField, SCREEN_WIDTH, CellSize * ((j - 1) * 2 + 0.15) + ScaleCoordX, CellSize * ((i - 1) * 1.75 + 0.2) + ScaleCoordY);
                            else
                                PutBombImage(cc, gameField, SCREEN_WIDTH, CellSize * ((j - 1) * 2 + 1.15) + ScaleCoordX, CellSize * ((i - 1) * 1.75 + 0.2) + ScaleCoordY);
                        }
                        else if (gameField.Field[i, j] == -1 && gameField.IsGame)
                        {
                            if (PutUpFlagsActive)
                                cc.SetSourceRGB(0, 0.5, 0.4); /*======*/
                            else
                                cc.SetSourceRGBA(0.5, 0.5, 1, 0.8);
                            cc.Fill();
                        }
                        if (gameField.FlagMap[i, j] == 0 && gameField.IsGame)
                        {
                            if (i % 2 != 0)
                                PutFlagImage(cc, gameField, SCREEN_WIDTH, CellSize * ((j - 1) * 2 + 0.25) + ScaleCoordX, CellSize * ((i - 1) * 1.75 + 0.25) + ScaleCoordY);
                            else
                                PutFlagImage(cc, gameField, SCREEN_WIDTH, CellSize * ((j - 1) * 2 + 1.25) + ScaleCoordX, CellSize * ((i - 1) * 1.75 + 0.25) + ScaleCoordY);
                        }
                    }
                }
            }
        }

        public void DrawSelectionFrame(Context cc, GameField gameField, int choosenX, int choosenY, ref bool isChoosen)
        {
            if (gameField.gameMode == GameMode.ClassicModeEasy || gameField.gameMode == GameMode.ClassicModeMedium || gameField.gameMode == GameMode.ClassicModeHard)
            {
                if (choosenX < 0 || choosenY < 0 || choosenX >= gameField.NumberOfCells || choosenY >= gameField.NumberOfCells || !isChoosen || PutUpFlagsActive)
                    return;

                cc.Rectangle(new PointD(choosenX * (CellSize + 1) + ScaleCoordX, choosenY * (CellSize + 1) + 17), CellSize, CellSize);

                if (gameField.Field[choosenY + 1, choosenX + 1] != -1)
                    cc.SetSourceRGB(0, 1, 0.5);
                else
                    cc.SetSourceRGB(1, 0, 0);

                cc.StrokePreserve();
                isChoosen = false;
            }
            else if (gameField.gameMode == GameMode.HexagonModeEasy || gameField.gameMode == GameMode.HexagonModeMedium || gameField.gameMode == GameMode.HexagonModeHard)
            {
                if (choosenX < 0 || choosenY < 0 || choosenX >= gameField.NumberOfCells || choosenY >= gameField.NumberOfCells || !isChoosen || PutUpFlagsActive)
                    return;

                cc.NewPath();
                if (choosenY % 2 == 0)
                {
                    for (int k = 0; k < 7; k++)
                    {
                        cc.LineTo(CellSize * (gameField.HexagonCoords[k, 1] + choosenX * 2) + ScaleCoordX, CellSize * (gameField.HexagonCoords[k, 0] + choosenY * 1.75) + ScaleCoordY);
                    }
                }
                else
                {
                    for (int k = 0; k < 7; k++)
                    {
                        cc.LineTo(CellSize * (gameField.HexagonCoords[k, 1] + choosenX * 2 + 1) + ScaleCoordX, CellSize * (gameField.HexagonCoords[k, 0] + choosenY * 1.75) + ScaleCoordY);
                    }
                }

                if (gameField.Field[choosenY + 1, choosenX + 1] != -1)
                    cc.SetSourceRGB(0, 1, 0.5);
                else
                    cc.SetSourceRGB(1, 0, 0);

                cc.StrokePreserve();
                isChoosen = false;
            }
        }

        public void DrawGameOverAnimation(Context cc, GameField gameField, int SCREEN_WIDTH, int SCREEN_HEIGHT)
        {
            if (gameField.IsWon)
            {
                int x = SCREEN_WIDTH / 2;
                int y = SCREEN_HEIGHT / 2;

                if (Size > 0)
                {
                    Size -= 3.8;

                    if (Size < 380)
                    {
                        Alpha -= 0.01;
                    }

                    cc.SetFontSize(Size);
                    cc.SetSourceRGB(0.2, 0.6, 0.2);

                    TextExtents extents = cc.TextExtents("You Won!");

                    cc.MoveTo(x - extents.Width / 2, y);
                    cc.TextPath("You won!");
                    cc.Clip();
                    cc.Stroke();
                    cc.PaintWithAlpha(Alpha);
                }
            }
            else if (gameField.IsLost)
            {
                int x = SCREEN_WIDTH / 2;
                int y = SCREEN_HEIGHT / 2;

                if (Size > 0)
                {
                    Size -= 3.8;

                    if (Size < 380)
                    {
                        Alpha -= 0.01;
                    }

                    cc.SetFontSize(Size);
                    cc.SetSourceRGB(1, 0.6, 0.2);

                    TextExtents extents = cc.TextExtents("Game Over");

                    cc.MoveTo(x - extents.Width / 2, y);
                    cc.TextPath("Game Over");
                    cc.Clip();
                    cc.Stroke();
                    cc.PaintWithAlpha(Alpha);
                }
            }
        }
    }
}
