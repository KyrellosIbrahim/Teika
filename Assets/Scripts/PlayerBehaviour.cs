using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehaviour : MonoBehaviour
{
    public float speed;
    private GameObject currentFruit;
    public float fruitOffsetY = -0.6f;
    public GameObject[] fruits;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(currentFruit != null) {
            // current player position
            Vector3 playerPos = transform.position;
            Vector3 fruitOffset = new Vector3(0.0f, fruitOffsetY, 0.0f);
            currentFruit.transform.position = playerPos + fruitOffset;
        }
        else {
            // make player hold a new fruit
            int choice = Random.Range(0, fruits.Length);
            currentFruit = Instantiate(fruits[choice], new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
        }
        Keyboard k = Keyboard.current;
        if(k.spaceKey.wasPressedThisFrame) {
            Rigidbody2D body = currentFruit.GetComponent<Rigidbody2D>();
            body.gravityScale = 1.0f;

            Collider2D collider = currentFruit.GetComponent<Collider2D>();
            collider.enabled = true;
            
            currentFruit = null;

        }
        if(k.leftArrowKey.isPressed || k.aKey.isPressed)
        {
            Vector3 newPos = transform.position;
            newPos.x -= speed;
            transform.position = newPos;
        }
        if(k.rightArrowKey.isPressed || k.dKey.isPressed)
        {
            Vector3 newPos = transform.position;
            newPos.x += speed;
            transform.position = newPos;
        }
    }
}
