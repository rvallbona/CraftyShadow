using UnityEditor.Presets;
using UnityEditor.VersionControl;
using UnityEngine;
public class LightController : MonoBehaviour
{
    private PlayerController playerController;
    private Light actualLight;
    [HideInInspector] public bool isActive;
    private Material outlineObject;

    //private float timer, defaultLight;
    //private bool interacted, canReset;
    [SerializeField] private GameObject[] lightObjectConnected;
    private void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        actualLight = this.gameObject.GetComponent<Light>();
        CheckStatusLight();

        outlineObject = this.gameObject.GetComponent<MeshRenderer>().materials[1];
        outlineObject.SetFloat("_Scale", 1);

        //timer = 0;
        //interacted = false;
        //defaultLight = this.gameObject.GetComponent<Light>().intensity;
    }
    private void Update()
    {
        CheckStatusLight();

        Debug.Log("energy: " + playerController.GetEnergy());
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
        //timer += Time.deltaTime;
        if (actualLight.intensity == 0)//Off
        {
            isActive = false;
        }
        else if (actualLight.intensity == 1)//On
        {
            isActive = true;
        }
        //ResetLight();
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

            //timer = 0;
            //interacted = true;

            playerController.SetInteractLight(false);
            playerController.SetEnergy(true);
        }
    }
    //private void ResetLight()
    //{
    //    if (timer >= 3 && interacted && playerController.GetEnergy())
    //    {
    //        Debug.Log("Reiniciando...");
    //        actualLight.intensity = defaultLight;
    //        playerController.SetInteractLight(false);
    //        playerController.SetEnergy(false);
    //        timer = 0;
    //        interacted = false;
    //    }
    //}
}
