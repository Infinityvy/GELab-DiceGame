using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DieObject : MonoBehaviour
{
    public TextMeshPro[] numbers; //front, back, top, bottom, right, left
    public AudioClip[] audioClips;
    public Material[] disintMaterials;

    private Material[] copiedDisintMaterials;

    private bool idleRotationEnabled = false;
    private float rotationSpeed = 40;
    private Vector3 rotVector;

    private AudioSource audioSource;
    private Rigidbody dieRigid;

    private bool disintegrating = false;
    private float disintegration = 0.0f;
    private float disintagrationSpeed = 0.7f;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        dieRigid = GetComponent<Rigidbody>();

        copiedDisintMaterials = new Material[disintMaterials.Length];
        for(int i = 0; i < disintMaterials.Length; i++)
        {
            copiedDisintMaterials[i] = new Material(disintMaterials[i].shader);
            copiedDisintMaterials[i].CopyPropertiesFromMaterial(disintMaterials[i]);
        }

    }


    void Update()
    {
        if (idleRotationEnabled)
        {
            transform.Rotate(rotVector * Time.deltaTime);
        }

        if(disintegrating)
        {
            for (int i = 0; i < copiedDisintMaterials.Length; i++)
            {
                disintegration += Time.deltaTime * disintagrationSpeed;
                copiedDisintMaterials[i].SetFloat("_Disintegration", disintegration);

                if(disintegration > 2)
                {
                    disintegrating = false;
                    Destroy(gameObject);
                }
            }
        }
    }

    public void setIdleRotation(bool state)
    {
        idleRotationEnabled = state;
        transform.rotation = Quaternion.identity;

        if (state)
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

    public void destroy()
    {
        GetComponent<MeshRenderer>().materials = copiedDisintMaterials;
        disintegrating = true;

        for (int i = 0; i < numbers.Length; i++)
        {
            numbers[i].enabled = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        float volume = audioSource.volume;
        volume *= dieRigid.velocity.magnitude * 0.03f;
        volume = Mathf.Clamp(volume, 0, 1.2f);
        audioSource.PlayOneShot(audioClips[Random.Range(0, audioClips.Length)], volume);
    }
}
