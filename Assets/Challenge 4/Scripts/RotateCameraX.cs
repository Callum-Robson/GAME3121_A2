using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Mathematics;

public class RotateCameraX : MonoBehaviour
{
    private float speed = 200;
    public GameObject player;
    private Keyboard keyboard;

    private void Start()
    {
        keyboard = Keyboard.current;
    }
    // Update is called once per frame
    void Update()
    {
        float horizontalInput = 0;
        if(keyboard.dKey.isPressed || keyboard.rightArrowKey.isPressed)
        {
            horizontalInput = 1;
        }
        else if (keyboard.aKey.isPressed || keyboard.leftArrowKey.isPressed)
        {
            horizontalInput = -1;
        }
        transform.Rotate(new float3 (0, 1, 0), horizontalInput * speed * Time.deltaTime);

        transform.position = player.transform.position; // Move focal point with player

    }
}
