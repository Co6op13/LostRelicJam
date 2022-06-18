using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitingService : MonoBehaviour
{
    [SerializeField] private float timeWaitService;
    [SerializeField] private float timeWait;
    [SerializeField] private GameObject progressBar;
    [SerializeField] private float coefficient = 1.9f;
    private float proc, oneProc;
    private bool isSend = false;
    

    private void OnEnable()
    {
        coefficient = progressBar.transform.localScale.y;
        isSend = false;
        oneProc =  100 / timeWaitService;
        timeWait = timeWaitService;
        progressBar.SetActive(true);
    }

    private void FixedUpdate()
    {
        if ((timeWait < 0) & (isSend == false))
        {
            if (gameObject.GetComponent<SClient>() != null)
            {
                Debug.Log("time is gone");
                gameObject.GetComponent<SClient>().GoAway();               
            }

            if (gameObject.GetComponent<RClient>() != null)
            {
                Debug.Log("time is gone");
                gameObject.GetComponent<RClient>().GoAway();              
            }
            progressBar.SetActive(false);
            isSend = true;
        }
        else
        {
            timeWait -= Time.fixedDeltaTime;
            
            proc = oneProc * timeWait /100 * coefficient;
            progressBar.transform.localScale = new Vector3(progressBar.transform.localScale.x, proc, progressBar.transform.localScale.z);
        }
    }
}
