using System.Runtime.InteropServices;
using UnityEngine;

public class MergingMehaviour : MonoBehaviour
{
    /*
    Merge behavior:
    Cherry -> Strawberry -> Banana -> Lemon -> Apple -> Orange -> Pear -> Pineapple -> Watermelon

    How do we create merge functionality?
    Detect when two objects collide. If the two objects are the same type, we destroy both and create a new object that follows the sequence.

    How do we add randomness to the spawns?
    When spawning a new fruit, randomly select from a few of the lower tier fruits.

    Make the game harder the longer the game goes?
    The bigger fruits obstruct more of the screen, making it harder to navigate and merge.
    */
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }
}
