using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class ObjectPooler : MonoBehaviour
{
    [System.Serializable, ExecuteAlways]
    public class Pool
    {
        private string tag;
        public GameObject prefab;
        public int size;

        [SerializeField] public string Tag { get => prefab.name; set => tag = prefab.name; }
    }


    [SerializeField] protected Transform parentObject;
    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    private void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject newObject = Instantiate(pool.prefab);
                newObject.transform.SetParent(parentObject);
                newObject.SetActive(false);
                objectPool.Enqueue(newObject);
            }

            poolDictionary.Add(pool.Tag, objectPool);
        }
    }

    public GameObject GetFromPool(string tag, Vector3 posistion, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag = " + tag + " doesn't exist!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            return null;
        }

        GameObject takingObject = poolDictionary[tag].Dequeue();

        takingObject.transform.position = posistion;
        takingObject.transform.rotation = rotation;
        takingObject.SetActive(true);

        poolDictionary[tag].Enqueue(takingObject);

        return takingObject;
    }


}
