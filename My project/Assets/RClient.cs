using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RClient : MonoBehaviour
{
    //[SerializeField] private bool isSend = false;

    [SerializeField] private ParticleSystem dust;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject[] variablePackage;
    [SerializeField] private GameObject[] queue;
    [SerializeField] private float speed = 3f;
    [SerializeField] private GameObject requiredPackage;
    [SerializeField] private BoxCollider2D pickUpPoint;
    [SerializeField] private Transform iñonPoint;
    [SerializeField] private bool isGoAway = false;
    private bool isDispleased;
    [SerializeField] private Transform exit;
    [SerializeField] private string queueTag;
    [SerializeField] private float iTime;
    [SerializeField] private float cTime;
    [SerializeField] private bool hold = true;
    private GameObject direction;
    private GameObject icon;
    private Transform childIcon;
    //private bool isReached = false;
    private int currentPositionInQueue;
    private bool trigerCoroutine = true;
    private bool trigerToFreePlace = true;

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
        isGoAway = false;
        hold = true;
        var temp = GameObject.FindGameObjectWithTag(queueTag);
        queue = new GameObject[temp.transform.childCount];
        for (int i = 0; i < temp.transform.childCount; i++)
        {
            queue[i] = temp.transform.GetChild(i).gameObject;
            
        }
        //queue = temp.GetComponentsInChildren<FreePlace>();
        currentPositionInQueue = queue.Length;
        spriteRenderer.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        GetDirection();
    }

    public void GoAway()
    {
        isGoAway = true;
        isDispleased = true;
        GameManager.Instance.InkreaseDiscontentClients(1);
    }
    private void FixedUpdate()
    {       

        if (isGoAway == true)
        {
            if (Vector3.Distance(transform.position, new Vector3(-2f, -15f, 0f)) > 0.1f)
            {
                animator.SetBool("isRunning", true);
                CreateDust();
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(-2f, -15f, 0f), Time.deltaTime * speed);
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
            trigerCoroutine = false;
            ShowIcon();
        }
        if (direction != null)
            if ((currentPositionInQueue == 0) & (Vector3.Distance(transform.position, direction.transform.position) < 0.1f))
            {
                gameObject.GetComponent<WaitingService>().enabled = true;
            }   

        if (isDispleased == true)
        {
            //collision.gameObject.transform.parent.gameObject.SetActive(false);
            childIcon.tag = "BOX";
            icon.SetActive(false);

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


    void ShowIcon()
    {
        requiredPackage = variablePackage[Random.Range(0, variablePackage.Length)];
        icon = PackagePool.Instance.GetFromPool(requiredPackage.name, iñonPoint.position, iñonPoint.rotation);
        childIcon = icon.transform.GetChild(0);
        childIcon.tag = "notBox";
        icon.GetComponentInChildren<BoxCollider2D>().enabled = false;
        //isReached = true;
        StartCoroutine(SpawnIcon());
    }

    //void ShowPackage()
    //{
    //    var randomPackage = variablePackage[Random.Range(0, variablePackage.Length)];
    //    icon = PackagePool.Instance.GetFromPool(randomPackage.name, gameObject.transform.position, gameObject.transform.rotation);        
    //    icon.GetComponentInChildren<BoxCollider2D>().enabled = false;
    //    //isReached = true;
    //    //StartCoroutine(SpawnIcon());
    //}

    //IEnumerator WaitIfBisy (float timeWait)
    //{
    //    yield return new WaitForSeconds(timeWait);
    //    if (bisy == true)
    //    {
    //        isRecived = true;
    //        icon.gameObject.SetActive(false);
    //    }
    //}
     IEnumerator SpawnIcon()
    {
        while (isGoAway == false)
        {
            for (float q = 0.5f; q < 1.2f; q += iTime)
            {
                var position = iñonPoint.transform.position;
                icon.transform.localScale = new Vector3(q, q, q);
                var temp = q;
                if (queueTag == "ReceivePlace")
                    temp = -temp;
                    
                icon.transform.position = new Vector3(position.x + temp, position.y + q, 0f); 
                yield return new WaitForSeconds(cTime);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("triget");
        if (collision.tag == "BOX")
        {
            //Debug.Log("tag box");
            //Debug.Log(collision.name + "  " + requiredPackage.name);
            if (collision.name == requiredPackage.name)
            {
                //Debug.Log(collision.name +"  "+ icon.name);
                collision.gameObject.transform.parent.gameObject.SetActive(false);
                childIcon.tag = "BOX";
                icon.SetActive(false);
                isGoAway = true;
                GameManager.Instance.TakePoint(1);
            }
        }
    }

    void CreateDust()
    {
        dust.Play();
    }
}
