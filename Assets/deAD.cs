using UnityEngine;
using UnityEngine.SceneManagement;

public class deAD : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        death();
    }

    public void death()
    {
        SceneManager.LoadScene("deathEnd");
    }
}