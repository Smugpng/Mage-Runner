using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegMover : MonoBehaviour
{
    public Transform limbSolverTarget;
    public float moveDistance;
    public LayerMask groundLayer;
    public LayerMask slopeLayer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckGround();
        if(Vector2.Distance(limbSolverTarget.position,transform.position) > moveDistance)
        {
            limbSolverTarget.position = transform.position;
        }
    }

    public void CheckGround()
    {
        RaycastHit2D hit = Physics2D.Raycast(gameObject.transform.position, Vector3.down, 5, groundLayer);
        if(hit.collider != null )
        {
            Vector3 point = hit.point;
            point.y += 0.1f;
            transform.position = point;
        }
        RaycastHit2D hit2 = Physics2D.Raycast(gameObject.transform.position, Vector3.down, 5, slopeLayer);
        if (hit2.collider != null)
        {
            Vector3 point = hit2.point;
            point.y += 0.1f;
            transform.position = point;
        }
        
    }
}
