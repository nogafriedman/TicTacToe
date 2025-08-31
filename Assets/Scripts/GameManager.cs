using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Scene References")]
    public TicTacToeCell[] cellViews;      // 9 cells with indices set 0..8
    public TextMeshProUGUI winText;
    public Button restartButton;

    [Header("Config")]
    public Mark startingPlayer = Mark.X;

    private TicTacToeBoard board = new TicTacToeBoard();
    private Mark currentPlayer;
    private bool gameOver;

    void Start()
    {
        foreach (var cell in cellViews)
        {
            cell.onClicked.AddListener(HandleCellClicked);
        }

        restartButton.onClick.AddListener(ResetBoard);
        ResetBoard();
    }

    private void HandleCellClicked(int idx)
    {
        if (gameOver) return;
        if (!board.IsEmpty(idx)) return;

        // Update board state
        if (!board.Place(idx, currentPlayer)) return;

        // Update cell view
        cellViews[idx].SetMark(currentPlayer);

        if (board.HasWinner(out var winner))
        {
            winText.text = $"{winner} wins!";
            gameOver = true;
            restartButton.gameObject.SetActive(true);
            return;
        }

        if (board.IsFull())
        {
            winText.text = "Tie!";
            gameOver = true;
            restartButton.gameObject.SetActive(true);
            return;
        }

        currentPlayer = (currentPlayer == Mark.X) ? Mark.O : Mark.X;
    }

    private void ResetBoard()
    {
        board.Reset();
        foreach (var cell in cellViews)
            cell.Clear();

        currentPlayer = startingPlayer;
        winText.text = "";
        gameOver = false;
        restartButton.gameObject.SetActive(false);
    }
}
