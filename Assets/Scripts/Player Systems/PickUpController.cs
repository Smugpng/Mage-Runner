using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpController : MonoBehaviour
{
    [Header("PickUp Setting")]
    [SerializeField] private Transform holdArea;
    private GameObject heldObj;
    private Rigidbody2D heldObjRB;

    [Header("Physics Parameters")]
    [SerializeField] private float pickUpRange = 5f;
    [SerializeField] private float pickupForce = 150f;


    private int layerIndex;
    private void Start()
    {
        layerIndex = LayerMask.NameToLayer("CanGrab");
    }
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.E))
        {
            if (heldObj == null)
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, pickUpRange);
                if (hit.collider != null && hit.collider.gameObject.layer == layerIndex) 
                {
                    PickUpObject(hit.transform.gameObject);
                }
            }
            else
            {
                DropObject();
            }
        }
        if (heldObj != null)
        {
            MoveOBJ();
        }
    }

    void MoveOBJ()
    {
        if(Vector2.Distance(heldObj.transform.position, holdArea.position) > 0.1f)
        {
            Vector2 moveDirection = (holdArea.position - heldObj.transform.position);
            heldObjRB.AddForce(moveDirection * pickupForce);
        }
    }

    void PickUpObject(GameObject pickObj)
    {
        if (pickObj.GetComponent<Rigidbody2D>())
        {
            heldObjRB = pickObj.GetComponent<Rigidbody2D>();
            heldObjRB.useAutoMass = false;
            heldObjRB.drag = 10;
            heldObjRB.constraints = RigidbodyConstraints2D.FreezeRotation;

            heldObjRB.transform.parent = holdArea;
            heldObj = pickObj;
        }
    }
    void DropObject()
    {
       
        
            
            heldObjRB.useAutoMass = true;
            heldObjRB.drag = 1;
            heldObjRB.constraints = RigidbodyConstraints2D.None;

            heldObjRB.transform.parent = null;
            heldObj = null;
        
    }
}

