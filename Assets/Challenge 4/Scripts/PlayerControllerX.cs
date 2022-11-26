using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Mathematics;

public class PlayerControllerX : MonoBehaviour
{
    private Rigidbody playerRb;
    public float speed = 500;
    public int speedBoostDuration = 2;
    public int speedBoostNewSpeed = 3000;
    public int speedBoostDecelerationAmount = 10;
    private bool isBoosted;
    private GameObject focalPoint;

    public bool hasPowerup;
    public GameObject powerupIndicator;
    public int powerUpDuration = 5;


    private float normalStrength = 10; // how hard to hit enemy without powerup
    private float powerupStrength = 25; // how hard to hit enemy with powerup

    private Keyboard keyboard;
    private Mouse mouse;

    private bool moving;
    private float verticalInput = 0f;

    public ParticleSystem smokeParticle;


    void Start()
    {
        keyboard = Keyboard.current;
        mouse = Mouse.current;
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
    }

    private void FixedUpdate()
    {
        if (moving == true)
        {
            playerRb.AddForce(focalPoint.transform.forward * verticalInput * speed * Time.deltaTime);
        }
    }

    void Update()
    {
        if (keyboard.wKey.isPressed || keyboard.upArrowKey.isPressed)
        {
            moving = true;
            verticalInput = 1f;
        }
        else if (keyboard.sKey.isPressed || keyboard.downArrowKey.isPressed)
        {
            moving = true;
            verticalInput = -1f;
        }
        else
        {
            moving = false;
        }

        smokeParticle.transform.position = new float3(transform.position.x, -0.75f, transform.position.z);
        if (keyboard.spaceKey.isPressed && isBoosted == false)
        {
            smokeParticle.Play();
            isBoosted = true;
            speed = speedBoostNewSpeed;
            StartCoroutine("SpeedBoostCooldown");
        }
        if(isBoosted && speed > 500)
        {
            speed -=    speedBoostDecelerationAmount;

            if (smokeParticle.isPlaying == false)
            {
                smokeParticle.Play();
            }
        }

        // Set powerup indicator position to beneath player
        powerupIndicator.transform.position = new float3(transform.position.x + 0, transform.position.y - 0.6f, transform.position.z + 0);

    }

    // If Player collides with powerup, activate powerup
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Powerup"))
        {
            Destroy(other.gameObject);
            hasPowerup = true;
            powerupIndicator.SetActive(true);
            StartCoroutine("PowerupCooldown");
        }
    }

    // Coroutine to count down powerup duration
    IEnumerator PowerupCooldown()
    {
        yield return new WaitForSeconds(powerUpDuration);
        hasPowerup = false;
        powerupIndicator.SetActive(false);
    }

    IEnumerator SpeedBoostCooldown()
    {
        yield return new WaitForSeconds(speedBoostDuration);
        speed = 500;
        isBoosted = false;
    }
        // If Player collides with enemy
        private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Rigidbody enemyRigidbody = other.gameObject.GetComponent<Rigidbody>();
            float3 awayFromPlayer =  other.gameObject.transform.position - transform.position; 
           
            if (hasPowerup) // if have powerup hit enemy with powerup force
            {
                enemyRigidbody.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
            }
            else // if no powerup, hit enemy with normal strength 
            {
                enemyRigidbody.AddForce(awayFromPlayer * normalStrength, ForceMode.Impulse);
            }


        }
    }



}
