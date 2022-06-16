using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speedMovemetn;
    [SerializeField] private float distanceCanHold = 0.5f;
    //s[SerializeField] private float Distance = 1f;
    [SerializeField] private GameObject PickUpPoint;
    private GameObject holdPosition;
    private Rigidbody2D rb;
    private Vector2 movement;
    [SerializeField] private bool hold = false;
    //[SerializeField] bool isPickUp = false;
    [SerializeField] private RaycastHit2D hit;
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
            PickUpPoint.transform.position = new Vector3(transform.position.x + distanceCanHold, transform.position.y, 0f);
        }
        else if (movement.x < 0)
        {
            PickUpPoint.transform.position = new Vector3(transform.position.x - distanceCanHold, transform.position.y, 0f);
        }
        if (movement.y > 0)
        {
            PickUpPoint.transform.position = new Vector3(transform.position.x, transform.position.y + distanceCanHold, 0f);
        }
        if (movement.y < 0)
        {
            PickUpPoint.transform.position = new Vector3(transform.position.x, transform.position.y - distanceCanHold, 0f);
        }


        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (hold == false)
            {
                Debug.Log("try picup");
                Physics2D.queriesStartInColliders = false;
                hit = Physics2D.Raycast(transform.position, PickUpPoint.transform.position, distanceCanHold, mask);
                if (hit.collider != null)
                {
                    //isPickUp = true;
                    hold = true;
                    hit.collider.gameObject.GetComponent<Collider2D>().isTrigger = true;
                }

            }
        }


        if (hold == true)
        {
           hit.collider.gameObject.transform.position = gameObject.transform.position;
            if (Input.GetKeyDown(KeyCode.P))
            {
                Debug.Log("try put back");
                hold = false;
                hit.collider.gameObject.transform.position = PickUpPoint.transform.position;
                hit.collider.gameObject.GetComponent<Collider2D>().isTrigger = false;
            }
        }  

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, PickUpPoint.transform.position);
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * Time.deltaTime * speedMovemetn);
    }
}
