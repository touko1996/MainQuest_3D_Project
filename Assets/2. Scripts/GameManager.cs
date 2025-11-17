using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("UI")]
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI coinText;

    public GameObject gameOverPanel;
    public GameObject gameClearPanel;

    [Header("게임 설정")]
    public float startTime = 60f;
    private float remainingTime;

    [Header("코인 관련 설정")]
    public int totalCoinsCollected = 0;        
    public CoinSpawner spawner;               // 목표 개수(totalTargetCoins) 참고용

    private bool isGameOver = false;
    private bool isGameClear = false;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void OnEnable()
    {
        // 코인 수집 이벤트 구독
        CoinEvents.OnCoinCollected += HandleCoinCollected;
    }

    private void OnDisable()
    {
        // 이벤트 해제
        CoinEvents.OnCoinCollected -= HandleCoinCollected;
    }

    private void Start()
    {
        remainingTime = startTime;

        timerText.text = $"Timer : {remainingTime:F1}";
        coinText.text = "Coins : 0";

        gameOverPanel.SetActive(false);
        gameClearPanel.SetActive(false);
    }

    private void Update()
    {
        if (isGameOver || isGameClear) return;

        remainingTime -= Time.deltaTime;

        if (remainingTime <= 0)
        {
            remainingTime = 0;
            GameOver();
        }

        timerText.text = $"Timer : {remainingTime:F1}";
    }

    // 코인 수집 처리
    private void HandleCoinCollected(Coin coin)
    {
        totalCoinsCollected++;
        coinText.text = $"Coins : {totalCoinsCollected}";

        // 목표 개수 달성 체크
        if (totalCoinsCollected >= spawner.totalTargetCoins)
            GameClear();
    }

    // 게임 오버
    public void GameOver()
    {
        if (isGameOver) return;

        isGameOver = true;

        Time.timeScale = 0f;
        gameOverPanel.SetActive(true);

        // 사운드 처리
        SoundManager.Instance.StopBGM();
        SoundManager.Instance.PlayGameOverSFX();
    }

    // 게임 클리어
    public void GameClear()
    {
        if (isGameClear) return;

        isGameClear = true;

        Time.timeScale = 0f;
        gameClearPanel.SetActive(true);

        // 사운드 처리
        SoundManager.Instance.StopBGM();
        SoundManager.Instance.PlayGameClearSFX();
    }

    // 재시작
    public void RestartGame()
    {
        Time.timeScale = 1f;

        SoundManager.Instance.PlayBGM();

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // 종료
    public void ExitGame()
    {
        Application.Quit();
    }
}
