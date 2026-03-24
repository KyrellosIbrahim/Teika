using UnityEngine;
using System.Collections.Generic;

public class QueueManager : MonoBehaviour
{
    public Sprite[] UISprites;
    public Queue<int> queue;
    private SpriteRenderer[] childRenderers;
    private int currentHeld = -1; // tracks what the player is currently holding

    void Awake()
    {
        queue = new Queue<int>();
        // Fill queue with one extra so slots 1-3 are always populated after first dequeue
        for (int i = 0; i < 4; i++) {
            queue.Enqueue(Random.Range(0, 4));
        }

        childRenderers = new SpriteRenderer[4];
        for (int i = 0; i < transform.childCount; i++) {
            childRenderers[i] = transform.GetChild(i).GetComponent<SpriteRenderer>();
        }
    }

    void Update()
    {
        // Slot 0 = what the player is currently holding
        if (currentHeld >= 0)
            childRenderers[0].sprite = UISprites[currentHeld];

        // Slots 1-3 = upcoming fruits in queue order
        int[] upcoming = queue.ToArray();
        for (int i = 1; i < childRenderers.Length; i++) {
            childRenderers[i].sprite = UISprites[upcoming[i - 1]];
        }
    }

    public int updateQueue() {
        currentHeld = queue.Dequeue();
        queue.Enqueue(Random.Range(0, 4));
        return currentHeld;
    }
}