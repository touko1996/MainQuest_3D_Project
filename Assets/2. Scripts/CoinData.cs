using UnityEngine;

[CreateAssetMenu(fileName = "CoinData", menuName = "Items/Coin")]
public class CoinData : ScriptableObject
{
    [Header("코인 정보")]         
    public string coinName = "Coin";  

    [Header("점수")]
    public int scoreValue = 1;     // 먹었을 때 올라갈 점수        

    [Header("사운드/이펙트")]
    public AudioClip pickupSfx;    // 먹을 때 사운드 (선택)
    public GameObject pickupEffectPrefab; // 먹을 때 이펙트 (선택)

    
}
