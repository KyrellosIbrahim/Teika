using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    public float speed;
    public float pivotPoint;
    public GameObject backPrefab;
    private GameObject[] backs;
    public float scale;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pivotPoint = scale * 16 * -0.32f;
        backPrefab.transform.localScale = new Vector3(scale, scale, scale);
        backs = new GameObject[3];
        for(int i = 0; i < 3; i++) {
            float xPos = pivotPoint - (pivotPoint/2 * i);
            float yPos = pivotPoint - (pivotPoint/2 * i);
            Vector2 position = new Vector2(xPos, yPos);
            backs[i] = Instantiate(backPrefab, position, Quaternion.identity);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
     for(int i = 0; i < 3; i++) {
        float xPos = backs[i].transform.position.x + speed;
        float yPos = backs[i].transform.position.y + speed;
        Vector2 position = new Vector2(xPos, yPos);
        if(backs[i].transform.position.x > -pivotPoint/2) {
            position = new Vector2(pivotPoint, pivotPoint);
        }
        backs[i].transform.position = position;
     }   
    }
}
