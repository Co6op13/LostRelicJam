using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPackage : MonoBehaviour
{
    [SerializeField] private Transform left, Right, stop;
    [SerializeField] private GameObject[] variablePackage;
    //private GameObject newPackage;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawn());
    }

    protected IEnumerator Spawn()
    {
        while (true)
        {
            
            var randomPackage = variablePackage[Random.Range(0, variablePackage.Length)];
            Debug.Log(randomPackage.name);

            //PackagePool.Instance.GetFromPool(randomPackage.name, transform.position, transform.rotation);

            yield return new WaitForSeconds(1f);
        }
    }
}
