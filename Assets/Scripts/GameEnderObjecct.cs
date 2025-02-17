using UnityEngine;

public class GameMenuTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the player collides with the sprite
        if (other.CompareTag("Player"))
        {
            SoundManager.instance.victorySound();
            GameManager.instance.ToggleGameMenu();
            Time.timeScale = 0;
        }
    }
}
