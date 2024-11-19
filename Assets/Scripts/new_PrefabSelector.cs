using UnityEngine;
using UnityEngine.UI; // For UI components
using UnityEngine.XR.ARFoundation; // For AR Foundation components
using UnityEngine.XR.ARSubsystems; // For ARSubsystems if needed
using UnityEngine.XR.Interaction.Toolkit.AR; // For AR Interaction Toolkit (if needed)
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections.Generic;


public class New_PrefabSelector : MonoBehaviour
{
    [Header("References")]
    public ARPlacementInteractable placementInteractable;
    public Button[] prefabButtons;
    public GameObject[] prefabs;
    public Button deleteButton;

    private GameObject spawnedObject;

    void Start()
    {
        // Initialize buttons with listeners
        for (int i = 0; i < prefabButtons.Length; i++)
        {
            int index = i;
            prefabButtons[i].onClick.AddListener(() => OnPrefabButtonClick(index));
        }

        deleteButton.onClick.AddListener(DeleteSpawnedObject);
    }

    private void OnPrefabButtonClick(int index)
    {
        if (index >= 0 && index < prefabs.Length)
        {
            // Destroy any previously spawned object
            if (spawnedObject != null)
            {
                Destroy(spawnedObject);
            }

            // Spawn the new prefab
            spawnedObject = Instantiate(prefabs[index]);
            placementInteractable.placementPrefab = spawnedObject;
        }
    }

    private void DeleteSpawnedObject()
    {
        if (spawnedObject != null)
        {
            Destroy(spawnedObject);
            spawnedObject = null;
        }
    }
}