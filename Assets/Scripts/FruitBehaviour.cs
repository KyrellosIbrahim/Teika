using UnityEngine;

public class FruitBehaviour : MonoBehaviour
{
    public float timeout;
    private float timeStart;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("TopBorder")) {
            timeStart = Time.time;
        }
    }

    private void OnTriggerStay2D(Collider2D other) {
        if(other.gameObject.CompareTag("TopBorder")) {
            float currentTime = Time.time;
            float timeThusFar = currentTime - timeStart;
            if(timeThusFar > timeout) {
                print("Game Over!!");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.CompareTag("TopBorder")) {
                timeStart = 0.0f;
        }
    }
}
