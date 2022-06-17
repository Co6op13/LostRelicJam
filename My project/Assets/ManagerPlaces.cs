using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ManagerPlaces : MonoBehaviour
{
    [SerializeField] private GameObject[] freePlaces;    
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
    }

    public void FreeUpPlace(GameObject place)
    {
        place.GetComponent<FreePlace>().isFree = false;     
    }
}
