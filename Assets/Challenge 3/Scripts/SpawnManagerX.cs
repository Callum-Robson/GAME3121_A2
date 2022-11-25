using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

namespace Part1
{
    public class SpawnManagerX : MonoBehaviour
    {
        public GameObject[] objectPrefabs;
        private float spawnDelay = 2;
        private float spawnInterval = 1.5f;

        private PlayerControllerX playerControllerScript;

        // Start is called before the first frame update
        void Start()
        {
            InvokeRepeating("SpawnObjects", spawnDelay, spawnInterval);
            playerControllerScript = GameObject.Find("Player").GetComponent<PlayerControllerX>();
        }

        // Spawn obstacles
        void SpawnObjects()
        {
            Unity.Mathematics.Random randomY = new Unity.Mathematics.Random();
            randomY.InitState((uint)System.DateTime.Now.Ticks);

            // Set random spawn location and random object index
            float3 spawnLocation = new float3(30, randomY.NextUInt(5, 15), 0);
            int index = randomY.NextInt(objectPrefabs.Length);

            // If game is still active, spawn new object
            if (!playerControllerScript.gameOver)
            {
                Instantiate(objectPrefabs[index], spawnLocation, objectPrefabs[index].transform.rotation);
            }

        }
    }

}
