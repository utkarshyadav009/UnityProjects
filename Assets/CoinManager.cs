using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public GameObject CoinPrefab;
    public GameObject ground; // Drag your ground GameObject here in the Inspector.
    public int numberOfCoins = 10; // Number of coins you want to spawn.

    void Start()
    {
        SpawnCoins();
    }

    void SpawnCoins()
    {
        if (ground != null && CoinPrefab != null)
        {
            Collider groundCollider = ground.GetComponent<Collider>();

            if (groundCollider != null)
            {
                for (int i = 0; i < numberOfCoins; i++)
                {
                    Vector3 randomPosition = GetRandomPositionOnGround(groundCollider.bounds);
                    GameObject coinInstance = Instantiate(CoinPrefab, randomPosition, Quaternion.identity);
                    //coinInstance.tag = "Coin";  // This line sets the tag
                }
            }
            else
            {
                Debug.LogError("No collider found on the ground object.");
            }
        }
    }


    Vector3 GetRandomPositionOnGround(Bounds groundBounds)
    {
        float randomX = Random.Range(groundBounds.min.x, groundBounds.max.x);
        float randomZ = Random.Range(groundBounds.min.z, groundBounds.max.z);

        // Using groundBounds.min.y to position the coin just above the ground. You can adjust this as needed.
        return new Vector3(randomX, groundBounds.min.y + 0.5f, randomZ);
    }
}
