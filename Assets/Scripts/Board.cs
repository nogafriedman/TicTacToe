using UnityEngine;

public class TicTacToeBoard
{
    private Mark[] cells = new Mark[9];

    private static readonly int[,] Wins = new int[,]
    {
        {0,1,2}, {3,4,5}, {6,7,8},
        {0,3,6}, {1,4,7}, {2,5,8},
        {0,4,8}, {2,4,6}
    };

    public void Reset()
    {
        for (int i = 0; i < cells.Length; i++)
            cells[i] = Mark.None;
    }

    public bool IsEmpty(int idx) => cells[idx] == Mark.None;

    public bool Place(int idx, Mark mark)
    {
        if (cells[idx] != Mark.None) return false;
        cells[idx] = mark;
        return true;
    }

    public bool HasWinner(out Mark winner)
    {
        for (int i = 0; i < Wins.GetLength(0); i++)
        {
            int a = Wins[i,0], b = Wins[i,1], c = Wins[i,2];
            if (cells[a] != Mark.None && cells[a] == cells[b] && cells[b] == cells[c])
            {
                winner = cells[a];
                return true;
            }
        }
        winner = Mark.None;
        return false;
    }

    public bool IsFull()
    {
        for (int i = 0; i < cells.Length; i++)
            if (cells[i] == Mark.None) return false;
        return true;
    }

    public Mark GetCell(int idx) => cells[idx];
}
