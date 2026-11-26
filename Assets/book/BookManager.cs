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

    private bool[] unlocked;
    private int currentPage = 0;
    private bool endingStarted = false;

    private void Start()
    {
        unlocked = new bool[pages.Length];

        bookButton.onClick.AddListener(ToggleBook);
        prevButton.onClick.AddListener(PrevPage);
        nextButton.onClick.AddListener(NextPage);
        closeButton.onClick.AddListener(CloseBook);

        foreach (var page in pages)
        {
            page.Init();
            page.Answered += OnPageAnswered;
            page.gameObject.SetActive(false);
        }

        bookPanel.SetActive(false);
        UpdateIcon();
    }

    // ===== Публичный API =====

    // Открывает конкретную страницу по индексу
    public void UnlockPage(int index)
    {
        if (endingStarted) return;
        if (index < 0 || index >= pages.Length)
        {
            Debug.LogWarning($"BookManager: неверный индекс страницы {index}");
            return;
        }
        if (unlocked[index]) return;

        unlocked[index] = true;
        currentPage = index;

        UpdateIcon();

        if (bookPanel.activeSelf)
            UpdateView();
    }

    // На случай, если где-то ещё остался старый вызов —
    // открывает первую неоткрытую страницу (по порядку).
    [System.Obsolete("Используй UnlockPage(index) — открывай нужную страницу напрямую.")]
    public void UnlockNextPage()
    {
        for (int i = 0; i < unlocked.Length; i++)
        {
            if (!unlocked[i])
            {
                UnlockPage(i);
                return;
            }
        }
    }

    public void ToggleBook()
    {
        if (bookPanel.activeSelf) CloseBook();
        else OpenBook();
    }

    public void OpenBook()
    {
        bookPanel.SetActive(true);

        // Если текущая страница не открыта — прыгаем на первую открытую
        if (!unlocked[currentPage])
        {
            int first = FindNextUnlocked(-1);
            if (first >= 0) currentPage = first;
        }

        UpdateView();
    }

    public void CloseBook()
    {
        bookPanel.SetActive(false);
    }

    public void NextPage()
    {
        int next = FindNextUnlocked(currentPage);
        if (next < 0) return;

        audioSource.Play();
        currentPage = next;
        UpdateView();
    }

    public void PrevPage()
    {
        int prev = FindPrevUnlocked(currentPage);
        if (prev < 0) return;

        audioSource.Play();
        currentPage = prev;
        UpdateView();
    }

    // ===== Проверки состояния =====

    public bool AllPagesAnswered()
    {
        for (int i = 0; i < pages.Length; i++)
        {
            if (!unlocked[i]) return false;
            if (!pages[i].IsAnswered) return false;
        }
        return true;
    }

    public bool AllAnswersCorrect()
    {
        if (!AllPagesAnswered()) return false;

        foreach (var page in pages)
        {
            if (!page.IsCorrect) return false;
        }
        return true;
    }

    // ===== Внутренняя логика =====

    private void UpdateView()
    {
        for (int i = 0; i < pages.Length; i++)
        {
            pages[i].gameObject.SetActive(i == currentPage && unlocked[i]);
        }
        prevButton.interactable = FindPrevUnlocked(currentPage) >= 0;
        nextButton.interactable = FindNextUnlocked(currentPage) >= 0;
    }

    private void UpdateIcon()
    {
        bool anyUnlocked = false;
        for (int i = 0; i < unlocked.Length; i++)
        {
            if (unlocked[i]) { anyUnlocked = true; break; }
        }

        bookButtonImage.sprite = anyUnlocked ? filledBookSprite : emptyBookSprite;
    }

    private int FindPrevUnlocked(int from)
    {
        for (int i = from - 1; i >= 0; i--)
            if (unlocked[i]) return i;
        return -1;
    }

    private int FindNextUnlocked(int from)
    {
        for (int i = from + 1; i < pages.Length; i++)
            if (unlocked[i]) return i;
        return -1;
    }

    private void OnPageAnswered(BookPage page)
    {
        if (endingStarted) return;
        if (!AllPagesAnswered()) return;

        endingStarted = true;

        if (AllAnswersCorrect())
            SceneManager.LoadScene(goodEndingScene);
        else
            SceneManager.LoadScene(badEndingScene);
    }

    private void OnDestroy()
    {
        if (pages == null) return;

        foreach (var page in pages)
        {
            if (page != null)
                page.Answered -= OnPageAnswered;
        }
    }
}