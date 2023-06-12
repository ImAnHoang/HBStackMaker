using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckWin : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // if (other.tag == "WinPos")
        // {
        //     // Debug.Log("Win");
        //     CharacterMovement.Instance.rb.velocity = Vector3.zero;
        //     CharacterMovement.Instance.transform.position = new Vector3(5.5f, 3f, 49.5f);
        //     // // InputHandler.Instance.enabled = false;
        //     CharacterMovement.Instance.BrickParent.transform.position = new Vector3(5.5f, 3f, 49.5f);
        // }
    }
}
