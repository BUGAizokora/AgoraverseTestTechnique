using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BuildingScript : MonoBehaviour
{
    public GameManager GM;
    private RaycastHit hit;
    private bool CanPose = false;
    public float rot = 0f;

    public int BI_cptMat = 0;

    void Start()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 50000.0f))
        {
            transform.position = hit.point;
        }
    }

    void Update()
    {
        // Déplacements du props
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 50000.0f))
        {
            transform.position = hit.point;
        }

        // Rotation du Props
        rot += Input.mouseScrollDelta.y * 4;
        transform.localRotation = Quaternion.Euler(new Vector3(transform.rotation.x, rot, transform.rotation.z));

        // Pose des Objets
        if (Input.GetMouseButtonDown(0) && CanPose)
        {
            GM.actualMode = (int)GameManager.Mode.Nothing;

            // Initialisation de l'autre script
            gameObject.AddComponent<BuildingInteract>();
            gameObject.GetComponent<BuildingInteract>().BS_rot = rot;
            gameObject.GetComponent<BuildingInteract>().BS_GM = GM;
            gameObject.GetComponent<BuildingInteract>().cptMat = BI_cptMat;

            Destroy(gameObject.GetComponent<BuildingScript>());
        }

        // Supression de l'Objet
        if (Input.GetKeyDown(KeyCode.Delete))
        {
            GM.actualMode = (int)GameManager.Mode.Nothing;
            Destroy(gameObject);
        }

        if (Input.GetMouseButtonDown(0)) { CanPose = false; }   // Sécurité lorsqu'on selection, pour pas le reposer desuite
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "props")
        {
            // Il est en collision avec autre chose
            GetComponent<Renderer>().material = AllMaterial.mat.poseNO;
            CanPose = false;
        }else
        {
            // Il est en collision avec autre chose
            GetComponent<Renderer>().material = AllMaterial.mat.poseOK;
            CanPose = true;
        }
    }
}
