using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGround : Obstacle
{
    //components
    private Collider2D _collider;

    // Start is called before the first frame update
    void Start()
    {
        DefaultStart();
        //get components
        _collider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // _collider.isTrigger = !isActive;               
    }

    void OnTriggerEnter2D(Collider2D col){
        if(col.gameObject.tag == "Player"){
            isActive = false;
        }
    }
}
