using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;


public class Interaction : MonoBehaviour
{
    [Header("UI elements")]
    [SerializeField] private Image fillCircle; // Drag your TextMeshPro object here
    [SerializeField] private CanvasGroup interactionCanvas; // Drag the CanvasGroup of your interaction UI here
    
    [Header("Settings")]
    [SerializeField] private float interactionTime = 2f; // Time required to complete interaction
    [SerializeField] private float rayDistance = 3f; // Max distance for interaction raycast
    
    private float textFadeSpeed = 5f; // Speed at which text fades in/out
    private float timer = 0f;
    private bool isInteracting = false;
    private Canvas currentObjectCanvas;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        interactionCanvas.alpha = 0f; // Hide interaction UI at start
    }

    // Update is called once per frame
    void Update()
    {
        HandleInteraction();
    }

    void HandleInteraction()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, rayDistance))
        {
            if (hit.collider.CompareTag("Interactable"))
            {
                Canvas objCanvas = hit.collider.GetComponentInChildren<Canvas>();
                if (objCanvas != null)
                {
                    currentObjectCanvas = objCanvas;

                    CanvasGroup group = currentObjectCanvas.GetComponent<CanvasGroup>();
                    if (group) group.alpha = Mathf.Lerp(group.alpha, 1, Time.deltaTime * textFadeSpeed);
                    currentObjectCanvas.transform.LookAt(transform.position);
                    currentObjectCanvas.transform.Rotate(0, 0, 0);
                }

                if (Input.GetKey(KeyCode.E))
                {
                    StartInteraction();
                    return;
                }
            }
        }
        else
        {
            if (currentObjectCanvas != null)
            {
                CanvasGroup group = currentObjectCanvas.GetComponent<CanvasGroup>();
                if (group) group.alpha = Mathf.Lerp(group.alpha, 0, Time.deltaTime * textFadeSpeed);
            }
        }

        ResetInteracting();
    }

    void StartInteraction()
    {
        isInteracting = true;
        interactionCanvas.alpha = 1f; // Show interaction UI
        timer += Time.deltaTime;
        fillCircle.fillAmount = timer / interactionTime;
        
        if (timer >= interactionTime)
        {
            OnInteractionComplete();
            ResetInteracting();
        }
    }

    void ResetInteracting()
    {
        isInteracting = false;
        timer = 0f;
        fillCircle.fillAmount = 0f;
        interactionCanvas.alpha = Mathf.Lerp(interactionCanvas.alpha, 0, Time.deltaTime * 10f);
    }
    
    void OnInteractionComplete()
    {
        // Implement what happens when interaction is complete (e.g., open door, pick up item)
        Debug.Log("Interaction Complete!");
    }
}
