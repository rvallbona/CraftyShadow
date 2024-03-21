using TMPro;
using UnityEngine;
public class Tutorial_0 : MonoBehaviour
{
    private TextMeshProUGUI text;
    [SerializeField] private string textContent;
    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        text.text = textContent;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.Joystick2Button1))
            this.gameObject.SetActive(false);
    }
}
