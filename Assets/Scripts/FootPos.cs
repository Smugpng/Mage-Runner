using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootPos : MonoBehaviour
{
   
    // reference to player character object
    public GameObject playerObj;

    // reference to IK target
    public Transform target;

    // reference to the other foot
    public FootPos otherFoot;

    public bool isBalanced;

    private void Update()
    {
        UpdateBalance();
    }

    private void UpdateBalance()
    {
        // get center of mass in world position
        float centerOfMass = playerObj.transform.position.x;
        // if center of mass is between two feet, the body is balanced
        isBalanced = IsFloatInRange(centerOfMass, target.position.x, otherFoot.target.position.x);
    }

    /// <summary>
    /// returns true if "value" is between "bound1" and "bound2"
    /// </summary>
    bool IsFloatInRange(float value, float bound1, float bound2)
    {
        float minValue = Mathf.Min(bound1, bound2);
        float maxValue = Mathf.Max(bound1, bound2);
        return value > minValue && value < maxValue;
    }
}
