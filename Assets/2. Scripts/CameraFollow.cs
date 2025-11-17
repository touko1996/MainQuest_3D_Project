using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("추적 대상")]
    public Transform target;   

    [Header("카메라 위치 설정")]
    public Vector3 offset = new Vector3(0f, 3f, -6f);     
    public float followSpeed = 10f;                      

    [Header("카메라 고정 각도")]
    public Vector3 fixedRotation = new Vector3(20f, 0f, 0f);  // 항상 유지할 카메라 각도

    private void LateUpdate()
    {
        if (target == null) return;

        // 목표 위치 = 플레이어 위치 + 오프셋
        Vector3 targetPos = target.position + offset;

        // 부드러운 카메라 이동
        transform.position = Vector3.Lerp(
            transform.position,
            targetPos,
            followSpeed * Time.deltaTime);

        // 고정된 카메라 각도 유지
        transform.rotation = Quaternion.Euler(fixedRotation);
    }
}
