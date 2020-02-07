using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Seperate Dev

public class Seek : MonoBehaviour
{
    public Kinematic character;
    public Kinematic target;

    float maxAcceleration;

    public virtual SteeringOutput getSteering()
    {
        SteeringOutput result = new SteeringOutput();

        // Get the direction to the target
        result.linear = target.transform.position - character.transform.position;

        return result;
    }
}
