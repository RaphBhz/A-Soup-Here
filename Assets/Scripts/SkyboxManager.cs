using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxManager : MonoBehaviour
{
    [SerializeField] Material[] Skyboxes;

    private System.Random rnd = new();

    void Start()
    {
        RenderSettings.skybox = Skyboxes[rnd.Next(Skyboxes.Length)];
    }
}
