using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private float defDistanceRay = 500f;
    public Transform laserFirepoint;
    public LineRenderer lineRenderer;
    Transform _transform;

    // Start is called before the first frame update
    void Awake()
    {
        _transform = GetComponent<Transform>();
        lineRenderer = GetComponent<LineRenderer>();
        laserFirepoint = GetComponent<Transform>();
    }

    private void Update()
    {
        ShootLaser();
    }

    void ShootLaser()
    {
        RaycastHit2D _hit = Physics2D.Raycast(_transform.position, _transform.right, defDistanceRay);

        if (_hit.collider != null)
        {
            Draw2DRay(laserFirepoint.position, _hit.point);
        }
        else
        {
            Draw2DRay(laserFirepoint.position, (Vector2)laserFirepoint.position + (Vector2)laserFirepoint.right * defDistanceRay);
        }
    }

    void Draw2DRay(Vector2 startPos, Vector2 endPos)
    {
        lineRenderer.SetPosition(0, startPos);
        lineRenderer.SetPosition(1, endPos);
    }
}
