using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    [SerializeField] GameObject Player;
    [SerializeField] GameObject[] NPCs;
    [SerializeField] GameObject[] TablePositions;
    [SerializeField] GameObject TableCenter;
    [SerializeField] GameObject PortalPosition;
    [SerializeField] float moveDuration = 5f;
    [SerializeField] float waitDuration = 10f;
    [SerializeField] float cooldown = 4f;
    [SerializeField] GameObject DialogBubble;
    [SerializeField] ReceipeManager ReceipeManager;

    private float Timer = 0f;
    private System.Random rnd = new();
    private GameObject[] Spawned;
    private AudioSource WarpSound;
    int nbClients = 3;

    // Start is called before the first frame update
    void Start()
    {
        WarpSound = GetComponent<AudioSource>();
        Spawned = new GameObject[nbClients];

        for (int i = 0; i < Spawned.Length; i++)
        {
            Spawned[i] = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Timer += Time.deltaTime;
        if (Timer <= cooldown)
        {
            return;
        }

        Timer = 0f;
        for (int i = 0; i < Spawned.Length; i++)
        {
            if (Spawned[i] == null || Spawned[i].gameObject.IsDestroyed())
            {
                Spawned[i] = Instantiate(NPCs[rnd.Next(nbClients)]);
                Spawned[i].transform.position = PortalPosition.transform.position;
                Spawned[i].transform.LookAt(TablePositions[i].transform.position);
                MovementManager movementScript = Spawned[i].GetComponent<MovementManager>();

                if (movementScript != null)
                {
                    // Set the movement parameters
                    movementScript.firstTargetPosition = TablePositions[i].transform.position;
                    movementScript.secondTargetPosition = PortalPosition.transform.position;
                    movementScript.moveDuration = moveDuration;
                    movementScript.waitDuration = waitDuration;
                    movementScript.toFace = TableCenter.transform.position;
                    movementScript.dialogBubble = DialogBubble;
                    movementScript.receipe = ReceipeManager.GetRandomReceipe();
                    movementScript.receipeManager = ReceipeManager;
                    movementScript.player = Player;

                    // Start the movement coroutine
                    StartCoroutine(movementScript.MoveAndWaitCoroutine());
                }

                WarpSound.Play();
                break;
            }
        }
    }
}
