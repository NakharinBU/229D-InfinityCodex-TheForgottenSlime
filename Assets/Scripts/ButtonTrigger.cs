using UnityEngine;
using UnityEngine.UI;

public class ButtonTrigger : MonoBehaviour
{
    public GameObject buttonUI;
    public GameObject targetObject;
    private bool isPlayerNearby = false;
    private bool isPressed = false;

    void Start()
    {
        buttonUI.SetActive(false);
    }

    void Update()
    {
        if (isPlayerNearby && !isPressed && Input.GetKeyDown(KeyCode.E))
        {
            PressButton();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isPressed && (other.CompareTag("Player") || other.CompareTag("SolidSlime")))
        {
            buttonUI.SetActive(true);
            isPlayerNearby = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("SolidSlime"))
        {
            buttonUI.SetActive(false);
            isPlayerNearby = false;
        }
    }

    void PressButton()
    {
        Debug.Log("Button Pressed!");
        isPressed = true;
        buttonUI.SetActive(false);
        if (targetObject != null)
        {
            targetObject.SetActive(!targetObject.activeSelf);
        }
    }
}
