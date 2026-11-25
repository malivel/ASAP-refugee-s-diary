using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
[RequireComponent(typeof(Image))]
public class BookCheckbox : MonoBehaviour
{
    [Header("Спрайты галочки")]
    [SerializeField] private Sprite uncheckedSprite;
    [SerializeField] private Sprite checkedSprite;

    [Header("Правильность ответа")]
    [SerializeField] private bool isCorrect;

    public bool IsCorrect => isCorrect;
    public bool IsChecked { get; private set; }

    public event Action<BookCheckbox> Clicked;

    private Image image;
    private Button button;
    private bool initialized;

    public void Init()
    {
        if (initialized) return;
        initialized = true;

        image = GetComponent<Image>();
        button = GetComponent<Button>();

        button.onClick.AddListener(OnClick);

        SetChecked(false);
    }

    private void OnClick()
    {
        Clicked?.Invoke(this);
    }

    public void SetChecked(bool value)
    {
        IsChecked = value;
        image.sprite = value
            ? checkedSprite
            : uncheckedSprite;
    }

    public void SetInteractable(bool value)
    {
        button.interactable = value;
    }
}