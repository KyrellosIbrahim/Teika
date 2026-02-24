using UnityEngine;

public class FruitBehaviour : MonoBehaviour
{
    public float timeout;
    private float timeStart;
    public GameObject[] fruits;
    public int fruitType;
    private AudioSource mergeSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
     fruits = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehaviour>().fruits;
     mergeSource = GameObject.FindGameObjectWithTag("Player").GetComponents<AudioSource>()[0];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Fruit") && other.gameObject.GetComponent<FruitBehaviour>().fruitType == fruitType && fruitType < fruits.Length-1) {
            if(gameObject.transform.position.x < other.gameObject.transform.position.x // first check x levels to merge
            || (gameObject.transform.position.x == other.gameObject.transform.position.x  // if x levels are the same, check y levels to merge
            && gameObject.transform.position.y >= other.gameObject.transform.position.y)) { // merge the one that's higher up
                
                // create new fruit that's next in sequence
                int choice = fruitType + 1;
                GameObject currentFruit = Instantiate(fruits[choice], 
                Vector3.Lerp(gameObject.transform.position, other.gameObject.transform.position, 0.5f), Quaternion.identity);
                currentFruit.GetComponent<Collider2D>().enabled = true;
                currentFruit.GetComponent<Rigidbody2D>().gravityScale = 1.0f;

                // merge sound
                mergeSource.Play();

                // update score
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehaviour>().updateScore(fruitType);

                // destroy the fruits (other fruit first)
                Destroy(other.gameObject);
                Destroy(gameObject); // or Destroy(gameObject);
            }
        }
    }
}
