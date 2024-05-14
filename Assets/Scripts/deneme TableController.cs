using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class denemeTableController : MonoBehaviour
{
    /*
    -1. �arpan playersa gold setactive(false)
    -2. aktif olmayan alt�nlar sonra geri d�n, reload
    3. gold setactive false ise toplanabilirli�i kapat
    4.player�n elindekileri a�
     
     */
    public GameObject gold;
    
    private void OnCollisionEnter(Collision collision)
    {
        if (!gold.activeSelf) return;
        if (collision.gameObject.tag != "Player") return;
        
        var PlayerController = collision.gameObject.GetComponent<PlayerController>();

        if (PlayerController.CollectGold())
        {
            gold.SetActive(false);
            Invoke(nameof(ReloadGold), Random.Range(5f, 15f));
        }


    }

    private void ReloadGold() {  gold.SetActive(true); }
}
