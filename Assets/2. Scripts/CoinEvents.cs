using System;

public static class CoinEvents //옵저버 패턴을 위한 정적 클래스
{
    // 코인이 수집되었을 때 발행되는 이벤트
    public static event Action<Coin> OnCoinCollected;

    // 이벤트 발행 함수
    // 호출하는 순간 CoinSpawner, GameManager, SoundManager, MinimapController 이벤트 동작
    public static void RaiseCoinCollected(Coin coin)
    {
        OnCoinCollected?.Invoke(coin);
    }

}
