using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Healthbar : MonoBehaviour
{
    public HealthSystem healthSystem;
    [SerializeField] private UnityEngine.UI.Image _healthBar;
    public float healthBarStart, healthbarCurrent;
    private float lastHp;
    // Start is called before the first frame update
    void Start()
    {
        healthSystem = GetComponent<HealthSystem>();
        healthBarStart = healthSystem.health;
        lastHp = healthSystem.health;
        _healthBar.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        healthbarCurrent = healthSystem.health;
        UpdateHealthBar();
        if (lastHp != healthbarCurrent)
        {
            lastHp = healthbarCurrent;
            _healthBar.enabled = true;
            StartCoroutine(TurnOffHB());
        }
    }
    public void UpdateHealthBar()
    {
        if (_healthBar != null)
        {
            _healthBar.fillAmount = healthbarCurrent / healthBarStart;
        }
    }
    private IEnumerator TurnOffHB()
    {
        yield return new WaitForSeconds(4);
        _healthBar.enabled = false;
    }
}
