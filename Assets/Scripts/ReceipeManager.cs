using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class ReceipeManager : MonoBehaviour
{
    [SerializeField] int ingredientCount = 4;
    [SerializeField] Image[] CurrentIngredients;
    [SerializeField] TMP_Text ScoreText;
    [SerializeField] GameObject spawnerObject;
    [SerializeField] Texture2D[] Images;

    private Spawner spawner;
    private System.Random rnd = new();
    private List<string> mixture = new List<string>();
    private int score = 0;
    private int currentIngredientsIdx = 0;

    public Texture2D GetImage(string name)
    {
        for (int i = 0; i < Images.Length; i++)
        {
            if (Images[i].name == name)
            {
                return Images[i];
            }
        }
        return null;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
        int i = 0;
        for (; i < Images.Length; i++)
        {
            if (Images[i].name == collision.gameObject.name || $"{Images[i].name}(Clone)" == collision.gameObject.name)
            {
                if (currentIngredientsIdx > CurrentIngredients.Length)
                {
                    currentIngredientsIdx = 0;
                }

                mixture.Add(collision.gameObject.name);

                Sprite sprite = Sprite.Create(
                    Images[i],
                    new Rect(0, 0, Images[currentIngredientsIdx].width, Images[currentIngredientsIdx].height),
                    new Vector2(0.5f, 0.5f)
                );
                CurrentIngredients[currentIngredientsIdx].sprite = sprite;
                Destroy(collision.gameObject);

                currentIngredientsIdx++;
            }
        }
    }

    public void Complete()
    {
        mixture = new List<string>();
        score += 100;
        ScoreText.text = $"{score.ToString()}";
    }

    public bool CompareMixture(List<string> receipe)
    {
        List<string> tmp = new List<string>(mixture);

        foreach (var ingredient in receipe)
        {
            if (tmp.Contains(ingredient))
            {
                tmp.Remove(ingredient);
            } else
            {
                return false;
            }
        }

        return tmp.Count == 0;
    }

    public List<string> GetRandomReceipe()
    {
        int size = rnd.Next(3) + 1;
        List<string> newReceipe = new List<string>();

        for (int i = 0; i < size; i++)
        {
            newReceipe.Add(spawner.GetRandomIngredient());
        }

        return newReceipe;
    }

    private void Awake()
    {
        spawner = spawnerObject.GetComponent<Spawner>();
        ScoreText.text = $"{score.ToString()}";
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
