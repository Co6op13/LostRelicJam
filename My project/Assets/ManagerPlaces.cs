using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ManagerPlaces : MonoBehaviour
{
    [SerializeField] private GameObject[] freePlaces;    
    //[SerializeField] GameObject[] places;

    private void Start()
    {
        //for (int i = 0; i < freePlaces.Length; i++)
        //{
        //    freePlacesStack.Push(freePlaces[i]);
        //}
       
    }

    public GameObject GetFreePlace()
    {
        for (int i = 0; i < freePlaces.Length; i++)
        {
            if (freePlaces[i].GetComponent<FreePlace>().isFree == true)
            {
                freePlaces[i].GetComponent<FreePlace>().isFree = false;
                return freePlaces[i].gameObject;
            }
        }

        return gameObject;
            //var tempGameObject = freePlacesStack.Pop();
            //bisyPlacesStack.Push(tempGameObject);
            //return tempGameObject;
    }

    public bool CanIGetSpace()
    {
        for (int i = 0; i < freePlaces.Length; i++)
        {
            if (freePlaces[i].GetComponent<FreePlace>().isFree == true)
            {
                return true;
            }
        }
        return false;
        //Debug.Log(freePlacesStack.Count);
        //if (freePlacesStack.Count > 0)
        //{
        //    return true;
        //}
        //else return false;

    }

    public void FreeUpPlace(GameObject place)
    {
        place.GetComponent<FreePlace>().isFree = false;
        //var tempGameObject = bisyPlacesStack.Pop();
        //freePlacesStack.Push(tempGameObject);       
    }
}
