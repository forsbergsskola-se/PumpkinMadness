using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
   public Vector2 InputVector { get; private set; }
   
   public Vector3 Mouseposition { get; private set; }
    void Update()
    {
        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");
        InputVector = new Vector2(h, v);
        
        Mouseposition = Input.mousePosition;
    }
}
