using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialesController : MonoBehaviour
{
    private GameManagerController _gameManager;
    
    // Start is called before the first frame update
    void Start()
    {
        _gameManager = FindObjectOfType<GameManagerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
            Debug.Log("+1 Material");
            _gameManager.ganarMaterial(1);
        }
    }
}
