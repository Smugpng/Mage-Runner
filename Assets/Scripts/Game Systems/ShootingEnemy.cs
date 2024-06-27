using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemy : MonoBehaviour
{

    public float speed;
    public Transform target;
    public float minimumDistance;
    bool canAttack;
    [SerializeField] private Animator _animator;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private Transform enemy;


    public GameObject projectile;
    public float timeBetweenShots;
    private float nextShotTime;
    private void Start()
    {
        canAttack = true;
        _rb = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        if(Time.time > nextShotTime && canAttack)
        {
            canAttack = false;
            StartCoroutine(Attack());
        }
        if (Vector2.Distance(target.position, target.position) < minimumDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, -speed * Time.deltaTime);
        }
        if (_rb.velocity.magnitude > 0)
        {
            _animator.SetBool("isWalking", true);
        }
        if (_rb.velocity.x >= 0.01f)
        {
            enemy.localScale = new Vector3(-20f, 20f, 1f);

        }
        else if (_rb.velocity.x <= -0.01f)
        {
            enemy.localScale = new Vector3(20f, 20f, 1f);
        }
    }

    IEnumerator Attack()
    {
        _animator.SetTrigger("Attack");
        yield return new WaitForSeconds(.7f);

        Instantiate(projectile, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(2f);
        canAttack = true;
        nextShotTime = Time.time + timeBetweenShots; ;
    }
    public void TakeDamge()
    {
        _animator.SetTrigger("DMGTaken");
    }
}
