
using UnityEngine;
using System.Collections;

public class PassengerSpawner : MonoBehaviour
{
    public Transform spawnPoint;                 // Start point
    //public Transform turnPoint;                  // L-shape ka turn
    public PassengerQueueManager queueManager;   // Queue manager
    public float spawnInterval = 1.0f;           // Har kitne sec me next nikle
    private int currentCount = 0;
    private bool isSpawning = false;
    public GameObject[] passengerTypePrefabs;    // Different human prefabs
    public int maxVisible = 20;                  // Hamesha itne dikhne chahiye

    void Start()
    {
        StartCoroutine(SpawnLoop());
    }

    IEnumerator SpawnLoop()
    {
        while (true)
        {
            // Agar line me kam hain to naya spawn karo
            if (currentCount < maxVisible && !isSpawning)
            {
                isSpawning = true;
                SpawnPassenger();
                yield return new WaitForSeconds(spawnInterval);
                isSpawning = false;
            }

            yield return null;
        }
    }

    public void SpawnPassenger()
    {
        if (passengerTypePrefabs.Length == 0) return;

        int index = Random.Range(0, passengerTypePrefabs.Length);

        GameObject p = Instantiate(
            passengerTypePrefabs[index],
            spawnPoint.position,
            Quaternion.identity
        );

        Passenger passenger = p.GetComponent<Passenger>();
        if (passenger == null)
            passenger = p.AddComponent<Passenger>();

        passenger.typeIndex = index;

        
        queueManager.AddPassenger(passenger);

        currentCount++; // 
    }

    // Jab koi passenger car me baith jaaye, call karo
    public void OnPassengerBoarded()
    {
        currentCount--; // ✅ pehle count ghatao
        // SpawnLoop automatically next spawn kar dega jab count < maxVisible hoga
    }
}