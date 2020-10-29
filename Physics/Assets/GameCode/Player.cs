using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    [SerializeField] private LayerMask _mask;
    [SerializeField] private float _force = 10.0f;
    [SerializeField] private Transform _handsPos;

    private Vector2 _diraction;
    private Rigidbody2D _grabedObject;
    private bool _isGrabed;
    void Awake()
    {
        _diraction = new Vector2(0.0f, 0.0f);
    }

    void Update()
    {
        var h = CrossPlatformInputManager.GetAxis("Horizontal");
        
        if (h > 0)
            _diraction.x = 1.0f;
        else if (h < 0)
            _diraction.x = -1.0f;

        if (_grabedObject)
            _grabedObject.transform.position = _handsPos.position;
        
        if (Input.GetKeyDown(KeyCode.F))
        {
            _isGrabed = !_isGrabed;
        }
    }

    private void FixedUpdate()
    {
        if (_isGrabed)
        {
            if (CheckCubeInHands())
            {
                _isGrabed = false;
                return;
            }
            GrabCube();
            _isGrabed = false;
        }
    }

    bool CheckCubeInHands()
    {
        if(_grabedObject != null)
        {
            var objJoin = _grabedObject.GetComponent<FixedJoint2D>();
            objJoin.connectedBody = null;
            objJoin.enabled = false;
            _grabedObject.AddForce(_diraction * _force, ForceMode2D.Impulse);
            _grabedObject = null;
            return true;
        }
        return false;
    }

    void GrabCube()
    {
        var hit = Physics2D.Raycast(transform.position, _diraction, 1.0f, _mask);
        if (hit.collider)
        {
            var rigObject = hit.collider.GetComponent<Rigidbody2D>();
            var objJoin = rigObject.GetComponent<FixedJoint2D>();
                
            if (_grabedObject != rigObject)
            {
                _grabedObject = rigObject;
                objJoin.connectedBody = GetComponent<Rigidbody2D>();
                objJoin.enabled = true;
            }
        }
    }
}
