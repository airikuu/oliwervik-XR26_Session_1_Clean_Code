using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField] private int scoreValue = 10;

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;

        var scorer = collision.gameObject.GetComponent<PlayerScore>();
        if (scorer != null)
        {
            scorer.AddScore(scoreValue);
        }

        Destroy(gameObject);
    }
}

