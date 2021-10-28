using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public float speed;
    private Transform target;
    private string PLAYER_TAG = "Player";
    public Animator animator;
    Vector2 movement;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        StartCoroutine(SelfDestruct());
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null) return;
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }
    IEnumerator LockControls()
    {
        while (true)
        {
            yield return new WaitForSeconds(3.0f);
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");

            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
            animator.SetFloat("Speed", movement.sqrMagnitude);

        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(PLAYER_TAG))
            Destroy(gameObject);
    }

    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
  
}


//private void OnCollisionEnter2D(Collision2D collision)
//{
//    if (collision.gameObject.CompareTag(PLAYER_TAG))
//        Destroy(gameObject);
//}

//IEnumerator SpawnFollowEnemy()
//{
//    while (true)
//    {
//        yield return new WaitForSeconds(respawntime);
//    }
//}

//You can use OnCollisionEnter or OnTriggerEnter (depending if you're using trigger or not) 
//then check if the other collider is the player
//then destroy if true