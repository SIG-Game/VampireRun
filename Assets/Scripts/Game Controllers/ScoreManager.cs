using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }
    public float Score
    {
        get { return score; }
        private set { score = value; }
    }

    [SerializeField]
    private TextMeshProUGUI scoreText;

    private const float moveMulti4Pts = 10.0f, // The value to multiply the distance traveled by
                        enemyFelledPts = 100.0f; // The points killing an enemy is worth

    private float score,
                  displayScore,
                  pastScore,
                  startY,
                  transitionSpeed = 100.0f;

    public void SetStartY(float y) { startY = y; }
    
    public void IncreaseScore(float amount) { Score += amount; }

    public void UpdateScoreDisplay()
    {
        scoreText.text = string.Format("Score: {0:00000}", displayScore);
    }

    public void EnemyFelled()
    {
        IncreaseScore(enemyFelledPts);
    }

    public void BossFelled(float pts)
    {
        IncreaseScore(pts);
    }

    public void AbruptLevelEnd()
    {
        GameObject player = GameObject.FindWithTag("Player");
        LevelEnd(player.transform.position.y);
    }

    public void LevelEnd(float curY)
    {
        if (curY > startY)
        {
            IncreaseScore(moveMulti4Pts * (curY - startY));
        }

        Score = (pastScore > Score) ? pastScore : Score;
        PlayerPrefs.SetFloat("lvl" + SceneManager.GetActiveScene().buildIndex + "HighScore", Score);
    }

    private void Awake()
    {
        Instance = this;
        pastScore = PlayerPrefs.GetFloat("lvl" + SceneManager.GetActiveScene().buildIndex + "HighScore");
    }

    private void Update()
    {
        displayScore = Mathf.MoveTowards(displayScore, score, transitionSpeed * Time.deltaTime);
        UpdateScoreDisplay();
    }
}