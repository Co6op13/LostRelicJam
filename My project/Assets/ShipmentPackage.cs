using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipmentPackage : MonoBehaviour
{

    [SerializeField] private GameObject[] variable;
    private GameObject icon;
    private Transform childIcon;
    private GameObject requiredPackage;
    private bool trigerCoroutine = true;
    [SerializeField] private float cTime, iTime;
    [SerializeField] private bool isRecived = false;

    private void OnEnable()
    {
        isRecived = false;
        trigerCoroutine = true;
    }

    private void Update()
    {
        if (trigerCoroutine == true)
        {
            requiredPackage = variable[Random.Range(0, variable.Length)];
            icon = PackagePool.Instance.GetFromPool(requiredPackage.name, transform.position, transform.rotation);
            icon.GetComponent<SpriteRenderer>().sortingOrder = 1000;
            childIcon = icon.transform.GetChild(0);
            childIcon.tag = "notBox";
            childIcon.GetComponent<SpriteRenderer>().sortingOrder = 1000;
            icon.GetComponentInChildren<BoxCollider2D>().enabled = false;
            //isReached = true;
            StartCoroutine(SpawnIcon());
            trigerCoroutine = false;
        }

        if (isRecived == true)
        {
            gameObject.SetActive(false);
        }
    }

    IEnumerator SpawnIcon()
    {
        while (isRecived == false)
        {
            for (float q = 0.5f; q < 1.2f; q += iTime)
            {
                var position = transform.position;
                icon.transform.localScale = new Vector3(q, q, q);
                icon.transform.position = new Vector3(position.x + q, position.y + q, 0f);
                yield return new WaitForSeconds(cTime);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("triget");
        if (collision.tag == "BOX")
        {
            Debug.Log("tag box");
            Debug.Log(collision.name + "  " + requiredPackage.name);
            if (collision.name == requiredPackage.name)
            {
                Debug.Log(collision.name +"  "+ icon.name);
                collision.gameObject.transform.parent.gameObject.SetActive(false);
                icon.GetComponent<SpriteRenderer>().sortingOrder = 610;
                childIcon.GetComponent<SpriteRenderer>().sortingOrder = 600;
                childIcon.tag = "BOX";
                icon.SetActive(false);
                isRecived = true;
                GameManager.Instance.TakePoint(1);
            }
        }
    }
}
