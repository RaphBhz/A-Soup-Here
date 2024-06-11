using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cube : MonoBehaviour
{
    private Rigidbody rb;
    private System.Random rnd = new();

    private void Awake()
    {
        float random = (100 + (rnd.Next(20) - 10)) / 100f;
        rb = GetComponent<Rigidbody>();
        rb.AddForce(((transform.forward * 100f + transform.up * 45f) * 18f) * random);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
