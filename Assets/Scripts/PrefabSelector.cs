using UnityEngine;
using UnityEngine.UI; // For UI components
using UnityEngine.XR.ARFoundation; // For AR Foundation components
using UnityEngine.XR.ARSubsystems; // For ARSubsystems if needed
using UnityEngine.XR.Interaction.Toolkit.AR; // For AR Interaction Toolkit (if needed)
using UnityEngine.XR.Interaction.Toolkit;
public class PrefabSelector : MonoBehaviour
{
    [Header("References")]
    public ARPlacementInteractable placementInteractable; // Reference to your ARPlacementInteractable
    public Button[] prefabButtons; // Array of buttons representing prefab images
    public GameObject[] prefabs; // Array of prefabs corresponding to the buttons
    public Camera arCamera; // Camera to cast the ray from (typically the AR camera)
    public string planeTag = "ARPlane"; // Tag to identify the AR plane

    public Button deleteButton;
    private GameObject spawnedObject;
    void Start()
    {
        // Initialize buttons with listeners
        for (int i = 0; i < prefabButtons.Length; i++)
        {
            int index = i; // Capture the correct index for each button
            prefabButtons[i].onClick.AddListener(() => OnPrefabButtonClick(index));
        }

        deleteButton.onClick.AddListener(DeleteSpawnedObject);
    }

    void Update()
    {
        // Detect touch or click event
        if (Input.touchCount > 0)
        {
            // Get the touch position
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                // Cast a ray from the AR camera to detect what was touched
                Ray ray = arCamera.ScreenPointToRay(touch.position);
                RaycastHit hit;

                // Check if the ray hits any object
                if (Physics.Raycast(ray, out hit))
                {
                    // If an object is hit, set it as the selected object
                    // if (hit.collider.gameObject.CompareTag(planeTag))
                    // {
                    //     Debug.Log("Cannot select Plane!");
                    //     return;
                    // }
                    spawnedObject = hit.collider.gameObject;
                    Debug.Log("Selected object: " + spawnedObject.name); // For debugging
                }
            }
        }
    }
    // Called when a prefab button is clicked
    private void OnPrefabButtonClick(int index)
    {
        // Check if the index is valid
        if (index >= 0 && index < prefabs.Length)
        {
            // Set the prefab in the ARPlacementInteractable component
            // spawnedObject = Instantiate(prefabs[index]);
            placementInteractable.placementPrefab = prefabs[index];
            // Debug.Log("Spawned object: " + spawnedObject.name); // Debug log to check if object is instantiated

            // spawnedObject = placementInteractable.placementPrefab;
        }
    }
    private void DeleteSpawnedObject()
    {
        if (spawnedObject != null)
        {
            // Check if the selected object is still valid (i.e., not destroyed)
            if (spawnedObject.GetComponent<ARPlane>() != null)
            {
                Debug.Log("Cannot delete the AR plane or placement indicator.");
                return; // Do nothing if it's the plane
            }
            Debug.Log("Deleting spawned object: " + spawnedObject.name); // Debug log
            Destroy(spawnedObject);
            spawnedObject = null;
        }
        else
        {
            Debug.Log("No object to delete.");

        }
    }
}
