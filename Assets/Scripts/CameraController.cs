using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
public GameObject player;
    void Start()
    {
        
    }
    void Update()
    {
        seguirConCamara();
    }

    private void seguirConCamara()
    {
        var x = player.transform.position.x;
        var y = player.transform.position.y;

        transform.position = new Vector3(x, y, transform.position.z);
    }
}
