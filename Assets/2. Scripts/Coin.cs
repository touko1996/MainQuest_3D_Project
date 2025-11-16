using UnityEngine;

public class Coin : MonoBehaviour
{
    [Header("코인데이터")]
    public CoinData data;

    [Header("자석효과")]
    public float magnetRange = 3f;       
    public float magnetSpeed = 8f;       

    private Transform player;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    private void Update()
    {
        if (player == null) return;

        transform.Rotate(0, 180f * Time.deltaTime, 0); //360도 돌면서 회전 

        float distance = Vector3.Distance(transform.position, player.position);

        // 자석효과
        if (distance < magnetRange)
        {
            Vector3 dir = (player.position - transform.position).normalized;
            transform.position += dir * magnetSpeed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        // 점수 추가
        //GameManager.Instance.AddScore(data.scoreValue);

        // 사운드 재생
        if (data.pickupSfx != null)
        {
            AudioSource.PlayClipAtPoint(data.pickupSfx, transform.position);
        }

        // 이펙트 재생
        if (data.pickupEffectPrefab != null)
        {
            Instantiate(data.pickupEffectPrefab, transform.position, Quaternion.identity);
        }

        // 코인 비활성화
        gameObject.SetActive(false);
    }
}
