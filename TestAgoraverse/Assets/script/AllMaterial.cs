using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllMaterial : MonoBehaviour
{
    public static AllMaterial mat;

    public List<Material> materiaList = new List<Material>();

    public Material poseOK;
    public Material poseNO;

    void Awake()
    {
        if (mat == null)
        {
            mat = this;
        }
    }
}
