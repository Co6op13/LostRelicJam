using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewSpawnPackage : MonoBehaviour
{
    [SerializeField] private Transform min, max;
    [SerializeField] private GameObject[] variable;
    [SerializeField] private float timeSpawn;
    private bool trigerCoroutine = true;
    private void Update()
    {
        if (trigerCoroutine == true)
        {
            StartCoroutine(SpawnPackage());
            trigerCoroutine = false;
        }
    }

    IEnumerator SpawnPackage()
    {
        while (true)
        {
            //Debug.Log("test2");
            int pos = Random.Range(1, 8);
            float randomPos = pos * 1.5f - 3;
            var package = variable[Random.Range(0, variable.Length)];
            PackagePool.Instance.GetFromPool(package.name, new Vector3(transform.position.x, randomPos, 0f), transform.rotation);
            yield return new WaitForSeconds(timeSpawn);
        }
    }
}
