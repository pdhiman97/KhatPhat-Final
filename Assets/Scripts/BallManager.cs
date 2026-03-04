using UnityEngine;

public class BallManager : MonoBehaviour
{
    public GameObject ballPrefab;
    public Transform[] spawnPoints; // Drag your child transforms here
    public GameObject exitTrigger;  // The gate area

    void Start()
    {
        SpawnBall();
        if (exitTrigger != null) exitTrigger.SetActive(false); // Hide exit until ball is found
    }

    void SpawnBall()
    {
        int randomIndex = Random.Range(0, spawnPoints.Length);
        Instantiate(ballPrefab, spawnPoints[randomIndex].position, Quaternion.identity);
    }

    public void OnBallCollected()
    {
        Debug.Log("Ball Collected! Return to the gate.");
        if (exitTrigger != null) exitTrigger.SetActive(true);
    }
}