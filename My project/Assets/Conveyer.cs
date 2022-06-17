using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conveyer : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    [SerializeField] private ManagerPlaces manager;
    private GameObject place;
    private bool trig = false;
    private bool isFree = true;
    //private GameObject newPackage;

    // Start is called before the first frame update
    void Update()
    {
        manager = GameObject.FindGameObjectWithTag("PackagePlaces").gameObject.GetComponent<ManagerPlaces>();
        if (place == null)
        {
            if (manager.CanIGetSpace() == true)
            {
                place = manager.GetFreePlace();
            }
            else
                isFree = false;
             gameObject.SetActive(false);
        }
        ////// I dont know whi this not work in start ????????????? 
        if (trig == false)
        {
            trig = true;
           // StartCoroutine(Spawn());
        }
    }

    private void FixedUpdate()
    {
        if ((Vector3.Distance(transform.position, place.transform.position) > 0.1f))
            transform.position = Vector3.MoveTowards(transform.position, place.transform.position, speed);
        else
            gameObject.GetComponent<Conveyer>().enabled = false;
    }

    //protected IEnumerator Spawn()
    //{
    //    while (isFree)        
    //    {

    //        //Debug.Log(randomPackage.name);
    //        ////int x = 
    //        //PackagePool.Instance.GetFromPool(randomPackage.name, place.transform.position, transform.rotation);           
    //        yield return new WaitForSeconds(1f);
    //    }
    //}
}
