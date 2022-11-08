using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Die
{
    //public
    public abstract int id { get; } //id of the die
    public abstract string meshName { get; } //name of the mesh the die wants to use
    public abstract string dieName { get; } //display name of the die
    public abstract string description { get; } //description of the die

    public int activeScore { get; protected set; } = -1;
    public int activeFaceValue { get; protected set; } = -1;

    public Transform transform;
    public bool transformInitiated { get; protected set; } = false;

    //protected
    protected abstract int facecount { get; }

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

    protected bool idleRotationEnabled = false;
    protected float rotationSpeed = 40;
    protected Vector3 rotVector;

    protected Rigidbody dieRigidbody;


    //methods

    private void Update()
    {
        if(idleRotationEnabled)
        {
            transform.Rotate(rotVector * Time.deltaTime);
        }
    }

    public virtual void init_Transform(Transform dieTransform) //initializes the die transform
    {
        if (dieTransform == null) throw new System.NullReferenceException();
        else
        {
            transformInitiated = true;
            transform = dieTransform;
            dieRigidbody = transform.GetComponent<Rigidbody>();
            rollModeEnabled = false;
        }

    }

    public virtual void init_Transform() //initializes the die transform generated from the die's mesh name
    {
        init_Transform((Transform) Resources.Load(meshName + "_Pref"));
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

       bool stoppedMoving = dieRigidbody.IsSleeping(); //might not work; if it doesnt use code above instead

        rollModeEnabled = !stoppedMoving;

        if (stoppedMoving) setActiveFaceValue();

        return stoppedMoving;
    }

    protected abstract void setActiveFaceValue();
    public virtual void calculateActiveScore(Die[,] dieFields, int x, int y) //default calulation. adds face value on active score for each duplicate face value in the row; 
    {                                                                        //override to customize score calculation
        activeScore = activeFaceValue;

        for(int y1 = 0; y1 < 3; y1++)
        {
            if (y1 == y) continue;
            else if (dieFields[x, y1].activeFaceValue == activeFaceValue) activeScore += activeFaceValue;
        }
    }

    public void setIdleRotation(bool state)
    {
        idleRotationEnabled = state;
        transform.rotation = Quaternion.identity;

        if(state)
        {
            int xFac = 0;
            int zFac = 0;
            while (xFac == 0 && zFac == 0)
            {
                xFac = Random.Range(-1, 2);
                zFac = Random.Range(-1, 2);
            }

            rotVector = new Vector3(rotationSpeed * xFac, rotationSpeed, rotationSpeed * zFac);
        }
    }
}
