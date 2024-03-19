using UnityEngine;
public class LightController : MonoBehaviour
{
    private PlayerController playerController;
    private Light actualLight;
    [HideInInspector] public bool isActive;
    private Material outlineObject;
    private float timer;
    private void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        actualLight = this.gameObject.GetComponent<Light>();
        CheckStatusLight();

        outlineObject = this.gameObject.GetComponent<MeshRenderer>().materials[1];
        outlineObject.SetFloat("_Scale", 1);
    }
    private void Update()
    {
        CheckStatusLight();

        //Debug.Log(this.gameObject.name + "-> isActive: " + this.gameObject.GetComponent<LightController>().isActive);
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerController.SetCanInteractLight(true);
            LightIntensityController();

            outlineObject.SetFloat("_Scale", 1.1f);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerController.SetCanInteractLight(false);
            playerController.SetInteractLight(false);

            outlineObject.SetFloat("_Scale", 1);
        }
    }
    private void CheckStatusLight()
    {
        timer += Time.deltaTime;
        Debug.Log(timer);
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
        //Off to On
        if (actualLight.intensity == 0 && playerController.GetInteractingLight() && playerController.GetEnergy())
        {
            actualLight.intensity = 1;
            playerController.SetInteractLight(false);
            playerController.SetEnergy(false);
        }
        //On to Off
        else if (actualLight.intensity == 1 && playerController.GetInteractingLight() && !playerController.GetEnergy())
        {
            actualLight.intensity = 0;
            timer = 0;
            playerController.SetInteractLight(false);
            playerController.SetEnergy(true);
        }
    }
}
