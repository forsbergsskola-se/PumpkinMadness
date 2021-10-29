using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private InputHandler _input;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private bool rotateTowardsMouse;
    [SerializeField] private Camera camera;
    [SerializeField] LayerMask collisionMask;
    [SerializeField] float distanceOffset = 0.5f;
    [SerializeField] private bool isGrounded = false;
    [SerializeField] private CapsuleCollider collider;
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private float jumpForce;

   
    

    

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _input = GetComponent<InputHandler>();
    }
    
    
    void FixedUpdate()
    {
        RaycastHit hit;
        float traceDistance = distanceOffset;
        Debug.Log(traceDistance);
        isGrounded = Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit,
            traceDistance, collisionMask);
        
        
        var targetVector = new Vector3(_input.InputVector.x, 0, _input.InputVector.y);
        
        //Move and rotate in direction we're aiming/travelling
        var movementVector = MoveTowardTarget(targetVector);
        if (!rotateTowardsMouse)
        {
            RotateTowardMovementVector(movementVector);
        }
        else
        {
            RotateTowardsMouseVector();
        }
        RotateTowardMovementVector(movementVector);

        if (Input.GetKeyDown("space"))
        {
            Jump();
        }

        if (_rb.position.y < -15)
        {
            FindObjectOfType<GameManager>().GameOver();
        }
        
       
    }
    

    private void Jump()
    {
        
        if (isGrounded)
        {
            _rb.AddForce(transform.up * jumpForce);
        }
    }

    private void RotateTowardsMouseVector()
    {
        Ray ray = camera.ScreenPointToRay(_input.Mouseposition);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, maxDistance: 300f))
        {
            var target = hitInfo.point;
            target.y = transform.position.y;
            transform.LookAt(target);
        }
    }

    private void RotateTowardMovementVector(Vector3 movementVector)
    {
        if (movementVector.magnitude == 0)
        {
            return;
        }
        var rotation = Quaternion.LookRotation(movementVector);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotateSpeed);
    }

    private Vector3 MoveTowardTarget(Vector3 targetVector)
    {
        var speed = moveSpeed * Time.deltaTime;
        targetVector = Quaternion.Euler(0, camera.gameObject.transform.eulerAngles.y, 0) * targetVector;
        var targetPosition = transform.position + targetVector * speed;
        transform.position = targetPosition;
        return targetVector;
    }
}
