using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehaviour : MonoBehaviour
{
    public float speed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Keyboard k = Keyboard.current;
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
