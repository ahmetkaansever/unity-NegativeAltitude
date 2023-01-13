using System.Xml;
using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float thrustSpeed = 2;
    [SerializeField] float rotationSpeed = 30;
    Rigidbody rb;
    AudioSource audioComp;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioComp = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust(){
        if(Input.GetKey(KeyCode.Space))
        {
            rb.AddRelativeForce(Vector3.up * thrustSpeed * Time.deltaTime);

            //Processing Audio
            if(!audioComp.isPlaying)
            {
                audioComp.Play();
            }
            
        }
        else
        {   
            audioComp.Stop();
        }
    }

    void ProcessRotation()
    {
        if(Input.GetKey(KeyCode.A))
        {
            Rotate(1);
        }
        else if(Input.GetKey(KeyCode.D))
        {
            Rotate(-1);
        }
    }

    void Rotate(int rotationDirection)
    {
        rb.freezeRotation = true;  //freezing rotation so we can manually rotate
        gameObject.transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime * rotationDirection);
        rb.freezeRotation = false;
    }
}
