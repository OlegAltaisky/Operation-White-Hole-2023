using System.Collections;
using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    private Animator animator;
    
    [Header("Rocket spawn settings")]
    [SerializeField] private GameObject rocketPrefab;
    [SerializeField] private float offset = 2.75f;

    [Header("Time")]
    [SerializeField] private float timeBetweenShoots = 0.5f;
    [SerializeField] private float timeUntilDestroy = 10.0f;
    private Coroutine coroutine;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Shoot()
    {
        if(coroutine == null)
        {
            coroutine = StartCoroutine(ShootRocket());
        }
    }

    private IEnumerator ShootRocket()
    {
        animator.SetTrigger("Attack");
        GameObject rocket = Instantiate(rocketPrefab, transform.position + transform.up * offset, transform.rotation); ;
        yield return new WaitForSeconds(timeBetweenShoots);
        Destroy(rocket, timeUntilDestroy);
        coroutine = null;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(transform.position + transform.up * offset, 0.2f);
    }
}
