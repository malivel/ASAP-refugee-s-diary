using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BookAnswerOption
{
    [TextArea(1, 3)] public string text;
    public bool isCorrect;
}

[Serializable]
public class BookPageData
{
    [Tooltip("Необязательное имя страницы для вызова из скриптов: UnlockPage(\"well\")")]
    public string pageId;

    public Sprite illustration;
    [TextArea(2, 6)] public string question;
    public List<BookAnswerOption> answers = new List<BookAnswerOption>();
}