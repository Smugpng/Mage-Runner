using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Healthbar : MonoBehaviour
{
    [Header("Health Bar Section")]
    public Canvas PlayerCanvas;
    public HealthSystem healthSystem;
    [SerializeField] private UnityEngine.UI.Image _healthBar;
    public float healthBarStart, healthbarCurrent;
    private float lastHp;

    [Header("Spell Section")]
    public WeaponScript WeaponScript;
    [SerializeField] private UnityEngine.UI.Image _nextSpell, _queuedSpell, _chargeUI;
    public List<Sprite> spellChoices;
    public int weaponCounterimage;
    public int nextWeaponCounterimage;
    private float _chargedSpellCount;

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
            if (PlayerCanvas == null)
            {
                Debug.Log("Can't find player canvas");
            }
        }
        #endregion

        #region Spell UI
        WeaponScript = GetComponentInChildren<WeaponScript>();
        weaponCounterimage = WeaponScript.weaponCounter;
        nextWeaponCounterimage = WeaponScript.nextWeaponCounter;
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

        weaponCounterimage = WeaponScript.weaponCounter;
        nextWeaponCounterimage = WeaponScript.nextWeaponCounter;
        _nextSpell.sprite = spellChoices[weaponCounterimage];
        _queuedSpell.sprite = spellChoices[nextWeaponCounterimage];

        _chargedSpellCount = WeaponScript.chargeTime;
        UpdateChargeBar();
    }
    public void UpdateHealthBar()
    {
        if (_healthBar != null)
        {
            _healthBar.fillAmount = healthbarCurrent / healthBarStart;
        }
    }
    public void UpdateChargeBar()
    {
        if (_chargeUI != null)
        {
            _chargeUI.fillAmount = _chargedSpellCount /2;
        }
    }
    private IEnumerator TurnOffHB()
    {
        yield return new WaitForSeconds(4);
        _healthBar.enabled = false;
    }
}
