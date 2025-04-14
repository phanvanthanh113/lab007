using UnityEngine;

public class SPAM : MonoBehaviour
{
    [Header("Cai dat spawn")]
    public GameObject prefabToSpawn; // Prefab muon spam
    public Sprite[] sprites; // Danh sach sprite
    public float spawnInterval = 2f;

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            Spawn();
            timer = 0f;
        }
    }

    void Spawn()
    {
        if (prefabToSpawn == null)
        {
            Debug.LogError("Prefab chua duoc gan trong Inspector!");
            return;
        }

        if (sprites == null || sprites.Length == 0)
        {
            Debug.LogError("Chua gan sprite trong mang sprites[]!");
            return;
        }

        Vector2 randomPos = new Vector2(Random.Range(-7f, 7f), Random.Range(-3f, 4f));
        GameObject obj = Instantiate(prefabToSpawn, randomPos, Quaternion.identity);

        SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();
        if (sr == null)
        {
            Debug.LogError("Prefab khong co SpriteRenderer!");
            return;
        }

        int index = Random.Range(0, sprites.Length);
        sr.sprite = sprites[index];
        obj.name = "SPAM_" + (index + 1);

        BALL ballScript = obj.GetComponent<BALL>();
        if (ballScript != null)
        {
            ballScript.ballID = index + 1;
        }
        else
        {
            Debug.LogWarning("Prefab khong co script BALL.cs");
        }
    }
}