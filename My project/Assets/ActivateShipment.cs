using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateShipment : MonoBehaviour
{
    [SerializeField] private float timeWaitService;    
    [SerializeField] private GameObject[] shipments;
    [SerializeField] private GameObject progressBar;
    [SerializeField] private float timeActivation;
    [SerializeField] private float coefficient;
    [SerializeField] private float proc, oneProc;
    private bool isSend = false;
    private bool trigerActivation = true;
    private float timeWait;
    private bool isShipmentOver = false;

    private void Start()
    {
        coefficient = progressBar.transform.localScale.y;
        oneProc = 100 / timeWaitService;
        timeWait = timeWaitService;       
    }



    private void FixedUpdate()
    {
        if (trigerActivation == true)
        {
            trigerActivation = false;
            ReActiveShipmets();
        }

        if ((timeWait < 0) & (isSend == false))
        {
            GameManager.Instance.InkreaseDiscontentClients(5);
            timeWait = timeWaitService;
            ReActiveShipmets();
        }
        else
        {
            timeWait -= Time.fixedDeltaTime;
            proc = oneProc * timeWait / 100 * coefficient;
            progressBar.transform.localScale = new Vector3(progressBar.transform.localScale.x, proc , progressBar.transform.localScale.z);
            if (CheckActiveShipments() == false)
            {
                GameManager.Instance.ShipmentsGo(5);
                timeWait = timeWaitService;
                ReActiveShipmets();
            }
            //float pos = 
            //progressBar.transform.position = new Vector3(progressBar.transform.position.x,
            //    transform.position.y - timeWait / 100 * coefficient, progressBar.transform.position.z);
        }
    }

    void ReActiveShipmets()
    {
        for (int i = 0; i < shipments.Length; i++)           
        {
            shipments[i].gameObject.SetActive(false);
            shipments[i].gameObject.SetActive(true);
        }

    }

    bool CheckActiveShipments()
    {
        for (int i = 0; i < shipments.Length; i++)
        {
            if (shipments[i].gameObject.activeSelf == true)
            {
                return true;
            }
        }
        return false;
    }
    //IEnumerator ActiveShipmetPoint ()
    //{
    //    while (isShipmentOver == false)
    //    {
    //        for (int i = 0; i < shipments.Length; i++)
    //            if (shipments[i].gameObject.activeSelf == false)
    //            {
    //                shipments[i].gameObject.SetActive(true);
    //                //yield return new WaitForSeconds(200f);
    //            }
    //        yield return new WaitForSeconds(timeActivation);
    //    }


    //}
}
