
using UnityEngine;
using System.Collections;

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



[Header("Animation")]

private Animator animator;
    public static bool boardingBusy = false;



    void Start()
{
    if (animator == null)
        animator = GetComponent<Animator>();
}
    
    
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

                animator.SetBool("IsWaking", true);
               
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
            animator.SetBool("IsWaking", true);
            float moveX = Mathf.Sign(xDiff) * moveSpeed * Time.deltaTime;
            transform.position += new Vector3(moveX, 0, 0);
            transform.forward = new Vector3(Mathf.Sign(xDiff), 0, 0);
        }
       /* else
        {
            reachedFinal = true;
            animator.SetBool("IsWaking", false);

              if (!isSeated)
                {
                    hasReachedBoardingPoint = true;
                }
        }     */


        else
            {
                reachedFinal = true;
                animator.SetBool("IsWaking", false);

                if (!isSeated && !boardingBusy)
                {
                    hasReachedBoardingPoint = true;
                    boardingBusy = true;   // lock boarding point
                }
            }
    }

public void SitOnSeat(Transform seatTarget, int seatCapacity)
{
    StartCoroutine(SitMove(seatTarget, seatCapacity));
}

IEnumerator SitMove(Transform seatTarget, int seatCapacity)
{
    isSeated = true;

    animator.SetBool("IsWaking", false);
    animator.SetBool("IsSeating", true);

    Vector3 startPos = transform.position;
    Vector3 endPos = seatTarget.position;

    float t = 0f;
    float speed = 4f;
    float jumpHeight = 0.2f;

    while (t < 1f)
    {
        t += Time.deltaTime * speed;

        Vector3 pos = Vector3.Lerp(startPos, endPos, t);
        pos.y += Mathf.Sin(t * Mathf.PI) * jumpHeight;

        transform.position = pos;

        yield return null;
    }

    transform.SetParent(seatTarget);
    transform.localPosition = Vector3.zero;
    transform.localRotation = Quaternion.identity;

    // 🔹 Custom scale (jaise tumne pehle rakha tha)
    if (seatCapacity == 4)
    {
        transform.localScale = new Vector3(0.959999979f,0.87272948f,1.30909443f);
    }
    else if (seatCapacity == 6)
    {
        transform.localScale = new Vector3(2.08999991f,1.89999819f,2.849998f);
    }
    else if (seatCapacity == 10)
    {
        transform.localScale = new Vector3(95.9000015f,87.1818085f,130.772736f);
    }

    boardingBusy = false;
   // hasReachedBoardingPoint = false;
}

}


