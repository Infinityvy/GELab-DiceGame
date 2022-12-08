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
    protected abstract Quaternion activeFaceRot { get; set; } //the rotation needed so that the active face is on top

    protected bool rollModeEnabled 
    { 
        get { return _rollModeEnabled; }
        set
        {
            _rollModeEnabled = value;
            dieRigidbody.useGravity = value;
            dieRigidbody.detectCollisions = value;
            dieRigidbody.freezeRotation = !value;
            if(!value) dieRigidbody.velocity = Vector3.zero;
        }
    }
    protected bool _rollModeEnabled = false;

    protected DieObject dieObject;
    protected Rigidbody dieRigidbody;


    //methods

    public virtual void init_Transform(Transform dieTransform) //initializes the die transform
    {
        if (dieTransform == null) throw new System.NullReferenceException();
        else
        {
            transformInitiated = true;
            transform = dieTransform;
            dieObject = transform.GetComponent<DieObject>();
            dieRigidbody = transform.GetComponent<Rigidbody>();
            rollModeEnabled = false;

            setFaceNumbers();
        }
    }

    public virtual void init_Transform() //initializes the die transform generated from the die's mesh name
    {
        init_Transform(MonoBehaviour.Instantiate(((GameObject)Resources.Load(meshName + "_Pref")).transform, Vector3.zero, Quaternion.identity));
    }

    public virtual void clear_Transform() //destroys the connected transform and severs the connection
    {
        dieObject.destroy();
        transform = null;
        transformInitiated = false;
    }

    protected abstract void setFaceNumbers();

    public virtual void roll(Vector3 force, Vector3 torque) //applies force and torque to the die's rigidbody
    {
        //Debug.Log(dieRigidbody);

        rollModeEnabled = true;
        dieRigidbody.AddForce(force, ForceMode.Impulse);
        dieRigidbody.AddTorque(torque);
    }

    public bool hasStoppedMoving() //returns true if the die has stopped moving and rotating
    {

        bool stoppedMoving = dieRigidbody.IsSleeping();

        if (stoppedMoving)
        {
            rollModeEnabled = false;
            setActiveFaceValue();
        }

        return stoppedMoving;
    }

    public void forceStopMoving()
    {
        rollModeEnabled = false;
        setActiveFaceValue();
    }

    protected abstract void setActiveFaceValue();
    /// <summary>
    /// Compares the die with the other Players board to potentially destroy conflicting dice.
    /// </summary>
    /// <param name="activePlayer">The player this die belongs to.</param>
    /// <param name="otherPlayer">The other player.</param>
    /// <param name="column">The column this die is located in.</param>
    /// <param name="row">The row this die is located in.</param>
    public virtual void attackBoard(Player activePlayer, Player otherPlayer, int column, int row) //default calculation. destroys same face value dice on the mirrored column of the enemy board
    {
        int mirroredColumn = column * -1 + 2;

        for (int currentRow = 0; currentRow < 3; currentRow++)
        {
            if(otherPlayer.dieFields[mirroredColumn, currentRow] != null && otherPlayer.dieFields[mirroredColumn, currentRow].activeFaceValue == activeFaceValue)
            {
                GameManager.current.players[(activeFaceValue + 1) % 2].discardDie(otherPlayer.dieFields[mirroredColumn, currentRow]);
                otherPlayer.dieFields[mirroredColumn, currentRow] = null;
            }
        }
    }

    public virtual void calculateActiveScore(Die[,] dieFields, int column, int row) //default calulation. adds face value on active score for each duplicate face value in the column
    {                                                                        
        activeScore = activeFaceValue;

        for(int currentRow = 0; currentRow < 3; currentRow++)
        {
            if (currentRow == row) continue;
            else if (dieFields[column, currentRow] != null && dieFields[column, currentRow].activeFaceValue == activeFaceValue) activeScore += activeFaceValue;
        }
    }


    public void setIdleRotation(bool state)
    {
        dieObject.setIdleRotation(state);
    }

    public virtual void rotateToActiveFace(int rotationIndex)
    {
        transform.rotation = Quaternion.Euler(0, 180 * rotationIndex, 0) * activeFaceRot;
    }
}
