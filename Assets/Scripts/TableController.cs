using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class TableController : MonoBehaviour
{
    public GameObject goldObject;
    
    public bool IsGoldCollectable => goldObject.activeSelf;
    
    private void OnCollisionEnter(Collision other)
    {
        if (!IsGoldCollectable) return;
        if (other.gameObject.tag != "Player") return;

        var player = other.gameObject.GetComponent<PlayerController>();

        if (player.CollectGold())
        {
            goldObject.SetActive(false);

        }


    }

}
