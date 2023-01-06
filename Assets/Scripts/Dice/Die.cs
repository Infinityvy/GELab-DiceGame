using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Die
{
    //public
    /// <summary>
    /// The ID of the die type. (readonly)
    /// </summary>
    public abstract int id { get; }
    /// <summary>
    /// The player ID of the player this die belongs to.
    /// </summary>
    public int playerID = 0;
    /// <summary>
    /// The name of the mesh the die wants to use. (readonly)
    /// </summary>
    public abstract string meshName { get; }
    /// <summary>
    /// The display name of the die. (readonly)
    /// </summary>
    public abstract string dieName { get; }
    /// <summary>
    /// The description of the die. (readonly)
    /// </summary>
    public abstract string description { get; }
    /// <summary>
    /// The active score of the die. Depends on active face value and the die grid it is currently in.
    /// </summary>
    public int activeScore { get; protected set; } = -1;
    /// <summary>
    /// The face the die rolled.
    /// </summary>
    public int activeFaceValue { get; protected set; } = -1;

    public Transform transform;
    /// <summary>
    /// Wether or not this die is connected to a transform. (readonly)
    /// </summary>
    public bool transformInitiated { get; protected set; } = false;

    //protected
    protected abstract int facecount { get; }
    /// <summary>
    /// The rotation needed so that the active face is on top.
    /// </summary>
    protected abstract Quaternion activeFaceRot { get; set; }
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

            Material[] dieMats = dieObject.GetComponent<MeshRenderer>().materials;
            for(int i = 0; i < dieMats.Length; i++)
            {
                if(dieMats[i].name.Contains("DieGlow"))
                {
                    dieMats[i] = (Material)Resources.Load("Player" + playerID + "Glow");
                }
                else if(dieMats[i].name.Contains("DieInnerFaceMat"))
                {
                    dieMats[i] = (Material)Resources.Load("Player" + playerID + "Mat");
                }
            }
            dieObject.GetComponent<MeshRenderer>().materials = dieMats;

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
                otherPlayer.discardDie(otherPlayer.dieFields[mirroredColumn, currentRow]);
                otherPlayer.dieFields[mirroredColumn, currentRow] = null;

                ((GS_PlaceDice)GameManager.current.activeGameState).destroyedDice = true;
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
