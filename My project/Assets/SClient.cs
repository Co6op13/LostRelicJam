using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SClient : MonoBehaviour
{
    [SerializeField] private ParticleSystem dust;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject[] variablePackage;
    [SerializeField] private GameObject[] queue;
    [SerializeField] private float speed = 3f;
    [SerializeField] private GameObject SendPackage;
    [SerializeField] private BoxCollider2D pickUpPoint;
    [SerializeField] private Transform iñonPoint;
    [SerializeField] private bool isGoAway = false;
    [SerializeField] private Transform exit;
    [SerializeField] private string queueTag;
    [SerializeField] private float iTime;
    [SerializeField] private float cTime;
    [SerializeField] private bool hold = true;

    private GameObject direction;
    private GameObject Package;
    private Transform iconPackage;
    //private bool isReached = false;
    private int currentPositionInQueue;
    private bool trigerCoroutine = true;
    private bool trigerToFreePlace = true;
    private bool isDispleased = false;
    //private bool bisy = true;

    private void OnEnable()
    {
        isGoAway = false;
        trigerCoroutine = true;
        trigerToFreePlace = true;
        hold = true;
        gameObject.GetComponent<WaitingService>().enabled = false;
        isDispleased = false;
    }
    private void Start()
    {
        var temp = GameObject.FindGameObjectWithTag(queueTag);
        queue = new GameObject[temp.transform.childCount];
        for (int i = 0; i < temp.transform.childCount; i++)
        {
            queue[i] = temp.transform.GetChild(i).gameObject;
        }
        //queue = temp.GetComponentsInChildren<FreePlace>();
        currentPositionInQueue = queue.Length;
        spriteRenderer.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        // gameObject.transform.GetComponentInChildren<SpriteRenderer>().color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        GetDirection();
    }

    public void GoAway ()
    {
        isGoAway = true;
        isDispleased = true;
        gameObject.GetComponent<WaitingService>().enabled = false;
        GameManager.Instance.InkreaseDiscontentClients(1);
        Package.GetComponent<BoxCollider2D>().enabled = false;
         Package.SetActive(false);

    }
    private void FixedUpdate()
    {

        if (isGoAway == true)
        {
            if (Vector3.Distance(transform.position, new Vector3(2f, -15f, 0f)) > 0.1f)
            {
                animator.SetBool("isRunning", true);
                CreateDust();
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(2f, -15f, 0f), Time.deltaTime * speed);
                if (trigerToFreePlace == true)
                {
                    if (Vector3.Distance(transform.position, direction.transform.position) > 1f)
                    {
                        if (queue[currentPositionInQueue] != null)
                        {
                            queue[currentPositionInQueue].GetComponent<FreePlace>().isFree = true;
                            trigerToFreePlace = false;
                        }
                    }
                }
            }
            else
            {
                animator.SetBool("isRunning", false);
                gameObject.SetActive(false);
            }
        }
        else
        {
            if (direction != null)
            {
                MoveDirection(direction.transform.position);
            }
            else
                GetDirection();
        }


        if ((trigerCoroutine == true))
        {
            ShowPackage();
            trigerCoroutine = false;
        }


        //Debug.Log(hold + "   " + currentPositionInQueue + "   " + (Vector3.Distance(transform.position, direction.transform.position)));
        if (direction != null)
            if ((hold == true)&  (currentPositionInQueue == 0) & ((Vector3.Distance(transform.position, direction.transform.position)) < 0.1f))
            {
                gameObject.GetComponent<WaitingService>().enabled = true;
                Package.GetComponentInChildren<BoxCollider2D>().enabled = true;
                Package.transform.position = iñonPoint.transform.position;
                hold = false;
            }

        if (hold == true)
        {
            Package.transform.position = gameObject.transform.position;
        }

        if (isDispleased == true)
        {
            Package.transform.position = gameObject.transform.position;
            Package.GetComponentInChildren<BoxCollider2D>().enabled = false;
        }
        
    }





    void MoveDirection(Vector3 currentDirection)
    {
        if ((Vector3.Distance(transform.position, currentDirection) > 0.1f) && (isGoAway == false))
        {
                        CreateDust();
            animator.SetBool("isRunning", true);
            transform.position = Vector3.MoveTowards(transform.position, currentDirection, Time.deltaTime * speed);

        }
        else
        {
            animator.SetBool("isRunning", false);
            GetDirection();
        }
    }

    void GetDirection()
    {

        for (int i = 0; i < currentPositionInQueue; i++)
        {
            if (queue[i].GetComponent<FreePlace>().isFree == true)
            {
                queue[i].GetComponent<FreePlace>().isFree = false;
                currentPositionInQueue = i;
                direction = queue[i].gameObject;
                // bisy = false;
                if (i + 1 < queue.Length)
                    queue[i + 1].GetComponent<FreePlace>().isFree = true;

            }
        }

      
    }


   

    void ShowPackage()
    {
        SendPackage = variablePackage[Random.Range(0, variablePackage.Length)];
        Package = PackagePool.Instance.GetFromPool(SendPackage.name, gameObject.transform.position, gameObject.transform.rotation);
        iconPackage = Package.transform.GetChild(0);
        iconPackage.tag = "notBox";
        Package.GetComponentInChildren<BoxCollider2D>().enabled = false;

    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (isDispleased == false)
        {
            // Debug.Log( "exit" + " " + collision.name);
            if (collision.tag == "notBox")
            {
                //  Debug.Log(collision.tag + " " + collision.name + "   " + SendPackage.name);
                if (collision.name == SendPackage.name)
                {
                    //  Debug.Log("trig"+ " " + collision.name);

                    isGoAway = true;
                    GameManager.Instance.TakePoint(1);
                }
            }
        }
    }


    void CreateDust()
    {
        dust.Play();
    }
}
