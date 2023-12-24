using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShooter : MonoBehaviour
{
    private Animator animator;
    
    [Header("Rocket spawn settings")]
    [SerializeField] private GameObject rocketPrefab;
    [SerializeField] private float offset = 1.75f;
    
    [Header("Time")]
    [SerializeField] private float timeBetweenShoots = 0.75f;
    [SerializeField] private float timeUntilDestroy = 5.0f;
    private Coroutine coroutine;
    
    [Header("Ammo setting")]
    [SerializeField] private int ammoMax = 10;
    [SerializeField] private int ammoSupply = 35;
    [SerializeField] private int ammoCurrent;
    [SerializeField] private float reloadTime = 5.0f;
    private float time;

    [Header("Text")]
    [SerializeField] private Text ammoCurrentText;
    [SerializeField] private Text reloadTimeText;


    private void Start()
    {
        animator = GetComponent<Animator>();
        ammoCurrent = ammoMax;
        ammoCurrentText.text = ammoCurrent + "/" + ammoMax + "      в запасе:" + ammoSupply;
    }

    private void Update()
    {
        if(Input.GetButton("Fire1") && coroutine == null && ammoCurrent > 0)
        {
            ammoCurrent -= 1;
            coroutine = StartCoroutine(ShootRocket());
        }

        if(Input.GetButtonDown("Reloading") || Input.GetButtonDown("Fire1") && coroutine == null && ammoCurrent != ammoMax)
        {
            coroutine = StartCoroutine(ReloadRocket());
        }

        if(time > 0)
        {
            reloadTimeText.text = "Reloading... " + Mathf.Round(time);
            time -= Time.deltaTime;
        }
        else
        {
            reloadTimeText.text = "";
        }
    }

    public void GiveAmmo(int ammo)
    {
        ammoSupply += ammo;
        ammoCurrentText.text = ammoCurrent + "/" + ammoMax + "      в запасе:" + ammoSupply;
    }

    private IEnumerator ShootRocket()
    {
        animator.SetTrigger("Attack");
        ammoCurrentText.text = ammoCurrent + "/" + ammoMax + "      в запасе:" + ammoSupply;
        GameObject rocket = Instantiate(rocketPrefab, transform.position + transform.up * offset, transform.rotation);;
        yield return new WaitForSeconds(timeBetweenShoots);
        Destroy(rocket, timeUntilDestroy);
        coroutine = null;
    }

    private IEnumerator ReloadRocket()
    {
        time = reloadTime;
        
        yield return new WaitForSeconds(reloadTime);

        int delta = ammoMax - ammoCurrent;
        
        if (ammoSupply >= delta)
        {
            ammoCurrent = ammoMax;
            ammoSupply -= delta;
        }
        else if(ammoSupply < delta) 
        {
            ammoCurrent += ammoSupply;
            ammoSupply = 0;
        }
        
        ammoCurrentText.text = ammoCurrent + "/" + ammoMax + "      в запасе:" + ammoSupply;
        coroutine = null;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(transform.position + transform.up * offset, 0.2f);
    }
}