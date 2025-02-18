using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject ui; 
    [SerializeField] private GameObject gameOverUi;
    [SerializeField] private GameObject endState;
    private int _enemies;
    public bool inMenu = false;

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
        _enemies = GameObject.FindGameObjectsWithTag("Enemy").Length;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) { 
            ToggleMenu();
            inMenu = true;
            Time.timeScale = 0f;
        }
    }

    public void EnemyUpdate()
    {
        _enemies--;
        if (_enemies == 0)
        {
            endState.GetComponent<Collider2D>().isTrigger = true;
        }
    }

    void ToggleMenu()
    {
        ui.SetActive(true);
    }

    public void Resume()
    {
        ui.SetActive(false);
        Time.timeScale = 1f;
        inMenu = false;
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
