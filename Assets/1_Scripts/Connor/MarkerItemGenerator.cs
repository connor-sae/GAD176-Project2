using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerItemGenerator : MonoBehaviour
{
    [SerializeField]
    private Marker[] generationMarkers;

    /// <summary>
    /// Generates Items at the points specified by the markers using their ID and their corrosponding GameObjects in the dictionary
    /// </summary>
    /// <param name="generationDict">the dictionary containing what can be generated</param>
    /// <returns>the GameObjects Generated</returns>
    public void Generate(Dictionary<string, GameObject[]> generationDict)
    {
        //List<GameObject> generatedItems = new();

        foreach (Marker marker in generationMarkers)
        {
            if(marker == null)
            {
                Debug.LogWarning($"marker added but not assigned for {name}\ncheck generator references!");
                continue;
            }
            if(!generationDict.ContainsKey(marker.ID.ToLower()))
            {
                Debug.LogWarning($"No marker of type \"{marker.ID.ToLower()}\" found\nEnsure all marker types are correcly assigned!");
                continue;
            }

            GameObject[] quantums = generationDict[marker.ID.ToLower()];

            //if a room of the same type in the designated roomGroup, replace the placeholder with a random one
            if (quantums != null && quantums.Length > 0)
            {
                GameObject quatntumToRealize = quantums[Random.Range(0, quantums.Length)];

                Quaternion randomRot = marker.transform.rotation;
                if (marker.randomizeRotation)
                {
                    if(marker.lockTo90)
                        randomRot = Quaternion.Euler(0, Random.Range(0, 4) * 90, 0);
                    else
                        randomRot = Quaternion.Euler(0, Random.Range(0, 360f), 0);
                }

                GameObject itemGenerated = Instantiate(quatntumToRealize, marker.transform.position, randomRot);
                Destroy(marker.gameObject);

                //generatedItems.Add(itemGenerated);
            }
        }

        //return generatedItems;
    }


}
