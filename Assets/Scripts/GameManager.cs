using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    public float initialGameSpeed = 5f;
    public float gameSpeedIncrease = 0.1f;
    public float gameSpeed {  get; private set; }

    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI hgScoreText;
    public TextMeshProUGUI scoreMultText;
    public Button retryButton;

    private Player player;
    private Spawner spawner;

    private float score;
    private float scoreMultiplier;
    private float scoreMultTxt;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            DestroyImmediate(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
        }
    }

    private void Start()
    {
        player = FindObjectOfType<Player>();
        spawner = FindObjectOfType<Spawner>();

        NewGame();
    }

    public void NewGame()
    {
        Obstacle[] obstacles = FindObjectsOfType<Obstacle>();

        foreach (var obstacle in obstacles)
        {
            Destroy(obstacle.gameObject);
        }

        gameSpeed = initialGameSpeed;
        score = 0f;
        scoreMultiplier = 1f;
        scoreMultTxt = 1f;
        enabled = true;

        player.gameObject.SetActive(true);
        spawner.gameObject.SetActive(true);

        gameOverText.gameObject.SetActive(false);
        retryButton.gameObject.SetActive(false);

        UpdateHiScore();
    }

    private void Update()
    {
        gameSpeed += gameSpeedIncrease * Time.deltaTime;
        score += gameSpeed * scoreMultiplier * Time.deltaTime;
        scoreText.text = Mathf.FloorToInt(score).ToString("D5");
        scoreMultText.text = "x" + Mathf.FloorToInt(scoreMultTxt).ToString();
    }

    public void GameOver()
    {
        gameSpeed = 0f;
        enabled = false;

        player.gameObject.SetActive(false);
        spawner.gameObject.SetActive(false);

        gameOverText.gameObject.SetActive(true);
        retryButton.gameObject.SetActive(true);

        UpdateHiScore();
    }

    private void UpdateHiScore()
    {
        float hiscore = PlayerPrefs.GetFloat("hiscore", 0);

        if (score > hiscore)
        {
            hiscore = score;
            PlayerPrefs.SetFloat("hiscore", hiscore);
        }

        hgScoreText.text = Mathf.FloorToInt(hiscore).ToString("D5");
    }

    public void AddMultiplier()
    {
        scoreMultiplier += 0.25f;
        scoreMultTxt += 1f;
    }

    public void DivideGameSpeed()
    {
        float luck = Random.value;

        if (luck < 0.5f)
        {
            gameSpeed /= 1.3f;
        }
        else
        {
            gameSpeed *= 1.3f;
        }
    }
}
