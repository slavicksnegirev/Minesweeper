using Cairo;

namespace Minesweeper
{
    public interface IGameplay
    {
        void GenerateField(int[,] array);
        void GenerateMines();
        void CountFlags();

        void SetScaleForCoordX(GameField gameField, double cellSize, int SCREEN_WIDTH);
        void SetScaleForCoordY(GameField gameField, double cellSize, int SCREEN_HEIGHT);
        void DrawField(Context cc, GameField gameField, int SCREEN_WIDTH);
        void DrawSelectionFrame(Context cc, GameField gameField, int choosenX, int choosenY, ref bool isChoosen);

        void FindMines(GameField gameField, GameMode gameMode, int y, int x);
        void CheckMinesAround(GameField gameField, GameMode gameMode, int y, int x);
    }
}
