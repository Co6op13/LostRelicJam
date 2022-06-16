using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PutOnShelf : MonoBehaviour
{
    [SerializeField] private bool isBysy = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isBysy == false)
        {
            Debug.Log("test1");
            if (collision.gameObject.tag == "BOX")
            {
                collision.gameObject.transform.parent.position = transform.position;
                collision.gameObject.transform.parent.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
                isBysy = true;
                Debug.Log("test");

            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isBysy = false;
      //  collision.gameObject.transform.parent.gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
    }
}
