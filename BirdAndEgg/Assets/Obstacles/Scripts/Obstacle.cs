using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public bool isActive;

    // Start is called before the first frame update
    protected void DefaultStart()
    {
        isActive = true;
    }

    // Update is called once per frame
    public virtual void Behaviour()
    {
        
    }
}
