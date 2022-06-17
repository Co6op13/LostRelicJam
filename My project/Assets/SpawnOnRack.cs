using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOnRack : MonoBehaviour
{
    [SerializeField] private GameObject[] variablePackage;
    [SerializeField] private GameObject[] shelf;
    bool trig = true;


    private void Update()
    {
        if ( trig )
        {
            for (int i = 0; i < shelf.Length; i++ )
            {
                var randomPackage = variablePackage[Random.Range(0, variablePackage.Length)];
                var package =  PackagePool.Instance.GetFromPool(randomPackage.name, shelf[i].transform.position, transform.rotation);
                package.GetComponent<Rigidbody2D>().simulated = false;


            }
        }
    }
}
