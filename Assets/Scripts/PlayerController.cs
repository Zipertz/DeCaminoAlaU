using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{


    private Rigidbody2D _rigidbody;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;

    public int velocidadPlayer, fuerzaSalto;

    public GameObject bulletFire;

    private GameManagerController _gameManager;
    
    private Vector3 lastCheckPointPosition;
    

    private int murio = 0;
    private bool puedeSaltar = true;

    //Constantes para animacion
    private const int ANIMATION_IDLE = 0;
    private const int ANIMATION_RUN = 1;
    private const int ANIMATION_JUMP = 2;
    private const int ANIMATION_ATTACK = 3;
    private const int ANIMATION_DEAD = 4;
    private const int ANIMATION_SHOT = 5;

    
    
    
    public int cantidadBalas = 0;
    

    
    void Start()
    {
        _gameManager = FindObjectOfType<GameManagerController>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();

    }
    
    
    
    void Update()
    {
        int valorBalas = _gameManager.balas;
        cantidadBalas = valorBalas;
        
        //DESPLAZAR DERECHA
        if (Input.GetKey(KeyCode.RightArrow))
        {
            CaminarDerecha();
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            if (velocidadPlayer == 0)
            {
                if (murio == 3)
                {
                    MorirPlayer();
                }
            }
            else
            {
                
                DetenerPlayer();
                
            }
            
            
        }
        
        //DESPALAZAR IZQUIERDA
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            CaminarIzquierda();
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            DetenerPlayer();
        }
        
        //SALTAR
        if (Input.GetKeyDown(KeyCode.UpArrow)&& puedeSaltar)
        {
            SaltarPlayer();
        }

        //DISPARAR
        if (Input.GetKeyDown(KeyCode.X) )
        {
            if (cantidadBalas >= 1)
            {
                DispararPlayer();
                
            }
            else
            {
                Debug.Log("No tiene balas");
            }
        }
        if (Input.GetKeyUp(KeyCode.X))
        {
            DetenerPlayer();
        }
        
        //ATACAR
        if ( Input.GetKeyDown(KeyCode.Z))
        {
            ChangeAnimation(ANIMATION_ATTACK);
        }
        if (Input.GetKeyUp(KeyCode.Z))
        {
            ChangeAnimation(ANIMATION_IDLE);
        }
    }

    public void DispararPlayer()
    {
        
        var game = FindObjectOfType<GameManagerController>();
             if(_spriteRenderer.flipX == false){
               
                var shieldPosition = transform.position + new Vector3(1,0,0);
                var gb = Instantiate(bulletFire,
                                 shieldPosition,
                                 Quaternion.identity) as GameObject;
                var controller =gb.GetComponent<BulletController>();
                controller.SetRightDirection(); 
                game.perderBala(1);
                cantidadBalas++;
                ChangeAnimation(ANIMATION_ATTACK);
                
               
             }
             if(_spriteRenderer.flipX==true){
                
                
                var shieldPosition = transform.position + new Vector3(-1,0,0);
                var gb = Instantiate(bulletFire,
                                 shieldPosition,
                                 Quaternion.identity) as GameObject;
                var controller =gb.GetComponent<BulletController>();
                controller.SetLeftDirection(); 
                game.perderBala(1);
                cantidadBalas++;
                ChangeAnimation(ANIMATION_ATTACK);
                
                
             }
        
    }
    public void SaltarPlayer()
    {
        puedeSaltar = false;
        _rigidbody.AddForce(new Vector2(0, fuerzaSalto), ForceMode2D.Impulse);
        ChangeAnimation(ANIMATION_JUMP);
    }
    
    public void CaminarIzquierda()
    {
          _rigidbody.velocity = new Vector2(-velocidadPlayer, _rigidbody.velocity.y);
            
            
            
           
           _spriteRenderer.flipX = true;
        
        ChangeAnimation(ANIMATION_RUN);
    }
    
    public void CaminarDerecha()
    {
        _rigidbody.velocity = new Vector2(velocidadPlayer, _rigidbody.velocity.y);
           _spriteRenderer.flipX = false;
            
            
        
        ChangeAnimation(ANIMATION_RUN);
    }

    public void DetenerPlayer()
    {
        _rigidbody.velocity = new Vector2(0, _rigidbody.velocity.y);
        ChangeAnimation(ANIMATION_IDLE);
    }

    public void MorirPlayer()
    {
        ChangeAnimation(ANIMATION_DEAD);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            murio = murio + 1;
            _gameManager.PerderVida(1);
            Debug.Log(murio);
            
            if (murio == 3)
            {
                Debug.Log("Murio PLAYER");
                MorirPlayer();
                SceneManager.LoadScene(0);
            }
            
        }
        
        if (other.gameObject.tag == "BulletTeacher")
        {
            murio = murio + 1;
            _gameManager.PerderVida(1);
            Debug.Log(murio);
            
            if (murio == 3)
            {
                Debug.Log("Murio PLAYER");
                MorirPlayer();
                SceneManager.LoadScene(3);
            }
            
        }
        
        if (other.gameObject.tag == "DarkHole") 

        {
             _gameManager.PerderVida(1);
            if (lastCheckPointPosition != null)
            {
               
                transform.position = lastCheckPointPosition;
                
            }
            murio++;
            if(murio == 3)
            {
               SceneManager.LoadScene("Menu"); 
            }
        }
        
        // Solo Salta cuando colisiona el Piso
        if (other.gameObject.tag == "Piso")
        {
            puedeSaltar = true;
            Debug.Log("Puede Saltar");
        }
        
    }


     private void OnTriggerStay2D(Collider2D collision)
    {
        var game = FindObjectOfType<GameManagerController>();
        var tag = collision.gameObject.tag;
        if (tag == "GanaBala")
        {
            
            game.ganarBala(1);
            cantidadBalas--;
             Destroy(collision.gameObject);
        }
         
    }

     //CheckPoint

     private void OnTriggerEnter2D(Collider2D other)
     {
         Debug.Log("Touch CheckPoint");
         Debug.Log(lastCheckPointPosition);
         lastCheckPointPosition = transform.position;
         
         if (other.gameObject.tag == "CheckPointRed")
         {
             Debug.Log("TERMINO EL JUEGO");
         }
          if (other.gameObject.tag == "final")        
        {
           
           
           SceneManager.LoadScene("Scene 2");
            _gameManager.SaveGame();
           
           
        }
         if (other.gameObject.tag == "final2")        
        {
           
           
           SceneManager.LoadScene("Scene 3");
            _gameManager.SaveGame();
           
           
        }
         if (other.gameObject.tag == "final3")        
        {
           
           
           SceneManager.LoadScene("PreBoss");
            _gameManager.SaveGame();
           
           
        }
         if (other.gameObject.tag == "final4")        
        {
           
           
           SceneManager.LoadScene("aprobado");
            _gameManager.SaveGame();
           
           
        }
     }
    
     

     private void ChangeAnimation(int animation)
    {
        _animator.SetInteger("Estado", animation);
    }

    
}


