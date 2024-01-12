using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    [SerializeField] float thrustPower = 100f;
    [SerializeField] float rotatingSpeed = 10f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem mainBooster;
    [SerializeField] ParticleSystem leftBoost;
    [SerializeField] ParticleSystem rightBoost;

    Rigidbody rb;
    AudioSource audioSource;


    // Start is called before the first frame update
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
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }
     void ProcessRotation(){
        if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) 
        {
            RotatingLeft();
        }
        else if(Input.GetKey(KeyCode.RightArrow)|| Input.GetKey(KeyCode.D)) 
        {
            RotatingRight();
        
        else 
        {
            StopRotating();
        }
    }
    void StartThrusting()
    {
        Debug.Log("Pressed SPACE - Thrusting");
        rb.AddRelativeForce(thrustPower * Vector3.up * Time.deltaTime);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }
        if (!mainBooster.isPlaying)
        {
            mainBooster.Play();
        }
    }
    void StopThrusting()
    {
        mainBooster.Stop();
        audioSource.Stop(); 
    }

   
    void RotatingLeft(){
        rightBoost.Stop();
        Debug.Log("Rotating Left");
        if (!leftBoost.isPlaying)
        {
            leftBoost.Play();
        }
        ApplyRotation(rotatingSpeed);
    }
    void RotatingRight(){
        leftBoost.Stop();
        Debug.Log("Rotating Right");
        if (!rightBoost.isPlaying)
        {
            rightBoost.Play();
        }
        ApplyRotation(-rotatingSpeed);
    }
    void StopRotating(){
        leftBoost.Stop();
        rightBoost.Stop();
    }

    void ApplyRotation(float rotationThisFrame) 
    {
        rb.freezeRotation = true; // freezing rotation so we can manually rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false; // unfreezing rotation so the physics system can take over
    }
}
