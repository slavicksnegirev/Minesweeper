using System;
using System.Drawing;
using Size = System.Drawing.Size;
using Cairo;

namespace Minesweeper
{
    public class ImageProcessing : GameLogic
    {
        private double _cellSize = -1;
        private double _cellSizeY = -1;
        
        private int _screenWidthBeforeResize = 700;

        private GameMode _gameModeBeforeChange = GameMode.None;

        ImageSurface surface;

        public double CellSize
        {
            get => _cellSize;
            set
            {
                _cellSize = value;
                if (_cellSize < 0)
                {
                    _cellSize = -1;
                }
            }
        }

        public double CellSizeY
        {
            get => _cellSizeY;
            set
            {
                _cellSizeY = value;
                if (_cellSizeY < 0)
                {
                    _cellSizeY = -1;
                }
            }
        }

        public int ScreenWidthBeforeResize
        {
            get => _screenWidthBeforeResize;
            set
            {
                _screenWidthBeforeResize = value;
                if (_screenWidthBeforeResize < 0)
                {
                    _screenWidthBeforeResize = 700;
                }
            }
        }

        public GameMode gameModeBeforeChange
        {
            get => _gameModeBeforeChange;
            set => _gameModeBeforeChange = value;
        }

        protected void ResizeImage(GameField gameField, int SCREEN_WIDTH)
        {
            while ((ScreenWidthBeforeResize != SCREEN_WIDTH) || (gameField.gameMode != gameModeBeforeChange))
            {
                var bitmap = new Bitmap("orangeFlag.png");
                bitmap = ResizeBitmap(bitmap, gameField);
                bitmap.Save("newOrangeFlag.png");

                bitmap = new Bitmap("bomb.png");
                bitmap = ResizeBitmap(bitmap, gameField);
                bitmap.Save("newBomb.png");

                ScreenWidthBeforeResize = SCREEN_WIDTH;
                gameModeBeforeChange = gameField.gameMode;
            }
        }

        protected void PutFlagImage(Context cc, GameField gameField, int SCREEN_WIDTH, double xPos, double yPos)
        {
            ResizeImage(gameField, SCREEN_WIDTH);
            surface = new ImageSurface("newOrangeFlag.png");
            cc.SetSourceSurface(surface, (int)xPos, (int)yPos);
            cc.Paint();
            surface.Dispose();
        }

        protected void PutBombImage(Context cc, GameField gameField, int SCREEN_WIDTH, double xPos, double yPos)
        {
            ResizeImage(gameField, SCREEN_WIDTH);
            surface = new ImageSurface("newBomb.png");
            cc.SetSourceSurface(surface, (int)xPos, (int)yPos);
            cc.Paint();
            surface.Dispose();
        }

        protected Bitmap ResizeBitmap(Bitmap bitmap, GameField gameField)
        {
            if (gameField.gameMode == GameMode.ClassicModeEasy || gameField.gameMode == GameMode.ClassicModeMedium || gameField.gameMode == GameMode.ClassicModeHard)
            {
                bitmap = new Bitmap(bitmap, new Size((int)CellSize, (int)CellSize));
            }
            else if (gameField.gameMode == GameMode.HexagonModeEasy || gameField.gameMode == GameMode.HexagonModeMedium || gameField.gameMode == GameMode.HexagonModeHard)
            {
                bitmap = new Bitmap(bitmap, new Size((int)(CellSize * 1.5), (int)(CellSize * 1.5)));
            }

            return bitmap;
        }
    }
}
