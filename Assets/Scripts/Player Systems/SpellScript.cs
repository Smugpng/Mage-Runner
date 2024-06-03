using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellScript : MonoBehaviour
{

    
    public GameObject HitPrefab;
    public bool isMissile;
    public bool isHitmark;
    public float deathTimer;
    public bool isMelee;
    private Transform spawn;
    bool isActive = false;
    [Header("Element Effects")]
    public AttackType attackType;
    [Header("Damage Type")]
    [SerializeField] public DamageType dmgType;
    [SerializeField] private float dmg;
    
    public enum AttackType //type of Attack
    {
        isFire, isCold, isWet, isSparked, isMuddy
    }
    public enum DamageType //Amount of damage dealt
    {
        weak = 25, medium = 50, strong = 100
    }
    
    private void Start()
    {
        spawn = GetComponent<Transform>();
       
        if (isMelee)
        {
            StartCoroutine(DestroyAfterDelay(deathTimer));
        }
        if (isHitmark)
        {
            StartCoroutine(DestroyAfterDelay(deathTimer));
        }
        dmg = Convert.ToSingle(dmgType);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
       if (!isActive)
        {
            isActive = true;
            Rigidbody2D otherRigidbody = other.GetComponent<Rigidbody2D>();
            if (isMissile)
            {

                Instantiate(HitPrefab, transform.position, transform.rotation);
                Destroy(this.gameObject);

            }
            if (other.gameObject.TryGetComponent<HealthSystem>(out HealthSystem healthSystem) &&
                other.gameObject.TryGetComponent<ElementalSystem>(out ElementalSystem elementalSystem) &&
                other.tag != "Player" && isMissile)
            {
                elementalSystem.ElementType(attackType);
                healthSystem.TakeDMG(dmg);
            }
            if (otherRigidbody != null)
            {
                // Adjust the force direction as needed
                float forceMagnitude = 2f; // Adjust the force magnitude as needed
                otherRigidbody.AddForce(spawn.right * forceMagnitude, ForceMode2D.Impulse);
            }
        }
    }


    IEnumerator DestroyAfterDelay(float deathTimer)
    {
        yield return new WaitForSeconds(deathTimer);
        Destroy(this.gameObject);
    }
}


