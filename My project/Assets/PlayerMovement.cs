using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speedMovemetn;
    [SerializeField] private float distanceCanHold = 0.5f;
    //s[SerializeField] private float Distance = 1f;
    [SerializeField] private GameObject pickUpPoint;
    [SerializeField] private GameObject[] rayCastPoints;
    private GameObject holdPosition;
    private Rigidbody2D rb;
    private Vector2 movement;
    [SerializeField] private bool hold = false;
    //[SerializeField] bool isPickUp = false;
    [SerializeField] private RaycastHit2D[] hits;
    [SerializeField] private LayerMask mask;
    

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


        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (hold == false)
            {
                Physics2D.queriesStartInColliders = false;
                for (int i = 0; i < rayCastPoints.Length; i++)
                {
                    hits[i] = Physics2D.Raycast(transform.position, rayCastPoints[i].transform.position, distanceCanHold, mask);

                    if (hits[i].collider != null)
                    {

                        hold = true;
                        hits[i].collider.gameObject.GetComponent<Collider2D>().isTrigger = true;

                    }
                }
            }
            else
            {
                hold = false;

                //if ( hit.collider.gameObject.GetComponent<Rigidbody2D>() != null)
                //{
                //    /// hit.collider.gameObject.GetComponent<Rigidbody2D>().
                //}
            }

        }

        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    if (hold == false)
        //    {
        //        Debug.Log("try picup");
        //        Physics2D.queriesStartInColliders = false;
        //        hit = Physics2D.Raycast(transform.position, PickUpPoint.transform.position, distanceCanHold, mask);
        //        if (hit.collider != null)
        //        {
        //            //isPickUp = true;
        //            hold = true;
        //            hit.collider.gameObject.GetComponent<Collider2D>().isTrigger = true;
        //        }

        //    }
        //}


        if (hold == true)
        {
            for (int i = 0; i < rayCastPoints.Length; i++)
            {
                if (hits[i].collider != null)
                {
                    hits[i].collider.gameObject.transform.position = gameObject.transform.position;
                    Debug.Log("try put back");
                    hits[i].collider.gameObject.transform.position = rayCastPoints[0].transform.position;
                    hits[i].collider.gameObject.GetComponent<Collider2D>().isTrigger = false;
                }
            }
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        for (int i = 0; i < rayCastPoints.Length; i++)
        {
            Gizmos.DrawLine(transform.position, rayCastPoints[i].transform.position);
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * Time.deltaTime * speedMovemetn);
    }
}
