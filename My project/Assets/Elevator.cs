using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed;
    [SerializeField] private Material material;


    private void OnTriggerStay2D(Collider2D collision)
    {      
       // Debug.Log(collision.name);
        collision.transform.position = new Vector3(collision.transform.position.x + speed * Time.deltaTime, collision.transform.position.y, 0f);
    }

  
  
    //private void FixedUpdate()
    //{
    //    material.mainTextureOffset = new Vector2(Time.time * 10 * Time.deltaTime, 0f);
    //    Vector3 pos = rb.position;
    //    rb.position += Vector2.right * speed * Time.fixedDeltaTime;
    //    rb.MovePosition(pos);

    //}
}
