using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabObjects : MonoBehaviour
{

    [SerializeField] private Transform grabPoint;
    [SerializeField] private Transform rayPoint;
    [SerializeField] private float rayDistance;

    public WeaponScript ws;

    private GameObject grabbedObejct;
    private int layerIndex;
    // Start is called before the first frame update
    void Start()
    {
        layerIndex = LayerMask.NameToLayer("CanGrab");
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(rayPoint.position, transform.right, rayDistance);
        Debug.Log(grabbedObejct);
        if (hitInfo.collider != null && hitInfo.collider.gameObject.layer == layerIndex)
        {
            
            if (Input.GetKeyDown(KeyCode.E) && grabbedObejct == null)
            {
                grabbedObejct = hitInfo.collider.gameObject;
                grabbedObejct.GetComponent<Rigidbody2D>().isKinematic = true;
                grabbedObejct.transform.position = grabPoint.position;
                grabbedObejct.transform.SetParent(transform);
                ws.canFire = false;
                ws.isGrab = true;
            }
            else if (Input.GetKeyDown(KeyCode.E) && grabbedObejct != null)
            {
                grabbedObejct.GetComponent<Rigidbody2D>().isKinematic = false;
                grabbedObejct.transform.SetParent(null);
                grabbedObejct = null;
                ws.canFire = true;
                ws.isGrab = false;
            }
            if (grabbedObejct != null)
            {
                ws.canFire = false;
                ws.isGrab = true;
            }

            
            if (Input.GetButton("Fire1") && grabbedObejct != null)
            {
                grabbedObejct.GetComponent<Rigidbody2D>().isKinematic = false;
                grabbedObejct.transform.SetParent(null);
                grabbedObejct = null;
                ws.canFire = true;
                ws.isGrab = false;
            }
            
        }
        
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(rayPoint.position, rayDistance);
    }
}
