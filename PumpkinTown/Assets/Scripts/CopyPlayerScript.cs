using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyPlayerScript : MonoBehaviour
{
    public Animator animator;

    private InputHandler _input;

    public float moveSpeed;
    public float rotateSpeed;
    public bool rotateTowardsMouse;
    public Camera camera;
    public float jumpSpeed;

    Rigidbody _rb;
    bool _canJump;


    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Floor"))
        {
            _canJump = true;
        }
    }
    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Floor"))
        {
            _canJump = false;
        }
    }


    private void Awake()
    {
        _input = GetComponent<InputHandler>();
        _rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        var targetVector = new Vector3(_input.InputVector.x, 0, _input.InputVector.y);

        //Move and rotate in direction we're aiming/travelling
        var movementVector = MoveTowardTarget(targetVector);
        if (!rotateTowardsMouse)
        {
            animator.SetBool("isWalking", true);
            RotateTowardMovementVector(movementVector);
        }
        else
        {
            animator.SetBool("isWalking", false);
            RotateTowardsMouseVector();
        }
        RotateTowardMovementVector(movementVector);

        if (Input.GetKeyDown(KeyCode.Space) && _canJump)
        {
            Jump();
        }
    }

    private void Jump()
    {
        _rb.AddForce(0f, jumpSpeed * Time.deltaTime, 0f);
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
