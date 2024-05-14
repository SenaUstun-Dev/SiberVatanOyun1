using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndWallController : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("car"))
        {
            Destroy(other.gameObject);
        }
    }
}
