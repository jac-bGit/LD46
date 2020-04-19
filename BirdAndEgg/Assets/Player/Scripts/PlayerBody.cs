using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBody : MonoBehaviour
{
    [Range(0,25)]
    [SerializeField] private float _gravity;
    [Range(0,1)]
    [SerializeField] private float _gravityTimeDelay;
    public bool IsGrounded { get; set;}
    private float startY;

    //components
    private Rigidbody2D _rb;
    private ConstantForce2D _constantForce;

    [SerializeField] private Transform _platform;
    [SerializeField] private PlayerBehaviour _playerBehaviour;

    // Start is called before the first frame update
    void Start()
    {
        //get components
        _rb = GetComponent<Rigidbody2D>();
        _constantForce = GetComponent<ConstantForce2D>();

        //get references
        _playerBehaviour = GetComponentInParent<PlayerBehaviour>();

        //setup
        IsGrounded = true;
        startY = transform.position.y;
        _constantForce.force = new Vector2(0,0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(_platform.position.x, transform.position.y);

        //stop jump
        if(startY > transform.position.y){
            _constantForce.force = new Vector2(0,0);
            _rb.velocity = new Vector2(_rb.velocity.x,0);
            transform.position = new Vector2(transform.position.x, startY);
            IsGrounded = true;
        }
    }

    void OnTriggerEnter2D(Collider2D col){
        if(col.gameObject.tag == "Obstacle"){
            //check speed
            _playerBehaviour.SpeedHitCheck();
        }
    }

    public void Jump(float jumpForce){
        _rb.AddForce(new Vector2(0, jumpForce));
        StartCoroutine(activeGravity(_gravityTimeDelay));
        IsGrounded = false;
    }

    IEnumerator activeGravity(float time){
        yield return new WaitForSeconds(time);
        _constantForce.force = new Vector2(0,-_gravity);
    }
}
