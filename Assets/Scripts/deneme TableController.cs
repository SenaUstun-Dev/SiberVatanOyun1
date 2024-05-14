using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class denemeTableController : MonoBehaviour
{
    /*
    -1. çarpan playersa gold setactive(false)
    -2. aktif olmayan altýnlar sonra geri dön, reload
    3. gold setactive false ise toplanabilirliði kapat
    4.playerýn elindekileri aç
     
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
