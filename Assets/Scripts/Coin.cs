using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ScoreSystem score = GameObject.FindGameObjectWithTag("Score").GetComponent<ScoreSystem>();
            score.AddScore(1);
            Destroy(gameObject);
            Debug.Log(score.score);
        }
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
