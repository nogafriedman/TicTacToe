using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[RequireComponent(typeof(Button), typeof(Image))]
public class TicTacToeCell : MonoBehaviour
{
    public int index;
    public Sprite xSprite;
    public Sprite oSprite;

    public UnityEvent<int> onClicked = new UnityEvent<int>();

    private Image _image;
    private Button _button;
    private Mark _mark = Mark.None;

    void Awake()
    {
        _image = GetComponent<Image>();
        _button = GetComponent<Button>();
        _button.onClick.AddListener(() => onClicked.Invoke(index));
        Render();
    }

    public Mark GetMark() => _mark;

    public void SetMark(Mark mark)
    {
        _mark = mark;
        Render();
        _button.interactable = (_mark == Mark.None);
    }

    public void Clear()
    {
        _mark = Mark.None;
        _button.interactable = true;
        Render();
    }

    private void Render()
    {
        switch (_mark)
        {
            case Mark.X: _image.sprite = xSprite; break;
            case Mark.O: _image.sprite = oSprite; break;
            default:     _image.sprite = null;    break;
        }
    }
}
