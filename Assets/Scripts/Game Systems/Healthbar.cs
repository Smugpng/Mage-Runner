using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class healthbar : MonoBehaviour
{
    [Header("Health Bar Section")]
    public Canvas PlayerCanvas;
    public HealthSystem healthSystem;
    [SerializeField] private UnityEngine.UI.Image _healthBar;
    public float healthBarStart, healthbarCurrent;
    private float lastHp;

    [Header("Spell Section")]
    [SerializeField] private UnityEngine.UI.Image _nextSpell, _queuedSpell;
    public WeaponScript WeaponScript;

    // Start is called before the first frame update
    void Start()
    {
        #region healthbarStart
        healthSystem = GetComponent<HealthSystem>();
        healthBarStart = healthSystem.health;
        lastHp = healthSystem.health;
        _healthBar.enabled = false;
        GameObject tempObject = GameObject.Find("UI");
        if (tempObject != null)
        {
            PlayerCanvas = tempObject.GetComponentInChildren<Canvas>();
            if (PlayerCanvas == null ) 
            {
                Debug.Log("Can't find player canvas");
            }
        }
        #endregion

        #region Spell UI
        WeaponScript = GetComponent<WeaponScript>();
        #endregion
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
        PlayerCanvas.transform.position = this.transform.position;
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
