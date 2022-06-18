using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
//    [SerializeField] private float speedMovement;
    [SerializeField] private float distanceCanHold = 0.5f;
    //s[SerializeField] private float Distance = 1f;
    [SerializeField] private GameObject pickUpPoint;
    [SerializeField] private GameObject[] rayCastPoints;
    private GameObject holdPosition;
    private Rigidbody2D rb;
    private Vector2 movement;
    [SerializeField] private bool hold = false;
    //[SerializeField] bool isPickUp = false;
    [SerializeField] private RaycastHit2D hit;
    [SerializeField] private LayerMask mask;
    private GameObject holdPackage;
    
    
    private float currentHorizontalSpeed;
    private float currentVerticalSpeed;
    [Header("WALKING")][SerializeField] private float acceleration = 90;
    [SerializeField] private float moveClamp = 13;
    [SerializeField] private float deAcceleration = 60f;
    [SerializeField] private float apexBonus = 2;
    private bool colUp, colRight, colDown, colLeft;

    public Vector3 RawMovement { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();        
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        if ( movement.x > 0 )
        {
            pickUpPoint.transform.eulerAngles = new Vector3(0f, 0f, 0f );
            //   PickUpPoint.transform.eulerAngles = new Vector3(0f, 90f, 0f);
            //PickUpPoint.transform.position = new Vector3(transform.position.x + distanceCanHold, transform.position.y, 0f);
        }
        else if (movement.x < 0)
        {
            pickUpPoint.transform.eulerAngles = new Vector3(0f, 0f,180f );
            //PickUpPoint.transform.position = new Vector3(transform.position.x - distanceCanHold, transform.position.y, 0f);
        }
        if (movement.y > 0)
        {
            pickUpPoint.transform.eulerAngles = new Vector3(0f, 0f, 90f);
            //PickUpPoint.transform.position = new Vector3(transform.position.x, transform.position.y + distanceCanHold, 0f);
        }
        if (movement.y < 0)
        {
            pickUpPoint.transform.eulerAngles = new Vector3(0f, 0f, 270f);
            //PickUpPoint.transform.position = new Vector3(transform.position.x, transform.position.y - distanceCanHold, 0f);
        }
        for (int i = 0; i < rayCastPoints.Length; i++)
        {
            Vector3 toOther = rayCastPoints[i].transform.position - transform.position;
            Debug.DrawRay(transform.position, toOther, Color.yellow);
        }
        CalculateWalk(); // Horizontal movement

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (hold == false)
            {
                Physics2D.queriesStartInColliders = false;
                for (int i = 0; i < rayCastPoints.Length; i++)
                {
                    Vector3 toOther = rayCastPoints[i].transform.position - transform.position;
                    hit = Physics2D.Raycast(transform.position, toOther, distanceCanHold, mask);
                    Debug.DrawRay(transform.position, rayCastPoints[i].transform.position, Color.yellow);
                    //Debug.Log(i);
                    if (hit.collider != null)
                    {
                        
                        hold = true;
                        holdPackage = hit.collider.gameObject;
                        holdPackage.GetComponent<Rigidbody2D>().simulated = false;
                        //holdPackage.GetComponent<Collider2D>().isTrigger = true;
                       // Debug.Log("ray " + i + "--------------------------------------------------------------------------");
                        break;

                    }                    
                }
            }
            else
            {
                hold = false;

                
            }

        }

        if (hold == true)
        {
            holdPackage.transform.position = gameObject.transform.position;
            holdPackage.gameObject.transform.position = rayCastPoints[0].transform.position;
            holdPackage.GetComponent<Rigidbody2D>().simulated = true;
            //holdPackage.gameObject.GetComponent<Collider2D>().isTrigger = false;

        }

    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector3(currentHorizontalSpeed , currentVerticalSpeed , 0f);
        //rb.MovePosition(rb.position + movement * Time.deltaTime * speedMovemetn);


    }

    private void CalculateWalk()
    {
        if (movement.x != 0)
        {
            // Set horizontal move speed
            currentHorizontalSpeed += movement.x * acceleration * Time.deltaTime;

            // clamped by max frame movement
            currentHorizontalSpeed = Mathf.Clamp(currentHorizontalSpeed, -moveClamp, moveClamp);

            // Apply bonus at the apex of a jump
            var _apexBonus = Mathf.Sign(movement.x) * apexBonus;
            currentHorizontalSpeed += apexBonus * Time.deltaTime;
        }
        else
        {
            // No input. Let's slow the character down
            currentHorizontalSpeed = Mathf.MoveTowards(currentHorizontalSpeed, 0, deAcceleration * Time.deltaTime);
        }


        if (movement.y != 0)
        {
            // Set horizontal move speed
            currentVerticalSpeed += movement.y * acceleration * Time.deltaTime;

            // clamped by max frame movement
            currentVerticalSpeed = Mathf.Clamp(currentVerticalSpeed, -moveClamp, moveClamp);

            // Apply bonus at the apex of a jump
            var _apexBonus = Mathf.Sign(movement.x) * apexBonus;
            currentVerticalSpeed += apexBonus * Time.deltaTime;
        }
        else
        {
            // No input. Let's slow the character down
            currentVerticalSpeed = Mathf.MoveTowards(currentVerticalSpeed, 0, deAcceleration * Time.deltaTime);
        }
        //if (currentHorizontalSpeed > 0 && colRight || currentHorizontalSpeed < 0 && colLeft)
        //{
        //    // Don't walk through walls
        //    currentHorizontalSpeed = 0;
        //}

    }

    

}
