using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [Header("배경 음악")]
    public AudioSource bgmSource;
    public AudioClip bgmClip;

    [Header("효과음")]
    public AudioSource sfxSource;
    public AudioClip gameOverSfx;
    public AudioClip gameClearSfx;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void OnEnable()
    {
        // 코인 이벤트 구독
        CoinEvents.OnCoinCollected += HandleCoinPickupSFX;
    }

    private void OnDisable()
    {
        CoinEvents.OnCoinCollected -= HandleCoinPickupSFX;
    }

    private void Start()
    {
        PlayBGM();
    }

    public void PlayBGM()
    {
        if (bgmSource == null || bgmClip == null) return;

        bgmSource.Stop();
        bgmSource.clip = bgmClip;
        bgmSource.loop = true;
        bgmSource.Play();
    }

    public void StopBGM()
    {
        if (bgmSource.isPlaying)
            bgmSource.Stop();
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip == null) return;
        sfxSource.PlayOneShot(clip);
    }

    public void PlayGameOverSFX()
    {
        PlaySFX(gameOverSfx);
    }

    public void PlayGameClearSFX()
    {
        PlaySFX(gameClearSfx);
    }

    // 코인 수집 이벤트를 듣고 자동으로 사운드 재생
    private void HandleCoinPickupSFX(Coin coin)
    {
        PlaySFX(coin.data.pickupSfx);
    }
}
