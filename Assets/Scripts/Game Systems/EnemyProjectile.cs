using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SpellScript;

public class EnemyProjectile : MonoBehaviour
{
    public GameObject HitPrefab;
    public bool isMissile;
    public bool isHitmark;
    public float deathTimer;
    public bool isMelee;
    private Transform spawn;
    bool isActive = false;

    
    [Header("Damage")]
   
    [SerializeField] private float dmg;

    Vector3 targetPos; public float speed;
    GameObject player; Transform target;

   
    

    // Start is called before the first frame update
    void Start()
    {
        spawn = GetComponent<Transform>();
        
        if (isMissile)
        {
            StartCoroutine(DestroyAfterDelay(deathTimer));
        }
        if (isHitmark)
        {
            StartCoroutine(DestroyAfterDelay(deathTimer));
        }

        targetPos = FindAnyObjectByType<PlayerMovement>().transform.position;
        player = FindAnyObjectByType<PlayerMovement>().gameObject;
        target = player.transform;
    }

    // Update is called once per frame
    void Update()
    {
       // transform.LookAt((new Vector3(0, 0, target.position.z)));

        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed*Time.deltaTime);
        
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
                Destroy(gameObject);

            }
            if (other.gameObject.TryGetComponent<HealthSystem>(out HealthSystem healthSystem)  &&
                other.tag == "Player" && isMissile)
            {
                
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
