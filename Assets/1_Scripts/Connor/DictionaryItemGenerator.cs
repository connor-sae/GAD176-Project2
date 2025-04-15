using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DictionaryItemGenerator : MonoBehaviour
{
    [SerializeField]
    private Marker[] generationMarkers;

    /// <summary>
    /// Generates Items at the points specified by the markers using the RoomGroup structured pairs
    /// </summary>
    /// <param name="generationDict">the dictionary containing what can be generated</param>
    /// <returns>the GameObjects Generated</returns>
    public List<GameObject> Generate(Dictionary<string, GameObject[]> generationDict)
    {
        List<GameObject> generatedItems = new();

        foreach (Marker marker in generationMarkers)
        {
            if(!generationDict.ContainsKey(marker.markerType.ToLower()))
            {
                Debug.LogWarning($"No marker of type \"{marker.markerType.ToLower()}\" found\nEnsure all marker types are correcly assigned!");
                continue;
            }

            GameObject[] quantums = generationDict[marker.markerType.ToLower()];

            //if a room of the same type in the designated roomGroup, replace the placeholder with a random one
            if (quantums != null && quantums.Length > 0)
            {
                GameObject quatntumToRealize = quantums[UnityEngine.Random.Range(0, quantums.Length)];
                GameObject itemGenerated = Instantiate(quatntumToRealize, marker.markerPoint.position, marker.markerPoint.rotation);
                Destroy(marker.markerPoint.gameObject);

                generatedItems.Add(itemGenerated);
            }
        }

        return generatedItems;
    }


    [Serializable]
    class Marker
    {
        public string markerType;
        public Transform markerPoint;
    }
}
