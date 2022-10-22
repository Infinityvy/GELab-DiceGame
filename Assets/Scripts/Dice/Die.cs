using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Die : MonoBehaviour
{
    //static
    protected static float minVelocityThreshold = 0.1f; //if a die's velocity is lower than this value it will count as not moving; currently unused
    protected static float minAngularVelocityThreshold = 0.1f; //if a die's angualr velocity is lower than this value it will count as not rotating; currently unused

    //public
    public virtual int id { get; } = -1;

    public virtual string dieName { get; } = "NULL";
    public virtual string description { get; } = "NULL";

    public int activeScore { get; protected set; } = -1;
    public int activeFaceValue { get; protected set; } = -1;


    //protected
    protected virtual int facecount { get; } = -1;

    protected bool rollModeEnabled 
    { 
        get { return _rollModeEnabled; }
        set
        {
            _rollModeEnabled = value;
            dieRigidbody.useGravity = value;
            dieRigidbody.detectCollisions = value;
            if(!value) dieRigidbody.velocity = Vector3.zero;
        }
    }
    protected bool _rollModeEnabled = false;

    protected Rigidbody dieRigidbody;


    //methods
    public void Awake()
    {
        init();
    }


    public virtual void init() //initializes the die
    {
        dieRigidbody = GetComponent<Rigidbody>();
        rollModeEnabled = false;
    }

    public virtual void roll(Vector3 force, Vector3 torque) //applies force and torque to the die's rigidbody
    {
        Debug.Log(dieRigidbody);

        rollModeEnabled = true;
        dieRigidbody.AddForce(force, ForceMode.Impulse);
        dieRigidbody.AddTorque(torque);
    }

    public bool hasStoppedMoving() //returns true if the die has stopped moving and rotating
    {
        //bool stoppedMoving = dieRigidbody.velocity.magnitude < minVelocityThreshold && 
        //       dieRigidbody.angularVelocity.magnitude < minAngularVelocityThreshold;

       bool stoppedMoving = dieRigidbody.IsSleeping(); //might not work; if it doesnt use code above instead

        rollModeEnabled = !stoppedMoving;

        if (stoppedMoving) setActiveFaceValue();

        return stoppedMoving;
    }

    protected virtual void setActiveFaceValue()
    {
        //override this probably
    }

    public virtual void calculateActiveScore(Die[,] dieFields, int x, int y) //default calulation. adds face value on active score for each duplicate face value in the row; 
    {                                                                        //override to customize score calculation
        activeScore = activeFaceValue;

        for(int y1 = 0; y1 < 3; y1++)
        {
            if (y1 == y) continue;
            else if (dieFields[x, y1].activeFaceValue == activeFaceValue) activeScore += activeFaceValue;
        }
    }
}
