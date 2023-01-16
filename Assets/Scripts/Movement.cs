using System.Xml;
using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    //PARAMETERS
    [SerializeField] float thrustSpeed = 2;
    [SerializeField] float rotationSpeed = 30;
    [SerializeField] AudioClip engineSound;

    [SerializeField] ParticleSystem mainThrusterParticle;
    [SerializeField] ParticleSystem rightThrusterFront;
    [SerializeField] ParticleSystem rightThrusterBack;
    [SerializeField] ParticleSystem leftThrusterFront;
    [SerializeField] ParticleSystem leftThrusterBack;
    

    //CACHE
    Rigidbody rb;
    AudioSource audioComp;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioComp = GetComponent<AudioSource>();
    }

   
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
                audioComp.PlayOneShot(engineSound);
            }
            if(!mainThrusterParticle.isPlaying)
            {
                mainThrusterParticle.Play();
            }
            
        }
        else
        {   
            audioComp.Stop();
            mainThrusterParticle.Stop();
        }
    }

    void ProcessRotation()
    {
        if(Input.GetKey(KeyCode.A))
        {
            Rotate(1);
            if(!rightThrusterBack.isPlaying){
                rightThrusterBack.Play();
            }
            if(!rightThrusterFront.isPlaying){
                rightThrusterFront.Play();
            }
        }
        else if(Input.GetKey(KeyCode.D))
        {
            Rotate(-1);
            if(!leftThrusterBack.isPlaying){
                leftThrusterBack.Play();
            }
            if(!leftThrusterFront.isPlaying){
                leftThrusterFront.Play();
            }
        }
        else{
            rightThrusterBack.Stop();
            rightThrusterFront.Stop();
            leftThrusterBack.Stop();
            leftThrusterFront.Stop();
        }
    }

    void Rotate(int rotationDirection)
    {
        rb.freezeRotation = true;  //freezing rotation so we can manually rotate
        gameObject.transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime * rotationDirection);
        rb.freezeRotation = false;
    }
}
