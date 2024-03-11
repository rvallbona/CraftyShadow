using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    private PlayerController playerController;
    private Light actualLight;
    public bool isActive;
    private void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        actualLight = this.gameObject.GetComponent<Light>();
        CheckStatusLight();
    }
    private void Update()
    {
        CheckStatusLight();

    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerController.SetCanInteractLight(true);
            LightIntensityController();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerController.SetCanInteractLight(false);
            playerController.SetInteractLight(false);
        }
    }
    private void CheckStatusLight()
    {
        if (actualLight.intensity == 0)
        {
            isActive = false;
        }
        else if (actualLight.intensity == 1)
        {
            isActive = true;
        }
    }
    private void LightIntensityController()
    {
        if (actualLight.intensity == 0 && playerController.GetInteractingLight())
        {
            actualLight.intensity = 1;
            playerController.SetInteractLight(false);
        }
        else if (actualLight.intensity == 1 && playerController.GetInteractingLight())
        {
            actualLight.intensity = 0;
            playerController.SetInteractLight(false);
        }
    }
}
