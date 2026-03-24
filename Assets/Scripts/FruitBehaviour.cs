using UnityEngine;

public class FruitBehaviour : MonoBehaviour
{
    public float timeout;
    private float timeStart;
    public GameObject[] fruits;
    public int fruitType;
    private AudioSource mergeSource;

    // Guard flag — prevents this fruit from merging more than once
    private bool hasMerged = false;

    void Start()
    {
        fruits = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehaviour>().fruits;
        mergeSource = GameObject.FindGameObjectWithTag("Player").GetComponents<AudioSource>()[0];

        // New fruits spawned from a merge start with their collider OFF.
        // PlayerBehaviour already handles turning it on for dropped fruits.
        // The merged fruit enables its own collider after a short delay (see below).
    }

    void Update() { }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // Already merging — ignore all further collisions this frame
        if (hasMerged) return;

        FruitBehaviour otherFruit = other.gameObject.GetComponent<FruitBehaviour>();

        if (otherFruit == null) return;                          // not a fruit
        if (otherFruit.hasMerged) return;                        // other already merging
        if (otherFruit.fruitType != fruitType) return;           // different type
        if (fruitType >= fruits.Length - 1) return;              // already max fruit

        // Use instance ID to decide which fruit "wins" the merge.
        // Only the fruit with the lower ID acts — guarantees exactly one merge per pair.
        if (gameObject.GetInstanceID() > other.gameObject.GetInstanceID()) return;

        // Lock both fruits immediately so no further collision events on either can re-trigger
        hasMerged = true;
        otherFruit.hasMerged = true;

        // Spawn merged fruit at midpoint
        int nextType = fruitType + 1;
        Vector3 spawnPos = Vector3.Lerp(
            gameObject.transform.position,
            other.gameObject.transform.position,
            0.5f
        );

        GameObject merged = Instantiate(fruits[nextType], spawnPos, Quaternion.identity);

        // Keep collider OFF on spawn — enable it after a short delay so the
        // new fruit doesn't immediately re-trigger merges before destroyed objects
        // are actually gone (Unity defers Destroy to end-of-frame).
        merged.GetComponent<Collider2D>().enabled = false;
        Rigidbody2D rb = merged.GetComponent<Rigidbody2D>();
        rb.gravityScale = 1.0f;

        // Re-enable collider on the merged fruit next frame via coroutine
        merged.GetComponent<FruitBehaviour>().EnableColliderDelayed();

        // Audio + score
        mergeSource.Play();
        GameObject.FindGameObjectWithTag("Player")
                  .GetComponent<PlayerBehaviour>()
                  .updateScore(fruitType);

        // Destroy both source fruits
        Destroy(other.gameObject);
        Destroy(gameObject);
    }

    // Called on the newly spawned merged fruit to safely re-enable its collider
    public void EnableColliderDelayed()
    {
        StartCoroutine(EnableColliderNextFrame());
    }

    private System.Collections.IEnumerator EnableColliderNextFrame()
    {
        yield return null; // wait one frame so destroyed fruits are actually gone
        if (this != null && gameObject != null)
            GetComponent<Collider2D>().enabled = true;
    }
}