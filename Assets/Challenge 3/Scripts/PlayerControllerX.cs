using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Mathematics;

public class PlayerControllerX : MonoBehaviour
{
    public bool gameOver;

    public float floatForce;
    private float gravityModifier = 1.1f;

    public Rigidbody playerRb;

    public ParticleSystem explosionParticle;
    public ParticleSystem fireworksParticle;

    private AudioSource playerAudio;
    public AudioClip moneySound;
    public AudioClip explodeSound;
    public AudioClip boingSound;

    public Keyboard keyboard;
    public Mouse mouse;

    public bool floatUp;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        keyboard = Keyboard.current;
        mouse = Mouse.current;
        Physics.gravity *= gravityModifier;
        playerAudio = GetComponent<AudioSource>();

        // Apply a small upward force at the start of the game
        //playerRb.AddForce(Vector3.up * 5, ForceMode.Impulse);

    }

    private void FixedUpdate()
    {
        if(floatUp == true)
        {
            playerRb.AddForce(new float3(0, 1, 0) * floatForce);
            playerRb.velocity = math.clamp(playerRb.velocity, -10, 10);
        }
    }

    // Update is called once per frame
    void Update()   
    {
        // While space is pressed and player is low enough, float up
        if (keyboard.spaceKey.isPressed && !gameOver && transform.position.y < 13)
        {
            floatUp = true;
        }
        else
        {
            floatUp = false;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        // if player collides with bomb, explode and set gameOver to true
        if (other.gameObject.CompareTag("Bomb"))
        {
            explosionParticle.Play();
            playerAudio.PlayOneShot(explodeSound, 1.0f);
            gameOver = true;
            playerRb.isKinematic = true;
            Debug.Log("Game Over!");
            Destroy(other.gameObject);
        } 

        // if player collides with money, fireworks
        else if (other.gameObject.CompareTag("Money"))
        {
            fireworksParticle.Play();
            playerAudio.PlayOneShot(moneySound, 1.0f);
            Destroy(other.gameObject);

        }

        else if (other.gameObject.CompareTag("Boundary"))
        {
            playerAudio.PlayOneShot(boingSound, 2.0f);
        }

    }

    

}
