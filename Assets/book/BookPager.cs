using System;
using System.Collections.Generic;
using UnityEngine;

public class BookPage : MonoBehaviour
{
    [Header("Варианты ответов")]
    [SerializeField]
    private List<BookCheckbox> checkboxes =
        new List<BookCheckbox>();

    public bool IsAnswered { get; private set; }
    public bool IsCorrect { get; private set; }
    public bool IsLocked { get; private set; }

    public event Action<BookPage> Answered;

    private bool initialized;

    public void Init()
    {
        if (initialized) return;
        initialized = true;

        foreach (var cb in checkboxes)
        {
            cb.Init();
            cb.Clicked += OnCheckboxClicked;
        }
    }

    private void OnCheckboxClicked(BookCheckbox clicked)
    {
        // Повторный выбор запрещён
        if (IsLocked) return;

        // Отмечаем только выбранный вариант
        foreach (var cb in checkboxes)
        {
            cb.SetChecked(cb == clicked);
        }

        IsAnswered = true;
        IsCorrect = clicked.IsCorrect;
        IsLocked = true;

        // Отключаем все варианты ответа
        foreach (var cb in checkboxes)
        {
            cb.SetInteractable(false);
        }

        // Сообщаем менеджеру об ответе
        Answered?.Invoke(this);
    }
}