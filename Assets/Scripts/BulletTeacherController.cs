using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTeacherController : MonoBehaviour
{
    private float Speed;
    private GameManagerController gameManager;
   
    private Rigidbody2D rb;
    public float velocity = 20;
    
    // Start is called before the first frame update

    public void SetRightDirection(){
        Speed = velocity;
    }
    public void SetLeftDirection(){
        Speed = -velocity;
    }


    void Start()
    {
        gameManager = FindObjectOfType<GameManagerController>();
        rb = GetComponent<Rigidbody2D>();
        Destroy(this.gameObject,5);
       
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(Speed,0);
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
            //gameManager.PerderVida(1);
            
        }
    }
}