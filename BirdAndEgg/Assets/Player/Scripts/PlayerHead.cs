using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHead : MonoBehaviour
{
    [SerializeField] private float _moveLimit;
    [SerializeField] private int _headSpeed;
    [SerializeField] private Transform _holder;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Moving();
        SimpleMoving();
    }

    void Moving(){
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 holderPos = _holder.transform.position;
        Vector2 finalPos = mousePos - holderPos;

        if(finalPos.magnitude > _moveLimit){
            float angle = Vector2.Angle(_holder.transform.right, finalPos);
            // if(mousePos.y < _holder.transform.position.y)
            //     angle = 360 - Vector2.Angle(_holder.transform.right, finalPos);
                
            angle *= Mathf.Deg2Rad;
            float x = Mathf.Cos(angle);
            float y = Mathf.Sin(angle);
            if(mousePos.y < _holder.transform.position.y)
                y = 0;

            finalPos = new Vector2(x,y) * _moveLimit + new Vector2(holderPos.x, holderPos.y);
        }
        else{
            if(mousePos.y < _holder.transform.position.y)
                finalPos = new Vector2(finalPos.x, 0);
            finalPos += new Vector2(holderPos.x, holderPos.y);
        }
        //move to mouse
        //transform.position = finalPos;
        transform.position = Vector3.MoveTowards(transform.position, finalPos, _headSpeed * Time.deltaTime);
    }

    void SimpleMoving(){
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float mousePosY = mousePos.y - _holder.transform.position.y;
        if(mousePosY > _moveLimit)
            mousePosY = _moveLimit;
        Vector3 finalPos = new Vector3(0, mousePosY) + _holder.transform.position;


        if(mousePos.y < _holder.transform.position.y)
            finalPos = new Vector3(0, 0) + _holder.transform.position;

        transform.position = Vector3.MoveTowards(transform.position, finalPos, _headSpeed * Time.deltaTime);
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(_holder.transform.position, _moveLimit);
    }
}
