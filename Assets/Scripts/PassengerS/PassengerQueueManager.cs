
using System.Collections.Generic;
using UnityEngine;

public class PassengerQueueManager : MonoBehaviour
{
    public Transform[] slots;          // S0, S1, S2...
    private List<Passenger> queue = new List<Passenger>();

    public void AddPassenger(Passenger p)
    {
        queue.Add(p);
        UpdatePositions();
    }

    public Passenger GetFrontPassenger()
    {
        if (queue.Count == 0) return null;
        return queue[0];
    }

    public void RemoveFrontPassenger()
    {
        if (queue.Count == 0) return;

        queue.RemoveAt(0);
        UpdatePositions();
    }

    void UpdatePositions()
    {
        int count = Mathf.Min(queue.Count, slots.Length);

        for (int i = 0; i < count; i++)
        {
            if (slots[i] != null)
            {
                queue[i].SetFinalPoint(slots[i]);  // Sirf final slot update
            }
        }
    }

    public int Count => queue.Count;
}