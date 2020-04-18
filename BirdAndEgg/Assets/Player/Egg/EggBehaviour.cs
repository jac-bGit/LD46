using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggBehaviour : MonoBehaviour
{
    [SerializeField] private Transform _head;
    [SerializeField] private float _maxSnapDistance, _snapSpeed;

    //compoenents
    private Rigidbody2D _rb;

    // Start is called before the first frame update
    void Start()
    {
        //get components
        _rb = GetComponent<Rigidbody2D>();

        transform.parent = _head;
    }

    // Update is called once per frame
    void Update()
    {
        //stick to head
        // if(Vector2.Distance(transform.position, _head.position) < _maxSnapDistance){
        //     Vector3 hPos = new Vector3(_head.position.x, transform.position.y, 0);
        //     transform.position = Vector3.MoveTowards(transform.position, hPos, _snapSpeed * Time.deltaTime);
        // }
    }

    public void Fall(){
        transform.parent = null;
        _rb.bodyType = RigidbodyType2D.Dynamic;
        _rb.AddForce(new Vector2(1000, 0));
        Debug.Log("fall!");
    }
}
