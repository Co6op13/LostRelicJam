using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnClients : MonoBehaviour
{
    [SerializeField] private Transform min, max;
    [SerializeField] private GameObject prefab;
    [SerializeField] private GameObject[] variablePackage;
    private GameObject client;
    bool test = false;


    private void Update()
    {
        if (test == false)
        {
            test = true;
            StartCoroutine(Spawn());

        }
    }


    protected IEnumerator Spawn()
    {
        while (true)
        {
            //Debug.Log(prefab.name);
            client = ClienPool.Instance.GetFromPool(prefab.name, new Vector3(Random.Range(min.position.x, max.position.x), transform.position.y, 0f), transform.rotation);
            var randomPackage = variablePackage[Random.Range(0, variablePackage.Length)];
            var clientScript = client.GetComponent<Client>();
            clientScript.SetNeededPackage(randomPackage);
            client.GetComponent<SpriteRenderer>().color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
            yield return new WaitForSeconds(2f);
        }
    }
}
