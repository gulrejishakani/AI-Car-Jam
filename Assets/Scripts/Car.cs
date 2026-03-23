/*using UnityEngine;

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

    // Passenger correct color ka ho aur boarding point reach kar chuka ho
    //if (frontPassenger.typeIndex == acceptedTypeIndex &&
       // frontPassenger.hasReachedBoardingPoint)


if (frontPassenger.typeIndex == acceptedTypeIndex &&
    frontPassenger.hasReachedBoardingPoint)



    {
        // Passenger ko reserve karo taki ek hi car use le
        //frontPassenger.isReserved = true;
        
        // Seat select karo
        Transform seatTarget = seatPoints[filledSeats];

        // Passenger ko seat pe bhejo
        frontPassenger.SitOnSeat(seatTarget, capacity);

        // Queue update
        queueManager.RemoveFrontPassenger();
        spawner.OnPassengerBoarded();

        filledSeats++;

        // Car full ho gayi
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
}*/

using UnityEngine;

public class Car : MonoBehaviour
{
    public int acceptedTypeIndex;
    public int capacity = 4;

    public Transform[] seatPoints;
    private int filledSeats = 0;

    public PassengerQueueManager queueManager;
    public PassengerSpawner spawner;

    public void TryBoardPassenger()
    {
        if (filledSeats >= capacity) return;

        // ✅ GLOBAL LOCK CHECK
        if (Passenger.boardingBusy) return;

        Passenger frontPassenger = queueManager.GetFrontPassenger();

        if (frontPassenger == null) return;

        if (frontPassenger.typeIndex == acceptedTypeIndex &&
            frontPassenger.hasReachedBoardingPoint)
        {
            // 🔒 LOCK (ONLY HERE)
            Passenger.boardingBusy = true;

            Transform seatTarget = seatPoints[filledSeats];

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
        StartCoroutine(DriveAway());
    }

    System.Collections.IEnumerator DriveAway()
    {
        float moveTime = 20f;
        float timer = 0f;

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