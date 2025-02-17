using UnityEngine;
using UnityEngine.SceneManagement;

public class UIButtonBehavior : MonoBehaviour
{
    [SerializeField] private GameObject _startButton;
    [SerializeField] private GameObject _backButton;
    [SerializeField] private GameObject _creditsButton;
    [SerializeField] private GameObject _quitButton;

    [SerializeField] private GameObject _title;
    [SerializeField] private GameObject _credits;

    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Time.timeScale = 1;
    }

    public void Credits()
    {
        _startButton.SetActive(false);
        _backButton.SetActive(true);
        _creditsButton.SetActive(false);
        _quitButton.SetActive(false);
        _title.SetActive(false);
        _credits.SetActive(true);
    }

    public void Back()
    {
        _startButton.SetActive(true);
        _backButton.SetActive(false);
        _creditsButton.SetActive(true);
        _quitButton.SetActive(true);
        _title.SetActive(true);
        _credits.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
