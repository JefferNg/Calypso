using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip walkingEffect;
    public AudioClip jumpEffect;
    public AudioClip doubleJumpEffect;
    public AudioClip screamEffect;
    public AudioClip weaponEffect;
    public AudioClip skeletonDeathEffect;
    public AudioClip victoryEffect;
    public AudioClip loseEffect;

    [SerializeField] AudioSource walkSource;
    [SerializeField] AudioSource jumpSource;

    public static SoundManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        } else
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
        
    }

    public void walkingSound()
    {
        walkSource.PlayOneShot(walkingEffect);
    }

    public void jumpSound()
    {
        jumpSource.PlayOneShot(jumpEffect);
    }

    public void doubleJumpSound()
    {
        jumpSource.PlayOneShot(doubleJumpEffect);
    }
    public void screamSound()
    {
        jumpSource.PlayOneShot(screamEffect);
    }
    public void weaponSound()
    {
        jumpSource.PlayOneShot(weaponEffect);
    }

    public void skeletonDeathSound()
    {
        jumpSource.PlayOneShot(skeletonDeathEffect);
    }

    public void victorySound()
    {
        jumpSource.PlayOneShot(victoryEffect);
    }

    public void loseSound()
    {
        jumpSource.PlayOneShot(loseEffect);
    }

    public void StopWalkingSound()
    {
        walkSource.Stop();
    }
}
