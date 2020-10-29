﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SPB {

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D playerRb;
    public GameObject wandPivotObject;
    public float impulseForce = 5;
    public Vector2 mouseDifference;
    public float xInput;
    public float rotationSpeed;
    public GameObject squarePivotObject;
    public float squarePivotRotation;
    public float mouseRotationZ;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            playerRb.AddForce(mouseDifference * impulseForce, ForceMode2D.Impulse);
        }
    }

    private void FixedUpdate()
    {
        mouseDifference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        mouseDifference.Normalize();

        mouseRotationZ = Mathf.Atan2(mouseDifference.y, mouseDifference.x) * Mathf.Rad2Deg;
        xInput = Input.GetAxis("Horizontal");
        squarePivotObject.transform.Rotate(Vector3.forward, xInput * rotationSpeed);

        RotateOnPivot(mouseDifference, wandPivotObject);

    }

    public void RotateOnPivot(Vector2 rotateVector, GameObject pivotObjectName)
    {
        float rotationZ = Mathf.Atan2(rotateVector.y, rotateVector.x) * Mathf.Rad2Deg;
        pivotObjectName.transform.rotation = Quaternion.Euler(0f, 0f, rotationZ);

    }
}
}






