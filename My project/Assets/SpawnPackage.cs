using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPackage : MonoBehaviour
{
    [SerializeField] private ManagerPlaces manager;
    [SerializeField] private GameObject[] variablePackage;
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
               // gameObject.SetActive(false);
        }
        ////// I dont know whi this not work in start ????????????? 
        if (trig == false)
        {
            trig = true;
            StartCoroutine(Spawn());
        }
    }

    protected IEnumerator Spawn()
    {
        while (isFree)
        {
            
            var randomPackage = variablePackage[Random.Range(0, variablePackage.Length)];
            Debug.Log(randomPackage.name);
            //int x = 
            PackagePool.Instance.GetFromPool(randomPackage.name, place.transform.position, transform.rotation);
            place = null;
            yield return new WaitForSeconds(0.01f);
        }
    }
}
