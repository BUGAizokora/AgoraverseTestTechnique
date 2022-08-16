using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GameManager : MonoBehaviour
{
    // stockage des batiment batte
    [System.Serializable]
    public struct Batiment
    {
        public string nameString;
        public GameObject prefab;
        public KeyCode key;
    }
    public List<Batiment> BatimentList;

    // Pour la gestion de la sélection
    public enum Mode { Nothing, Build, ChangeColor, Select }
    public int actualMode = (int)Mode.Nothing;
    GameObject objectSelect;
    private string nameSelect = "";

    // Update is called once per frame
    void Update()
    {
        // Sert de sécuriter 
        if (actualMode == (int)Mode.Nothing) { 
            objectSelect = null;
            nameSelect = "";
        }

        if (Input.anyKeyDown) { 
            if (actualMode == (int)Mode.Build || actualMode == (int)Mode.Nothing) { 
                for (int i = 0; i < BatimentList.Count; i++) {
                    
                    //On annule la construction
                    if (Input.GetKeyDown(BatimentList[i].key) && nameSelect == BatimentList[i].nameString)
                    {
                        Destroy(objectSelect);
                        objectSelect = null;
                        nameSelect = "";
                        actualMode = (int)Mode.Nothing;
                        break;
                    } else { actualMode = (int)Mode.Build; }

                    // On créer le props sélection
                    if (Input.GetKeyDown(BatimentList[i].key) && actualMode == (int)Mode.Build) {
                        actualMode = (int)Mode.Build;

                        // On suprime la sélection, si un autre props était sélection
                        if (objectSelect != BatimentList[i].prefab) { 
                            Destroy(objectSelect);
                            objectSelect = null;
                            nameSelect = "";
                        }
                    
                        objectSelect = Instantiate(BatimentList[i].prefab);
                        nameSelect = BatimentList[i].nameString;
                    
                        objectSelect.GetComponent<BuildingScript>().GM = GetComponent<GameManager>();

                        break;
                    }
                }
            }
        }

    }
}
