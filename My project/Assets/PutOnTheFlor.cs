using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PutOnTheFlor : MonoBehaviour
{
    BoxCollider2D BC;

    private void Start()
    {
        BC = GetComponent<BoxCollider2D>();
    }
    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //   BC.isTrigger = false;
    //}
    private void FixedUpdate()
    {
        
    }
}
