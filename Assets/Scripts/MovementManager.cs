using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementManager : MonoBehaviour
{
    public Vector3 firstTargetPosition;
    public Vector3 secondTargetPosition;
    public float moveDuration;
    public float waitDuration;
    public Vector3 toFace;
    public string[] order;
    public GameObject dialogBubble;
    public List<string> receipe;
    public ReceipeManager receipeManager;
    public GameObject player;
    
    private Animator animator;

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        StartCoroutine(MoveAndWaitCoroutine());
    }

    public IEnumerator MoveAndWaitCoroutine()
    {
        yield return StartCoroutine(MoveOverTime(firstTargetPosition, moveDuration));

        if (animator != null)
        {
            animator.Play("Idle_A");
            animator.speed = 0.6f;
        }

        transform.LookAt(toFace);
        GameObject bubble = Instantiate(dialogBubble);
        bubble.transform.position = transform.position + new Vector3(0, 1f, 0);
        bubble.GetComponent<BubbleManager>().player = player;
        
        string order = "";
        receipe.ForEach(e => order += $"{e}\n");

        BubbleManager bubbleManager = bubble.GetComponent<BubbleManager>();
        for (int i = 0; i < receipe.Count; ++i)
        {
            bubbleManager.SetImage(receipeManager.GetImage(receipe[i]), i);
        }


        // Wait for a specified duration or until the condition is met
        float elapsedTime = 0;
        while (elapsedTime < waitDuration)
        {
            Vector3 targetPosition = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
            transform.LookAt(targetPosition);
            transform.Rotate(0, 180, 0);

            if (receipeManager.CompareMixture(receipe))
            {
                receipeManager.Complete();
                break;
            }
            elapsedTime += Time.deltaTime;
            yield return null; // Wait for the next frame
        }

        Destroy(bubble);

        yield return StartCoroutine(MoveOverTime(secondTargetPosition, moveDuration));

        Destroy(gameObject);
    }

    private IEnumerator MoveOverTime(Vector3 targetPosition, float duration)
    {
        if (animator != null)
        {
            animator.Play("Walk");
            animator.speed = 0.6f;
        }


        Vector3 startPosition = transform.position;
        transform.LookAt(targetPosition);
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;
    }
}
