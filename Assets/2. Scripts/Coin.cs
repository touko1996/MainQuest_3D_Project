using UnityEngine;

public class Coin : MonoBehaviour
{
    [Header("코인데이터")]
    public CoinData data;

    [Header("자석효과")]
    public float magnetRange = 3f;
    public float magnetSpeed = 8f;

    private Transform player;
    private bool isCollected = false;

    private void OnEnable()
    {
        isCollected = false;
        player = GameObject.FindWithTag("Player")?.transform;
    }

    private void Update()
    {
        if (player == null || isCollected) return;

        // 회전하는 효과(애니메이션 느낌)
        transform.Rotate(0f, 180f * Time.deltaTime, 0f);

        // 자석 효과
        float distance = Vector3.Distance(transform.position, player.position);
        if (distance < magnetRange)
        {
            Vector3 dir = (player.position - transform.position).normalized;
            transform.position += dir * magnetSpeed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isCollected) return;
        if (!other.CompareTag("Player")) return;

        isCollected = true;

        // 이펙트 생성 + 자동 삭제
        if (data.pickupEffectPrefab != null)
        {
            GameObject effect = Instantiate(
                data.pickupEffectPrefab,
                transform.position,
                Quaternion.identity
            );

            Destroy(effect, 2f); 
        }

        // 이벤트 발행
        CoinEvents.RaiseCoinCollected(this);

        // 풀로 복귀
        gameObject.SetActive(false);
    }

}
