using UnityEngine;
using System.Collections.Generic;

public class CoinPoolManager : MonoBehaviour
{
    public static CoinPoolManager Instance;

    [Header("코인 프리팹")]
    public GameObject coinPrefab;

    [Header("풀 크기")]
    public int poolSize = 10;

    private Queue<GameObject> pool = new Queue<GameObject>();

    private void Awake()
    {
        Instance = this;

        // 초기 코인 풀 생성
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(coinPrefab);
            obj.SetActive(false);
            pool.Enqueue(obj);
        }
    }

    
    // 비활성화된 코인을 풀에서 꺼내서 반환
    public GameObject GetCoin()
    {
        if (pool.Count > 0)
        {
            GameObject coin = pool.Dequeue();
            coin.SetActive(true);
            return coin;
        }

        return null; 
    }

   
    // 사용을 마친 코인을 풀에 다시 넣기
    public void ReturnCoin(GameObject coin)
    {
        coin.SetActive(false);
        pool.Enqueue(coin);
    }
}
