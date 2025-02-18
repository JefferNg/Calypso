using Unity.VisualScripting;
using UnityEngine;

public class TrapBehavior : MonoBehaviour
{
    [SerializeField] private GameObject oldText;
    [SerializeField] private GameObject newText;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.position = new Vector3(8, -1, other.transform.position.z);
            oldText.SetActive(false);
            newText.SetActive(true);
        }
    }
}
