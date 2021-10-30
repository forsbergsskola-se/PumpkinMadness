using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
//{
//    private InputHandler _input;

//    [SerializeField] private float moveSpeed;
//    [SerializeField] private float rotateSpeed;
//    [SerializeField] private bool rotateTowardsMouse;
//    [SerializeField] private Camera camera;
//    [SerializeField] LayerMask collisionMask;
//    [SerializeField] float distanceOffset = 0.5f;
//    [SerializeField] private bool isGrounded = false;
//    [SerializeField] private CapsuleCollider collider;
//    [SerializeField] private Rigidbody _rb;
//    [SerializeField] private float jumpForce;
//    [SerializeField] private AudioClip _playerMoveSound;




//    private void Awake()
//    {
//        _rb = GetComponent<Rigidbody>();
//        _input = GetComponent<InputHandler>();
//    }


//    void FixedUpdate()
//    {
//        RaycastHit hit;
//        float traceDistance = distanceOffset;
//        Debug.Log(traceDistance);
//        isGrounded = Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit,
//            traceDistance, collisionMask);


//        var targetVector = new Vector3(_input.InputVector.x, 0, _input.InputVector.y);

//        //Move and rotate in direction we're aiming/travelling
//        var movementVector = MoveTowardTarget(targetVector);
//        if (!rotateTowardsMouse)
//        {
//            RotateTowardMovementVector(movementVector);
//            GameObject Audio = GameObject.FindGameObjectWithTag("Audio");
//            Audio.GetComponent<AudioManager>().PlayAudio(_playerMoveSound, false);


//        }
//        else
//        {
//            RotateTowardsMouseVector();

//        }
//        RotateTowardMovementVector(movementVector);

//        if (Input.GetKeyDown("space"))
//        {
//            Jump();
//        }

//        if (_rb.position.y < -2)
//        {
//            FindObjectOfType<GameManager>().GameOver();
//        }


//    }


//    private void Jump()
//    {

//        if (isGrounded)
//        {
//            _rb.AddForce(transform.up * jumpForce);
//        }
//    }

//    private void RotateTowardsMouseVector()
//    {
//        Ray ray = camera.ScreenPointToRay(_input.Mouseposition);

//        if (Physics.Raycast(ray, out RaycastHit hitInfo, maxDistance: 300f))
//        {
//            var target = hitInfo.point;
//            target.y = transform.position.y;
//            transform.LookAt(target);
//        }
//    }

//    private void RotateTowardMovementVector(Vector3 movementVector)
//    {
//        if (movementVector.magnitude == 0)
//        {
//            return;
//        }
//        var rotation = Quaternion.LookRotation(movementVector);
//        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotateSpeed);
//    }

//    private Vector3 MoveTowardTarget(Vector3 targetVector)
//    {
//        var speed = moveSpeed * Time.deltaTime;
//        targetVector = Quaternion.Euler(0, camera.gameObject.transform.eulerAngles.y, 0) * targetVector;
//        var targetPosition = transform.position + targetVector * speed;
//        transform.position = targetPosition;
//        return targetVector;
//    }
//}
{
    public float speed;
    public float rotationSpeed;
    public float jumpSpeed;
    public float jumpButtonGracePeriod;

    
    [SerializeField] private Animator animator;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private float ySpeed;
    [SerializeField] private float originalStepOffset;
    [SerializeField] private float? lastGroundedTime;
    [SerializeField] private float? jumpButtonPressedTime;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        originalStepOffset = characterController.stepOffset;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis("Vertical");
        float verticalInput = Input.GetAxis("Horizontal");

        Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);
        float magnitude = Mathf.Clamp01(movementDirection.magnitude) * speed;
        movementDirection.Normalize();

        ySpeed += Physics.gravity.y * Time.deltaTime;

        if (characterController.isGrounded)
        {
            lastGroundedTime = Time.time;
        }

        if (Input.GetButtonDown("Jump"))
        {
            jumpButtonPressedTime = Time.time;
        }

        if (Time.time - lastGroundedTime <= jumpButtonGracePeriod)
        {
            characterController.stepOffset = originalStepOffset;
            ySpeed = -0.5f;

            if (Time.time - jumpButtonPressedTime <= jumpButtonGracePeriod)
            {
                ySpeed = jumpSpeed;
                jumpButtonPressedTime = null;
                lastGroundedTime = null;
            }
        }
        else
        {
            characterController.stepOffset = 0;
        }

        Vector3 velocity = movementDirection * magnitude;
        velocity.y = ySpeed;

        characterController.Move(velocity * Time.deltaTime);

        if (movementDirection != Vector3.zero)
        {
            animator.SetBool("isWalking", true);
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
       
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Enemy"))
        {
            FindObjectOfType<GameManager>().GameOver();
        }
    }
}