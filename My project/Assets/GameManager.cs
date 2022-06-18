using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton
    [SerializeField] private int record = 0;
    [SerializeField] private int discontentClients = 0;
    [SerializeField] private AudioSource takePoint;
    [SerializeField] private AudioSource discontentSound;

    public static GameManager Instance;

    private void Awake()
    {
        Instance = this;
    }
#endregion


    public void TakePoint(int point)
    {
        record += point;
        takePoint.pitch = Random.Range(0.8f, 1.2f);
        takePoint.Play(0);
    }

    public void InkreaseDiscontentClients (int point)
    {
        discontentClients += point;
        takePoint.pitch = Random.Range(0.8f, 1.2f);
        discontentSound.Play(0);
    }
}
