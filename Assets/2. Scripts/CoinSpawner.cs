using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [Header("스폰 범위")]
    public Transform plane;
    public float yOffset = 1.5f;

    [Header("스폰 설정")]
    public int maxActiveCoins = 10;    // 동시에 존재하는 코인 수
    public int totalTargetCoins = 20;  // 전체 목표 개수

    private int coinsSpawned = 0;      // 지금까지 스폰된 총량

    private void OnEnable()
    {
        // 코인 이벤트 구독
        CoinEvents.OnCoinCollected += HandleCoinCollected;
    }

    private void OnDisable()
    {
        // 이벤트 해제
        CoinEvents.OnCoinCollected -= HandleCoinCollected;
    }

    private void Start()
    {
        // 초기 코인 스폰
        for (int i = 0; i < maxActiveCoins; i++)
            SpawnOneCoin();
    }

    // 코인 수집 이벤트 발생 시 처리
    private void HandleCoinCollected(Coin coin)
    {
        // 풀링 시스템으로 반환
        CoinPoolManager.Instance.ReturnCoin(coin.gameObject);

        // 아직 스폰해야 할 코인이 남았다면 새로 스폰
        if (coinsSpawned < totalTargetCoins)
            SpawnOneCoin();
    }

    // 코인 하나 스폰
    private void SpawnOneCoin()
    {
        GameObject coin = CoinPoolManager.Instance.GetCoin();
        if (coin == null) return;

        coin.transform.position = GetRandomPointOnPlane();
        coinsSpawned++;

        // 미니맵 갱신
        FindObjectOfType<MinimapController>()?.UpdateCoinIcons();
    }

    // 랜덤 위치
    private Vector3 GetRandomPointOnPlane()
    {
        Vector3 scale = plane.localScale;

        float halfX = 5f * scale.x;
        float halfZ = 5f * scale.z;

        float randX = Random.Range(-halfX, halfX);
        float randZ = Random.Range(-halfZ, halfZ);

        return new Vector3(randX, yOffset, randZ);
    }
}
