using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickScripts : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Brick")
        {
            other.gameObject.tag = "Untagged";
            CharacterMovement.Instance.PicKGold(other.gameObject);
            other.gameObject.AddComponent<Rigidbody>();
            other.gameObject.GetComponent<Rigidbody>().useGravity = false;
            other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            other.gameObject.AddComponent<BrickScripts>();
            other.gameObject.AddComponent<BuildBrick>();
            Destroy(this);
        }
    }
}
