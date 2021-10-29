using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    private Animator animator;

    public Transform target;
    public float speed = 4f;
    Rigidbody rig;
    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody>();        
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        //Make enemy move towards player
        Vector3 pos = Vector3.MoveTowards(transform.position, target.position, speed * Time.fixedDeltaTime);
        rig.MovePosition(pos);
        transform.LookAt(target);

        //animator.SetBool("isWalking", true);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(GameObject.Find("Player"));
            FindObjectOfType<GameManager>().GameOver();
        }

        //animator.SetBool("isWalking", false);
    }
}
