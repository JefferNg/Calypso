using System.Collections;
using UnityEngine;

public class WeaponBurst : MonoBehaviour
{
    private Collider2D colliderWeapon;
    public Animator animator;
    private bool inAttack = false;
    private PlayerMovement playerMovement;

    void Start()
    {
        colliderWeapon = GetComponent<Collider2D>();
        colliderWeapon.enabled = false;

        playerMovement = GetComponentInParent<PlayerMovement>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !inAttack && playerMovement != null && !playerMovement.isKnockedBack)
        {
            SoundManager.instance.weaponSound();
            StartCoroutine(PerformAttack());
        }
    }

    private IEnumerator PerformAttack()
    {
        inAttack = true;

        animator.SetBool("AttackOccured", true);

        yield return new WaitForSeconds(0.1f);
        colliderWeapon.enabled = true;

        yield return new WaitForSeconds(0.1f);

        colliderWeapon.enabled = false;
        animator.SetBool("AttackOccured", false);
        inAttack = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) {
            SoundManager.instance.skeletonDeathSound();
            Destroy(collision.gameObject);
        }
       
    }
}
