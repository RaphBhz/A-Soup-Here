using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityRandom = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    public GameObject[] Ingredients;
    public Transform[] points;
    public float beat = (60 / 130) * 2;
    public GameObject[] Throwers;
    
    private float timer;
    private System.Random rnd = new();

    public string GetRandomIngredient()
    {
        return Ingredients[rnd.Next(Ingredients.Length)].gameObject.name;
    }

    private void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(timer > beat)
        {
            int throwFrom = UnityRandom.Range(0, points.Length);
            GameObject cube = Instantiate(Ingredients[UnityRandom.Range(0, Ingredients.Length)], points[throwFrom]);
            cube.transform.localPosition = Vector3.zero;
            cube.transform.Rotate(transform.forward, 90 * UnityRandom.Range(0, 4));
            timer -= beat;

            // Throwers[throwFrom].GetComponent<Animation>().Play();
        }
        timer += Time.deltaTime;
    }
}
