using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    //movement
    [SerializeField] private int _maxSpeed;
    public int speed;
    [SerializeField] private float _acceleration, _decceleration;
    [SerializeField] private float _legCycleTime;
    [SerializeField] private bool _isRunning;
    [SerializeField] private bool _lockedLegInput;
    [SerializeField] private LegInput _legInput;
    private bool _immidiateStop;
    [SerializeField] private float _stopTimeMax;
    private float _stopTime, _slowDownTime;
    public enum LegInput { 
        None,
        Left,
        Right
    }

    //legs
    [SerializeField] private GameObject _legLeftGO, _legRightGO;
    
    //components
    private Rigidbody2D _rb;


    // Start is called before the first frame update
    void Start()
    {
        //get components
        _rb = GetComponent<Rigidbody2D>();

        //setup
        _immidiateStop = false;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    private void Movement(){

        if(!_lockedLegInput){
            //Inputs
            if(Input.GetKeyDown(KeyCode.A) && _legInput != LegInput.Left){
                _legInput = LegInput.Left;
                _isRunning = true;
            }

            if(Input.GetKeyDown(KeyCode.S)  && _legInput != LegInput.Right){
                _legInput = LegInput.Right;
                _isRunning = true;
            }

            //waiting too long for input
            if(!_immidiateStop){
                if(_stopTime > _stopTimeMax){
                    _stopTime -= Time.deltaTime;
                }
                else
                    _immidiateStop = true;
            }
                
        }
        else{
            //wrong press time stops moving
            if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S))
                _immidiateStop = true;;
        }

        //moving
        if(_isRunning){
            //leg cycles
            if(!_lockedLegInput){
                switch(_legInput){
                    case LegInput.Left:
                    StartCoroutine(LegCycle(_legCycleTime, _legLeftGO));
                    break;
                    case LegInput.Right:
                    StartCoroutine(LegCycle(_legCycleTime, _legRightGO));
                    break;
                }
            }
        }

        //slowing
        if(!_immidiateStop)
            SlowDown(1);
        else
            SlowDown(10);
    }

    private IEnumerator LegCycle(float time, GameObject lefGO){
        _lockedLegInput = true;
        _immidiateStop = false;
        _stopTime = _stopTimeMax;
        _slowDownTime = _legCycleTime / 2;
        _rb.AddForce(new Vector2(_acceleration,0));
        Debug.Log("run");
        yield return new WaitForSeconds(time);
        _lockedLegInput = false;
        _isRunning = false;
        Debug.Log("stop");
    }

    private void SlowDown(float mod){
        if(_rb.velocity.x > 0){
            if(_slowDownTime > _legCycleTime / 2){
                _slowDownTime -= Time.deltaTime;
            }
            else{
                float finalSpeed = _rb.velocity.x - _decceleration * mod;
                _rb.velocity = new Vector2(finalSpeed, 0);
            }
        }
        if(_rb.velocity.x < 0)
            _rb.velocity = new Vector2(0, 0);
    }


}
