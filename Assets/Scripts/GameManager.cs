using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject ui; 
    [SerializeField] private GameObject gameOverUi;

    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) { 
            ToggleMenu();
        }
    }

    void ToggleMenu()
    {
        ui.SetActive(true);
    }

    public void Resume()
    {
        ui.SetActive(false);
    }

    public void ToggleGameMenu()
    {
        gameOverUi.SetActive(true);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
