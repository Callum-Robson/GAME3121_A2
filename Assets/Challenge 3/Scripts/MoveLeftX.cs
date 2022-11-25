﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

namespace Part1
{
    public class MoveLeftX : MonoBehaviour
    {
        public float speed;
        private PlayerControllerX playerControllerScript;
        private float leftBound = -10;

        // Start is called before the first frame update
        void Start()
        {
            playerControllerScript = GameObject.Find("Player").GetComponent<PlayerControllerX>();
        }

        // Update is called once per frame
        void Update()
        {
            // If game is not over, move to the left
            if (playerControllerScript.gameOver == false)
            {
                transform.Translate(new float3(-1, 0, 0) * speed * Time.deltaTime, Space.World);
            }

            // If object goes off screen that is NOT the background, destroy it
            if (transform.position.x < leftBound && !gameObject.CompareTag("Background"))
            {
                Destroy(gameObject);
            }

        }
    }
}
