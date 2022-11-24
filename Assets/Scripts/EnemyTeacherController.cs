using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTeacherController : MonoBehaviour
{
    
    private float tiempo;

    public GameObject disparo;
    private Transform player;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        tiempo += Time.deltaTime;
        if (tiempo >= 3)
        {
            

            var shieldPosition = transform.position + new Vector3(-1,0,0);
            var gb = Instantiate(disparo,
                shieldPosition,
                Quaternion.identity) as GameObject;
            var controller =gb.GetComponent<BulletTeacherController>();
            controller.SetLeftDirection(); 
            tiempo = 0;
            

        }

    }
}
