using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SpellScript;

public class ElementalSystem : MonoBehaviour
{
    [SerializeField] private HealthSystem healthSystem;
    [Header("Elemental effects")]
    [SerializeField] private float elementNum = 0;
    [SerializeField] private float elementTimer;
    [SerializeField] private bool isActive; /*isSmoke*/
    private float dmg = 25f;

    [Header("Particle effects")]
    public GameObject onFire, onCold, onWet, onSparked, onMuddy; /*isSmoking*/
    //private float _lastElementUsed, smokeTimer;

    // Start is called before the first frame update
    void Start()
    {
        isActive = false;
       // isSmoking.active = false;
       // isSmoke = false;
        healthSystem = GetComponent<HealthSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (elementTimer >= 5)
        {
            DestroyElements();
        }
       /* if (smokeTimer >= 2)
        {
            isSmoking.active = false;
            isSmoke = false;
        }*/
        ElementInteractions();
        if (isActive)
        {
            elementTimer += Time.deltaTime;
        }
       /*if (isSmoke)
        {
            smokeTimer += Time.deltaTime;
        }*/
    }
    public void ElementType(AttackType type)
    {
        if (type == AttackType.isFire)
        {
            elementNum += 1;
        }
        else if (type == AttackType.isCold)
        {
            elementNum -= 2;
        }
        else if (type == AttackType.isWet)
        {
            elementNum -= 1;
        }
        else if (type == AttackType.isSparked)
        {
            elementNum += 2;
        }
        else if (type == AttackType.isMuddy)
        {
            elementNum = 0;
        }
        
        elementTimer = 0;
        isActive = true;
        
    }
    void DestroyElements()
    {
        
        elementNum = 0;
        isActive = false;
        elementTimer = 0;
        
    }
   /* void SmokeCount()
    {
        isSmoking.active = true;
        isSmoke = true;
    }*/
    void ElementInteractions()
    {
        //Double Damage
        if (elementNum >= 3 || elementNum <= -3) 
        {
            healthSystem.TakeDMG(dmg);
            DestroyElements(); 
        }
       

        //Particle effects
        if (elementNum == 1)
        {
            onFire.SetActive(true);
           // SmokeCount();
        }
        else
        {
            onFire.SetActive(false);
        }
        if (elementNum == 2)
        {
            onSparked.SetActive(true);
            // SmokeCount();
        }
        else
        {
            onSparked.SetActive(false);
        }
        if (elementNum == -1)
        {
            onWet.SetActive(true);
            // SmokeCount();
        }
        else
        {
            onWet.SetActive(false);
        }
        if (elementNum == -2)
        {
            onCold.SetActive(true);
            //SmokeCount();
        }
        else
        {
            onCold.SetActive(false);
        }

    }
}
