using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingInteract : MonoBehaviour
{
    public int cptMat = 0;

    public float BS_rot = 0f;
    public GameManager BS_GM;
    void Start()
    {
        GetComponent<Renderer>().material = AllMaterial.mat.materiaList[cptMat];

        gameObject.layer = LayerMask.NameToLayer("Default"); // 0;
    }

    private void OnMouseOver()
    {
        // Replacement
        if (Input.GetMouseButtonDown(0) && BS_GM.actualMode == (int)GameManager.Mode.Nothing) {
            BS_GM.actualMode = (int)GameManager.Mode.Select;

            // Initialisation de l'autre script
            gameObject.AddComponent<BuildingScript>();
            gameObject.GetComponent<BuildingScript>().rot = BS_rot;
            gameObject.GetComponent<BuildingScript>().GM = BS_GM;
            gameObject.GetComponent<BuildingScript>().BI_cptMat = cptMat;

            gameObject.layer = LayerMask.NameToLayer("Ignore Raycast"); // 2

            Destroy(gameObject.GetComponent<BuildingInteract>());
        }

        // Changement de couleur
        if (Input.GetMouseButtonDown(1) && BS_GM.actualMode == (int)GameManager.Mode.Nothing) {
            BS_GM.actualMode = (int)GameManager.Mode.Select;

            cptMat++;
            cptMat %= AllMaterial.mat.materiaList.Count;
            GetComponent<Renderer>().material = AllMaterial.mat.materiaList[cptMat];
        }

        // Sécuriter pour éviter que plusieurs features ce chauvauche
        if (!Input.GetMouseButton(0) && !Input.GetMouseButton(1) && BS_GM.actualMode != (int)GameManager.Mode.Build)
        {
            BS_GM.actualMode = (int)GameManager.Mode.Nothing;
        }
    }
}
