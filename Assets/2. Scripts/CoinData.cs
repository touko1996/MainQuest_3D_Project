using UnityEngine;

[CreateAssetMenu(fileName = "CoinData", menuName = "Items/Coin")]
public class CoinData : ScriptableObject
{
    [Header("코인 정보")]
    public string coinName = "Coin";

    [Header("점수")]
    public int scoreValue = 1;  

    [Header("픽업 이펙트")]
    public GameObject pickupEffectPrefab;  

    [Header("픽업 사운드")]
    public AudioClip pickupSfx;           
}
