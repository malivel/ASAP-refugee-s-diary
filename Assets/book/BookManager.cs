using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BookManager : MonoBehaviour
{
    public AudioSource audioSource;
    [Header("UI")]
    [SerializeField] private Button bookButton;
    [SerializeField] private Image bookButtonImage;
    [SerializeField] private GameObject bookPanel;

    [Header("Иконки книги")]
    [SerializeField] private Sprite emptyBookSprite;
    [SerializeField] private Sprite filledBookSprite;

    [Header("Навигация")]
    [SerializeField] private Button prevButton;
    [SerializeField] private Button nextButton;
    [SerializeField] private Button closeButton;

    [Header("Страницы (6 штук)")]
    [SerializeField] private BookPage[] pages;

    [Header("Сцены концовок")]
    [SerializeField] private string goodEndingScene = "GoodEnding";
    [SerializeField] private string badEndingScene = "BadEnding";

    private int unlockedCount = 0;
    private int currentPage = 0;

    private bool endingStarted = false;

    private void Start()
    {
        bookButton.onClick.AddListener(ToggleBook);
        prevButton.onClick.AddListener(PrevPage);
        nextButton.onClick.AddListener(NextPage);
        closeButton.onClick.AddListener(CloseBook);

        foreach (var page in pages)
        {
            page.Init();

            // Подписываемся на ответ страницы
            page.Answered += OnPageAnswered;

            page.gameObject.SetActive(false);
        }

        bookPanel.SetActive(false);
        UpdateIcon();
    }

    // Вызывается при игровом событии
    public void UnlockNextPage()
    {
        if (endingStarted) return;
        if (unlockedCount >= pages.Length) return;

        unlockedCount++;

        currentPage = unlockedCount - 1;

        UpdateIcon();

        if (bookPanel.activeSelf)
        {
            UpdateView();
        }
    }

    public void ToggleBook()
    {
        if (bookPanel.activeSelf)
            CloseBook();
        else
            OpenBook();
    }

    public void OpenBook()
    {
        bookPanel.SetActive(true);
        UpdateView();
    }

    public void CloseBook()
    {
        bookPanel.SetActive(false);
    }

    public void NextPage()
    {
        if (currentPage < unlockedCount - 1)
        {
            audioSource.Play();
            currentPage++;
            UpdateView();
        }
    }

    public void PrevPage()
    {
        if (currentPage > 0)
        {
            audioSource.Play();
            currentPage--;
            UpdateView();
        }
    }

    private void UpdateView()
    {
        for (int i = 0; i < pages.Length; i++)
        {
            pages[i].gameObject.SetActive(
                i == currentPage && i < unlockedCount
            );
        }

        prevButton.interactable = currentPage > 0;

        nextButton.interactable =
            currentPage < unlockedCount - 1;
    }

    private void UpdateIcon()
    {
        bookButtonImage.sprite =
            unlockedCount > 0
                ? filledBookSprite
                : emptyBookSprite;
    }

    public bool AllPagesAnswered()
    {
        if (unlockedCount < pages.Length)
            return false;

        foreach (var page in pages)
        {
            if (!page.IsAnswered)
                return false;
        }

        return true;
    }

    public bool AllAnswersCorrect()
    {
        if (!AllPagesAnswered())
            return false;

        foreach (var page in pages)
        {
            if (!page.IsCorrect)
                return false;
        }

        return true;
    }

    // Вызывается после выбора галочки
    private void OnPageAnswered(BookPage page)
    {
        if (endingStarted) return;

        // Ждём заполнения всех страниц
        if (!AllPagesAnswered()) return;

        endingStarted = true;

        // Выбираем концовку
        if (AllAnswersCorrect())
        {
            SceneManager.LoadScene(goodEndingScene);
        }
        else
        {
            SceneManager.LoadScene(badEndingScene);
        }
    }

    private void OnDestroy()
    {
        if (pages == null) return;

        foreach (var page in pages)
        {
            if (page != null)
            {
                page.Answered -= OnPageAnswered;
            }
        }
    }
}