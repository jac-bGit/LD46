﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavour : MonoBehaviour
{
    [SerializeField] private Transform _target;
    private Vector3 _offset;

    // Start is called before the first frame update
    void Start()
    {
        _offset = transform.position - _target.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = _target.position + _offset;
    }
}
