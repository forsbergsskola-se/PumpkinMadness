using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyPlayerScript : MonoBehaviour
{
    public float speed;
    public float rotationSpeed;
    public float jumpSpeed;
    public float jumpButtonGracePeriod;

    private Animator animator;
    private CharacterController characterController;
    private float ySpeed;
    private float originalStepOffset;
    private float? lastGroundedTime;
    private float? jumpButtonPressedTime;


        // Start is called before the first frame update
        void Start()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        originalStepOffset = characterController.stepOffset;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movementDirection = new Vector3(0, horizontalInput, verticalInput);
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
}




//{
//    public Animator animator; //NYTT

//    private InputHandler _input;

//    public float moveSpeed;
//    public float rotateSpeed;
//    public bool rotateTowardsMouse;
//    public Camera camera;
//    public float jumpSpeed;

//    Rigidbody _rb;
//    bool _canJump;


//    private void OnCollisionEnter(Collision other)
//    {
//        if (other.gameObject.CompareTag("Floor"))
//        {
//            _canJump = true;
//        }
//    }
//    private void OnCollisionExit(Collision other)
//    {
//        if (other.gameObject.CompareTag("Floor"))
//        {
//            _canJump = false;
//        }
//    }


//    private void Awake()
//    {
//        _input = GetComponent<InputHandler>();
//        _rb = GetComponent<Rigidbody>();
//    }

//    private void Start()
//    {
//        animator = GetComponent<Animator>();
//    }

//    void Update()
//    {
//        var targetVector = new Vector3(_input.InputVector.x, 0, _input.InputVector.y);

//        //Move and rotate in direction we're aiming/travelling
//        var movementVector = MoveTowardTarget(targetVector);
//        if (!rotateTowardsMouse)
//        {
//            animator.SetBool("isWalking", true); //NYTT
//            RotateTowardMovementVector(movementVector);
//        }
//        else
//        {
//            animator.SetBool("isWalking", false); //NYTT
//            RotateTowardsMouseVector();
//        }
//        RotateTowardMovementVector(movementVector);

//        if (Input.GetKeyDown(KeyCode.Space) && _canJump)
//        {
//            Jump();
//        }
//    }

//    private void Jump()
//    {
//        _rb.AddForce(0f, jumpSpeed * Time.deltaTime, 0f);
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
