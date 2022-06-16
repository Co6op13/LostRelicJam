using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPackage : MonoBehaviour
{
    [SerializeField] private Transform left, Right, stop;
    [SerializeField] private GameObject[] variablePackage;
    private bool trig = false;
    //private GameObject newPackage;

    // Start is called before the first frame update
    void Update()
    {
        if (trig == false)
        {
            trig = true;
            StartCoroutine(Spawn());
        }
    }

    protected IEnumerator Spawn()
    {
        while (true)
        {
            
          //  var randomPackage = variablePackage[Random.Range(0, variablePackage.Length)];
          ////  Debug.Log(randomPackage.name);
          //int x = 
          //  PackagePool.Instance.GetFromPool(randomPackage.name, new Vector3 (Random.Range()), transform.rotation);

            yield return new WaitForSeconds(1f);
        }
    }
}
