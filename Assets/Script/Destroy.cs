using TMPro;
using UnityEngine;

public class Destroy : MonoBehaviour
{

    public TextMeshProUGUI scoreText;
    private int score = 0;

    private void Start()
    {
        scoreText.text = $"Score: {score}";
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            Destroy(other.gameObject);
            scoreText.text = $"Score: {score++}";
        }
    }
}
