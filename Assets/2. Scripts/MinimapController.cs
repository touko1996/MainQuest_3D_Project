using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class MinimapController : MonoBehaviour
{
    [Header("참조")]
    public Transform player;
    public RectTransform minimapPanel;
    public RectTransform coinIconParent;
    public GameObject coinIconPrefab;

    [Header("설정")]
    public float mapScale = 4f; // 스케일 조정

    private Dictionary<GameObject, RectTransform> coinIcons                 
        = new Dictionary<GameObject, RectTransform>();                          //게임오브젝트랑 렉트트랜스폼을 딕셔너리로 받아서 오브젝트 ->해당아이콘으로 매칭

    void Start()
    {
        UpdateCoinIcons();
    }

    void Update()
    {
        UpdateCoinIconPositions();
    }

    public void UpdateCoinIcons()
    {
        // 기존 아이콘 제거
        foreach (var pair in coinIcons)
            Destroy(pair.Value.gameObject);

        coinIcons.Clear();

        // 씬 안에 활성/비활성 포함해서 모든 코인 검색
        Coin[] coins = FindObjectsOfType<Coin>(true);

        foreach (Coin coin in coins)
        {
            GameObject icon = Instantiate(coinIconPrefab, coinIconParent);
            RectTransform rt = icon.GetComponent<RectTransform>();
            coinIcons.Add(coin.gameObject, rt);
        }
    }

    private void UpdateCoinIconPositions()
    {
        foreach (var pair in coinIcons)
        {
            GameObject coinObj = pair.Key;
            RectTransform iconRT = pair.Value;

            if (!coinObj.activeSelf) //코인을 수집해서 비활성화되면 자동적으로 코인아이콘도 비활성화 때리기
            {
                iconRT.gameObject.SetActive(false);
                continue;
            }

            Vector3 offset = coinObj.transform.position - player.position;
            Vector2 minimapPos = new Vector2(offset.x, offset.z) * mapScale;
            iconRT.anchoredPosition = minimapPos;
        }
    }
}
