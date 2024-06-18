using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemy : MonoBehaviour
{

    public float speed;
    public Transform target;
    public float minimumDistance;


    public GameObject projectile;
    public float timeBetweenShots;
    private float nextShotTime;

    // Update is called once per frame
    void Update()
    {
        if(Time.time > nextShotTime)
        {
            Instantiate(projectile, transform.position, Quaternion.identity);
            nextShotTime = Time.time+ timeBetweenShots;
        }
        if (Vector2.Distance(target.position, target.position) < minimumDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, -speed* Time.deltaTime);
        }
    }
}
