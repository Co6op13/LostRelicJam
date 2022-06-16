using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Client : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private ManagerPlaces manager;
    private GameObject place;
    [SerializeField] private GameObject requiredPackage;
    [SerializeField] private BoxCollider2D pickUpPoint;
    [SerializeField] private Transform i�onPoint;
    [SerializeField] private bool isRecived = false;
    [SerializeField] private Transform exit;
    private GameObject icon;
    private Transform childIcon;
    private bool isReached = false;


    private void Start()
    {
        
       
        manager = FindObjectOfType<ManagerPlaces>();
        exit =    FindObjectOfType<SpawnClients>().GetComponent<Transform>();
        if (place == null)
        {
            if (manager.CanIGetSpace() == true)
            {
                place = manager.GetFreePlace();
            }
            else
                gameObject.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        if ((Vector3.Distance(transform.position, place.transform.position) > 0.1f) && (isRecived == false))
            transform.position = Vector3.MoveTowards(transform.position, place.transform.position, Time.deltaTime * speed);
        else
        {
            if (isReached == false)
            {
                icon = PackagePool.Instance.GetFromPool(requiredPackage.name, i�onPoint.position, i�onPoint.rotation);
                //icon.GetComponentInChildren<BoxCollider2D>().enabled = false;
                childIcon = icon.transform.GetChild(0);

                childIcon.tag = "notBox";
                icon.GetComponentInChildren<BoxCollider2D>().enabled = false;
                //icon.GetComponent<BoxCollider2D>().enabled = false;
               // icon.SetActive(true);              // icon = PackagePool.Instance.GetFromPool(requiredPackage.name, i�onPoint.position, i�onPoint.rotation);
                isReached = true;
                StartCoroutine(SpawnIcon());
            }
           
        }
        if (isRecived == true)
        {
            if (Vector3.Distance(transform.position, exit.position) > 0.1f)
                transform.position = Vector3.MoveTowards(transform.position, exit.position, Time.deltaTime * speed);
            else
                gameObject.SetActive(false);
        }


    }

    public void SetNeededPackage(GameObject Package)
    {
        requiredPackage = Package;

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("triget");
        if (collision.tag == "BOX")
        {
            Debug.Log("tag box");
            if (collision.name == requiredPackage.name)
            {
                //Debug.Log();
                collision.gameObject.transform.parent.gameObject.SetActive(false);
                childIcon.tag = "BOX";
                icon.SetActive(false);
                isRecived = true;
            }


        }
    }

    private void OnDisable()
    {
        if (place != null)
        {
            manager.FreeUpPlace(place);
        }
    }


    protected IEnumerator SpawnIcon()
    {
        while (isRecived == false)
        {
            for (float q = 0.5f; q < 1.2f; q += .05f)
            {
                icon.transform.localScale = new Vector3(q, q, q);
                yield return new WaitForSeconds(.1f);
            }
        }
    }

}
