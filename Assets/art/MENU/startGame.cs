using UnityEngine;
using UnityEngine.SceneManagement; // Обязательно добавляем это для работы со сценами

public class MenuManager : MonoBehaviour
{
    public AudioSource audioSource;
    // Метод для переключения сцены
    public void ChangeScene(string sceneName)
    {
        audioSource.Play();
        // Загружаем сцену по её имени
        SceneManager.LoadScene(sceneName);
    }

    // Метод для выхода из игры
    public void QuitGame()
    {
        audioSource.Play();
        Debug.Log("Игра закрывается..."); // Это сообщение появится в консоли
        Application.Quit();

        // Этот блок нужен только для того, чтобы выход работал прямо в редакторе Unity
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}