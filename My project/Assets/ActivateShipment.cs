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
    private float proc, oneProc;
    private bool isSend = false;
    private bool trigerCoroutine = true;
    private float timeWait;
    private bool isShipmentOver = false;

    private void Start()
    {
        coefficient = progressBar.transform.localScale.y;
        oneProc = 100 / timeWaitService;
        timeWait = timeWaitService;
       
    }


    private void Update()
    {
        if (trigerCoroutine == true)
        {
            trigerCoroutine = false;
            StartCoroutine(ActiveShipmetPoint());
        }
    }

    private void FixedUpdate()
    {
        if ((timeWait < 0) & (isSend == false))
        {
            GameManager.Instance.InkreaseDiscontentClients(5);

        }
        else
        {
            timeWait -= Time.fixedDeltaTime;

            proc = oneProc * timeWait / 100 * coefficient;
            progressBar.transform.localScale = new Vector3(progressBar.transform.localScale.x, proc, progressBar.transform.localScale.z);
        }
    }

    IEnumerator ActiveShipmetPoint ()
    {
        while (isShipmentOver == false)
        {
            for (int i = 0; i < shipments.Length; i++)
                if (shipments[i].gameObject.activeSelf == false)
                {
                    shipments[i].gameObject.SetActive(true);
                    //yield return new WaitForSeconds(200f);
                }
            yield return new WaitForSeconds(timeActivation);
        }


    }
}
