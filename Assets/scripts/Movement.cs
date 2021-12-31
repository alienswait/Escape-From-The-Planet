using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    Rigidbody rb;
    [SerializeField] float rotationThrust = 1f;
    [SerializeField] float mainThrust = 100f;
    AudioSource audioSource;
    [SerializeField] AudioClip mainEngine;

    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem leftThrustlerParticles;
    [SerializeField] ParticleSystem rightThustlerParticles;

    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
       if (Input.GetKey(KeyCode.Space))
       {
           rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
           if(!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(mainEngine);
            }
            
            if(!mainEngineParticles.isPlaying)
            {    
                mainEngineParticles.Play();
            }
       }
       else
        {
            audioSource.Stop();
            mainEngineParticles.Stop();
        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(rotationThrust);
            if(!rightThustlerParticles.isPlaying)
            {    
                rightThustlerParticles.Play();
            }
        }
        else if (Input.GetKey(KeyCode.D))
       {
            ApplyRotation(-rotationThrust);
            if(!leftThrustlerParticles.isPlaying)
            {    
                leftThrustlerParticles.Play();
            }
       }

       else
       {
           rightThustlerParticles.Stop();
           leftThrustlerParticles.Stop();
       }
    }

    void ApplyRotation( float rotationThisFrame)
    {
        rb.freezeRotation = true; //freezing rotation so we can manually rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false; //unfreezing rotation so the physics system can take over
    }
}
