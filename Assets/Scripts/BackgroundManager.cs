using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    public float speed;
    public GameObject backPrefab;
    private GameObject[] backs;
    public float scale;

    private float camRight;
    private float camTop;
    private float spriteWidth;
    private float spriteHeight;

    void Start()
    {
        Camera cam = Camera.main;
        camRight = cam.ViewportToWorldPoint(new Vector3(1, 1, 0)).x;
        camTop   = cam.ViewportToWorldPoint(new Vector3(1, 1, 0)).y;

        backPrefab.transform.localScale = new Vector3(scale, scale, scale);
        Sprite s = backPrefab.GetComponent<SpriteRenderer>().sprite;
        spriteWidth  = s.bounds.size.x * scale;
        spriteHeight = s.bounds.size.y * scale;

        float overlap = 0.6f; // lower = more overlap, 1.0 = edge to edge

        backs = new GameObject[4];
        for (int i = 0; i < 4; i++) {
            Vector2 pos = new Vector2(camRight + spriteWidth  * overlap * i,
                                      camTop   + spriteHeight * overlap * i);
            backs[i] = Instantiate(backPrefab, pos, Quaternion.identity);
        }
    }

    void Update()
    {
        float camLeft   = Camera.main.ViewportToWorldPoint(Vector3.zero).x;
        float camBottom = Camera.main.ViewportToWorldPoint(Vector3.zero).y;

        for (int i = 0; i < 4; i++) {
            Vector3 pos = backs[i].transform.position;
            pos.x -= speed * Time.deltaTime;
            pos.y -= speed * Time.deltaTime;
            backs[i].transform.position = pos;

            if (pos.x < camLeft - spriteWidth || pos.y < camBottom - spriteHeight) {
                backs[i].transform.position = new Vector2(camRight + spriteWidth,
                                                          camTop   + spriteHeight);
            }
        }
    }
}