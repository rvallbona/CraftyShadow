using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Unity.VisualScripting;
using UnityEngine;

public class KeyLaserLogic : MonoBehaviour
{
    private PlayerController playerController;
    private void Start()
    {
        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerController.SetKey(true);
            Destroy(this.gameObject);
        }
    }
}
