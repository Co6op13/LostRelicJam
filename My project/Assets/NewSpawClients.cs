using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewSpawClients : MonoBehaviour
{
    
    [SerializeField] private GameObject[] variable;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float timeSpawn = 5f;
    private bool trigerCoroutine = true;
    
    private void Update()
    {
        
        if (trigerCoroutine == true)
        {
            //Debug.Log("test1");
            StartCoroutine( SpawnClients());
            trigerCoroutine = false;
        }
    }

    IEnumerator SpawnClients()
    {
        while (true)
        {
            int x = Random.Range(0, variable.Length);
           //Debug.Log("test2");
            var client = variable[x];
            int offset = -1;
            if (x == 1)
                offset = 1;
            ClienPool.Instance.GetFromPool(client.name, new Vector3(spawnPoint.position.x + offset, spawnPoint.position.y, 0f), spawnPoint.rotation);
            yield return new WaitForSeconds(timeSpawn);
        }
    }
}
