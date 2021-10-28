using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewDestroyPumpkin : MonoBehaviour
{

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(this.gameObject, 2f);
        }
    }

}
