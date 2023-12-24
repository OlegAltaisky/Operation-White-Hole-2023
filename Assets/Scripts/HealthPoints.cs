using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class HealthPoints : MonoBehaviour
{
    [SerializeField] private UnityEvent OnDie;
    [SerializeField] private UnityEvent OnDie2;
    [SerializeField] private int healthMax = 10;
    [SerializeField] private int healthCurrent;
    [SerializeField] private Text healthCurrentText;
    [SerializeField] private bool useText;

    private void Start()
    {
        healthCurrent = healthMax;
        
        if (useText)
        {
            healthCurrentText.text = "HP: " + healthCurrent;
        }
    }

    public void TakeDamage(int damage)
    {
        healthCurrent = Mathf.Clamp(healthCurrent - damage, 0, healthMax);

        if(useText)
        {
            healthCurrentText.text = "HP: " + healthCurrent;
        }
        
        if(healthCurrent == 0)
        {
            StartCoroutine(Die());
        }
    }

    public void Heal()
    {
        healthCurrent = healthMax;
        healthCurrentText.text = "HP: " + healthCurrent;
    }

    private IEnumerator Die()
    {
        GetComponent<Animator>().SetBool("Death", true);
        OnDie.Invoke();
        yield return new WaitForSeconds(3.0f);
        OnDie2.Invoke();
        Destroy(this.gameObject, 3.0f);
    }
}
