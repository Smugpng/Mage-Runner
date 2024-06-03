using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{

    public Rigidbody2D hook;
    public GameObject[] prefabRopSegs;
    public GameObject swingLight;
    public int numLinks = 5;
    
    void Start()
    {
        GenerateRope(); 
    }

    
    void GenerateRope()
    {
        Rigidbody2D prevBod = hook;
        for(int i = 0; i < numLinks; i++)
            if (i == numLinks - 1)
            {
                GameObject newSeg = Instantiate(swingLight);
                newSeg.transform.parent = transform;
                newSeg.transform.position = transform.position;
                HingeJoint2D hj = newSeg.GetComponent<HingeJoint2D>();
                hj.connectedBody = prevBod;

                prevBod = newSeg.GetComponent<Rigidbody2D>();
            }
            else 
            {
                int index = Random.Range(0,prefabRopSegs.Length);
                GameObject newSeg = Instantiate(prefabRopSegs[index]);
                newSeg.transform.parent = transform;
                newSeg.transform.position = transform.position;
                HingeJoint2D hj = newSeg.GetComponent<HingeJoint2D>();
                hj.connectedBody = prevBod;

                prevBod = newSeg.GetComponent<Rigidbody2D>();
            
            }
        
    }
}
