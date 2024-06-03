using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SpellScript;

public class HealthSystem : MonoBehaviour
{

    public float health = 3;
    public GameManager gameManager;
    [SerializeField] private Animator _anim;
    //bool iFrames = false;

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        //StartCoroutine(IFrames());
        
    }
    public void TakeDMG(float dmg)
    {
        //iFrames = true;
        health -= dmg;


        Debug.Log(health + "Health Left");
        Debug.Log(dmg + "Damage Taken");
        if (health > 0 && tag == "Player")
        {
            _anim.SetTrigger("takeDmg");

        }
        else if (health <= 0 && tag == "Player")
        {
            _anim.SetBool("isDead", true);
            gameManager.PlayerDeath();
            StartCoroutine(STOPANIM());
        }
        else if (health <= 0f)
        {
            Destroy(gameObject);
        }
        /*if (!iFrames)
        {
            
        }*/
    }
   /* IEnumerator IFrames()
    {
        yield return new WaitForSeconds(.5f);
        iFrames = false;
    }*/
    IEnumerator STOPANIM()
    {
        yield return new WaitForSeconds(.6f);
        
            _anim.speed = 0f;
        
    }
}
