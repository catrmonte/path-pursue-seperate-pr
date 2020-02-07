using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Seperate dev

public class Kinematic : MonoBehaviour
{
    public Vector3 linearVelocity;
    public float angularVelocity;  // Millington calls this rotation
    // because I'm attached to a gameobject, we also have:
    // rotation <<< Millington calls this orientation
    // position
    public string behaviorType;
    public int behaviorNum = 1;
    public Text text;

    public GameObject myTarget;
    public Kinematic alsoMyTargetForPursueTho;

    pathFollow myPathFollow = new pathFollow();
    Seperate mySeperate = new Seperate();

    // Set of gameobjects to create path
    public GameObject[] pathToFollow;

    // Set of targets to seperate away from
    public Kinematic[] targetsForSeperate;

    public enum Behavior
    {
        Seek,
        Flee
    }

    // Update is called once per frame
    void Update()
    {
        if (behaviorNum != 0)
        {
            // update my position and rotation
            this.transform.position += linearVelocity * Time.deltaTime;
            Vector3 v = new Vector3(0, angularVelocity, 0);
            this.transform.eulerAngles += v * Time.deltaTime;
        }

        // update linear and angular velocities
        SteeringOutput steering = new SteeringOutput();

        // Seek: target is "alsoMyTargetForPursueTho"
        if (behaviorNum == 1)
        {
            Seek mySeek = new Seek();
            text.text = "Seek";

            mySeek.target = alsoMyTargetForPursueTho;
            mySeek.character = this;

            steering = mySeek.getSteering();
            linearVelocity += steering.linear * Time.deltaTime;
            angularVelocity += steering.angular * Time.deltaTime;
        }
        // Flee
        else if (behaviorNum == 2)
        {
            Flee myFlee = new Flee();
            text.text = "Flee";

            myFlee.character = this;
            myFlee.target = myTarget;
            steering = myFlee.getSteering();
            linearVelocity += steering.linear * Time.deltaTime;
            angularVelocity += steering.angular * Time.deltaTime;
        }
        // Arrive
        else if (behaviorNum == 3)
        {
            Arrive myArrive = new Arrive();
            text.text = "Arrive";

            myArrive.character = this;
            myArrive.target = myTarget;
            steering = myArrive.getSteering();
            if (steering != null)
            {
                linearVelocity += steering.linear * Time.deltaTime;
                angularVelocity += steering.angular * Time.deltaTime;
            }
            else
            {
                linearVelocity = Vector3.zero;
            }
        }
        // Align
        else if (behaviorNum == 4)
        {
            Align myAlign = new Align();
            text.text = "Align";

            myAlign.character = this;
            myAlign.target = myTarget;
            steering = myAlign.getSteering();
            if (steering != null)
            {
                linearVelocity += steering.linear * Time.deltaTime;
                angularVelocity += steering.angular * Time.deltaTime;
            }
        }
        // Face
        else if (behaviorNum == 5)
        {
            Face myFace = new Face();
            text.text = "Face";

            myFace.character = this;
            myFace.target = myTarget;
            steering = myFace.getSteering();
            if (steering != null)
            {
                linearVelocity += steering.linear * Time.deltaTime;
                angularVelocity += steering.angular * Time.deltaTime;
            }
        }
        // Look where you're going
        else if (behaviorNum == 6)
        {
            LWYG myLook = new LWYG();
            text.text = "LWYG";

            myLook.character = this;
            steering = myLook.getSteering();
            if (steering != null)
            {
                linearVelocity += steering.linear * Time.deltaTime;
                angularVelocity += steering.angular * Time.deltaTime;
            }
        }
        // Path following
        else if (behaviorNum == 7)
        {
            text.text = "Path Follow";

            myPathFollow.character = this;
            myPathFollow.path = pathToFollow;
            steering = myPathFollow.getSteering();
            if (steering != null)
            {
                linearVelocity += steering.linear * Time.deltaTime;
                angularVelocity += steering.angular * Time.deltaTime;
            }
        }
        // Pursue
        else if (behaviorNum == 8)
        {
            Pursue myPursue = new Pursue();
            text.text = "Pursue";

            myPursue.character = this;
            myPursue.target = alsoMyTargetForPursueTho;
            steering = myPursue.getSteering();
            if (steering != null)
            {
                linearVelocity += steering.linear * Time.deltaTime;
                angularVelocity += steering.angular * Time.deltaTime;
            }
        }
        // Seperate
        else if (behaviorNum == 9)
        {
            text.text = "Seperate";

            mySeperate.character = this;
            mySeperate.targets = targetsForSeperate;
            steering = mySeperate.getSteering();
            if (steering != null)
            {
                linearVelocity += steering.linear * Time.deltaTime;
                angularVelocity += steering.angular * Time.deltaTime;
            }
        }
        
    }

    // Cycles through behaviorNums to change what the behaviorCube is doing
    public void ChangeBehavior()
    {
        behaviorNum++;
        
        if (behaviorNum > 9)
        {
            behaviorNum = 1;
        }
    }
}
