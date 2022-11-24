using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;

    //Constantes para animacion
    private const int ANIMATION_IDLE = 0;
    private const int ANIMATION_RUN = 1;
    private const int ANIMATION_DEAD = 2;
    int cont1;
    
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }
    void Update()
    {
        
    }

    private void ChangeAnimation(int animation)
    {
        _animator.SetInteger("Estado", animation);
    }

    void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.tag =="Player" ){
          
        }

        if (other.gameObject.tag == "Bullet")
        {
            cont1++;
            if (cont1 >= 2)
            {
                Destroy(this.gameObject);
              
                
                
            }
        }


      
    }
}
