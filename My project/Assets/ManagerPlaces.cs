using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ManagerPlaces : MonoBehaviour
{
    [SerializeField] private GameObject[] freePlaces;
    [SerializeField] private Stack<GameObject> freePlacesStack = new Stack<GameObject>();
    [SerializeField] private Stack<GameObject> bisyPlacesStack = new Stack<GameObject>();
    //[SerializeField] GameObject[] places;

    private void Start()
    {
        for (int i = 0; i < freePlaces.Length; i++)
        {
            freePlacesStack.Push(freePlaces[i]);
        }
       
    }

    public GameObject GetFreePlace()
    {
            var tempGameObject = freePlacesStack.Pop();
            bisyPlacesStack.Push(tempGameObject);
            return tempGameObject;
    }

    public bool CanIGetSpace()
    {
        //Debug.Log(freePlacesStack.Count);
        if (freePlacesStack.Count > 0)
        {
            return true;
        }
        else return false;

    }

    public void FreeUpPlace()
    {
        var tempGameObject = bisyPlacesStack.Pop();
        freePlacesStack.Push(tempGameObject);       
    }
}
