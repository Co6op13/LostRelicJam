using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    #region Singleton
    [SerializeField] private int record = 0;
    [SerializeField] private int discontent = 0;
    [SerializeField] private int discontentMax = 20;
    [SerializeField] private AudioSource takePoint;
    [SerializeField] private AudioSource discontentSound;
    [SerializeField] private AudioSource ShipmetSound;
    [SerializeField] private TMP_Text recordText;
    [SerializeField] private TMP_Text discontentText;

    public static GameManager Instance;

    private void Awake()
    {
        Instance = this;
        recordText.text = "Record : " + record;
        discontentText.text = "Discontent : " + discontent + " of " + discontentMax;
    }
#endregion


    public void TakePoint(int point)
    {
        record += point;
        discontentText.text = "Record : " + record;
        takePoint.pitch = Random.Range(0.8f, 1.2f);
        takePoint.Play(0);
    }

    public void InkreaseDiscontentClients (int point)
    {
        discontent += point;
        discontentText.text = "Discontent : " + discontent + " of " + discontentMax;
        takePoint.pitch = Random.Range(0.8f, 1.2f);
        discontentSound.Play(0);
    }

    public void ShipmentsGo(int point)
    {
        discontent += point;
        recordText.text = "Discontent : " + discontent + " of " + discontentMax;
        takePoint.pitch = Random.Range(0.8f, 1.2f);
        ShipmetSound.Play(0);
    }

    
}
