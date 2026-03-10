
using UnityEngine;

public class Passenger : MonoBehaviour
{
    public int typeIndex;
    public float moveSpeed = 2f;

    private Transform boardingPoint;
    private bool reachedZ = false;
    private bool reachedFinal = false;

    [Header("Car Seats")]
    public bool hasReachedBoardingPoint = false;
private bool isSeated = false;

    public void SetFinalPoint(Transform finalPoint)
    {
        boardingPoint = finalPoint;
        reachedZ = false;
        reachedFinal = false;
    }

    void Update()
    {
       if (isSeated) return;
       
        //if (boardingPoint == null || reachedFinal) return;
        if (boardingPoint == null || reachedFinal && isSeated) return;

        Vector3 current = transform.position;
        Vector3 target = boardingPoint.position;

        // 🔹 Step 1: First align Z (vertical movement)
        if (!reachedZ)
        {
            float zDiff = target.z - current.z;

            if (Mathf.Abs(zDiff) > 0.05f)
            {
                float moveZ = Mathf.Sign(zDiff) * moveSpeed * Time.deltaTime;
                transform.position += new Vector3(0, 0, moveZ);
                transform.forward = new Vector3(0, 0, Mathf.Sign(zDiff));
            }
            else
            {
                reachedZ = true;
            }

            return;
        }

        // 🔹 Step 2: Then move in X (horizontal)
        float xDiff = target.x - transform.position.x;

        if (Mathf.Abs(xDiff) > 0.05f)
        {
            float moveX = Mathf.Sign(xDiff) * moveSpeed * Time.deltaTime;
            transform.position += new Vector3(moveX, 0, 0);
            transform.forward = new Vector3(Mathf.Sign(xDiff), 0, 0);
        }
        else
        {
            reachedFinal = true;

              if (!isSeated)
    {
        hasReachedBoardingPoint = true;
    }
        }


       
    }


// Car Working Code 4 Seater

/*public void SitOnSeat(Transform seatTarget)
{
    isSeated = true;

    transform.SetParent(seatTarget);

    transform.localPosition = Vector3.zero;
    transform.localRotation = Quaternion.identity;

    Vector3 desiredScale = new Vector3(0.819999993f,0.745456696f,1.11818504f);
   // transform.localScale = new Vector3(0.580501616f,0.527728736f,0.791593134f);

   Vector3(1.67999995,1.52727246,2.29090905)
   
}*/




//7-3-2026

public void SitOnSeat(Transform seatTarget, int seatCapacity)
{
    isSeated = true;

    transform.SetParent(seatTarget);
    transform.localPosition = Vector3.zero;
    transform.localRotation = Quaternion.identity;

    if (seatCapacity == 4)
    {
       // transform.localScale = new Vector3(0.819999993f,0.745456696f,1.11818504f);
        transform.localScale = new Vector3(0.959999979f,0.87272948f,1.30909443f);
        
    }
    else if (seatCapacity == 6)
    {
        //transform.localScale = new Vector3(0.580501616f,0.527728736f,0.791593134f);
       //transform.localScale = new  Vector3(2.28999996f,2.0818162f,3.12272501f);
       transform.localScale = new   Vector3(2.08999991f,1.89999819f,2.849998f);
    }
    else if (seatCapacity == 10)
    {
       // transform.localScale = new Vector3(1.67999995f,1.52727246f,2.29090905f);
        
        transform.localScale = new Vector3(95.9000015f,87.1818085f,130.772736f);
    }
}






// cube 3 Vector3(1.77999997,0.430000007,-0.800000012)
//cube 4  Vector3(2.43000007,0.430000007,-0.800000012)

// Seat Vector3(2.49000001,0.180000007,-0.379999995)
//seat 4 Vector3(1.76999998,0.180000007,-0.379999995)
}

//Vector3(1.76999998,0.180000007,0.698000014)