/*using UnityEngine;

public class ParkingSlot : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Car car = other.GetComponent<Car>();

        if (car != null)
        {
            car.TryBoardPassenger();
        }
    }
}


*/

using UnityEngine;
using System.Collections;

public class ParkingSlot : MonoBehaviour
{

    private Coroutine boardingRoutine;
   /* private void OnTriggerEnter(Collider other)
    {
        Car car = other.GetComponent<Car>();

        if (car != null)
        {
            StartCoroutine(AutoBoard(car));
        }
    }*/

private void OnTriggerEnter(Collider other)
{
    Car car = other.GetComponent<Car>();

    if (car != null)
    {
        boardingRoutine = StartCoroutine(AutoBoard(car));
    }
}



    IEnumerator AutoBoard(Car car)
    {
        while (car != null)
        {
            car.TryBoardPassenger();
            yield return new WaitForSeconds(0.2f);
        }
    }
}




