using UnityEngine;

public class Car : MonoBehaviour
{
    public int acceptedTypeIndex;
    public int capacity = 4;

    public Transform[] seatPoints;   // Seat positions
    private int filledSeats = 0;

    public PassengerQueueManager queueManager;
    public PassengerSpawner spawner;

    public void TryBoardPassenger()
    {
        if (filledSeats >= capacity) return;

        Passenger frontPassenger = queueManager.GetFrontPassenger();
        if (frontPassenger == null) return;

       if (frontPassenger.typeIndex == acceptedTypeIndex 
    && frontPassenger.hasReachedBoardingPoint)
        
        {
            // Seat target assign karo
            Transform seatTarget = seatPoints[filledSeats];

           // frontPassenger.SetFinalPoint(seatTarget);
           // frontPassenger.SitOnSeat(seatTarget,capacity);
            frontPassenger.SitOnSeat(seatTarget, capacity);

            queueManager.RemoveFrontPassenger();
            spawner.OnPassengerBoarded();

            filledSeats++;

            if (filledSeats >= capacity)
            {
                CarFull();
            }
        }
    }

    void CarFull()
    {
        Debug.Log("Car Full!");

        // Drive animation start
        StartCoroutine(DriveAway());
    }

    System.Collections.IEnumerator DriveAway()
    {
        float moveTime = 20f;
        float timer = 05f;

        Vector3 startPos = transform.position;
        Vector3 endPos = startPos + Vector3.forward * 1f;

        while (timer < moveTime)
        {
            transform.position = Vector3.Lerp(startPos, endPos, timer / moveTime);
            timer += Time.deltaTime;
            yield return null;
        }

        transform.position = endPos;

        Destroy(gameObject);
    }
}