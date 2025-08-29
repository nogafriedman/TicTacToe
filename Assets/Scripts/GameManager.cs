using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public Button[] cells;
    public Sprite xSprite;
    public Sprite oSprite;
    private string currentPlayer = "X";
    public TextMeshProUGUI winText;
    public Button restartButton;
    private bool gameOver = false;

    void Start()
    {
        foreach (Button cell in cells)
        {
            cell.onClick.AddListener(() => OnCellClicked(cell));
        }

        restartButton.onClick.AddListener(ResetBoard);
        restartButton.gameObject.SetActive(false);
        winText.text = "";
    }

    void OnCellClicked(Button cell)
{
    if (gameOver) return;

    Image cellImage = cell.GetComponent<Image>();
    if (cellImage.sprite != null) return; // already filled

    cellImage.sprite = (currentPlayer == "X") ? xSprite : oSprite;

    if (CheckWinner())
    {
        winText.text = currentPlayer + " wins!";
        restartButton.gameObject.SetActive(true);
        gameOver = true;
        return;
    }

    if (CheckTie())
    {
        winText.text = "Tie!";
        restartButton.gameObject.SetActive(true);
        gameOver = true;
        return;
    }

    currentPlayer = (currentPlayer == "X") ? "O" : "X";
}


    bool CheckWinner()
    {
        int[,] wins = new int[,]
        {
            {0,1,2}, {3,4,5}, {6,7,8},
            {0,3,6}, {1,4,7}, {2,5,8},
            {0,4,8}, {2,4,6}
        };

        for (int i = 0; i < wins.GetLength(0); i++)
        {
            Sprite a = cells[wins[i,0]].GetComponent<Image>().sprite;
            Sprite b = cells[wins[i,1]].GetComponent<Image>().sprite;
            Sprite c = cells[wins[i,2]].GetComponent<Image>().sprite;

            if (a != null && a == b && b == c) return true;
        }
        return false;
    }

    bool CheckTie()
    {
    foreach (Button cell in cells)
    {
        if (cell.GetComponent<Image>().sprite == null)
            return false; 
    }
    return true;
    }


    void ResetBoard()
    {
        foreach (Button cell in cells)
        {
            cell.GetComponent<Image>().sprite = null;
        }
        currentPlayer = "X";
        winText.text = "";
        restartButton.gameObject.SetActive(false);
        gameOver = false;
    }
}
