using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    [Header("카메라 세팅")]
    public Vector3 offset = new Vector3(0f, 3f, -6f);  // 월드 기준 오프셋
    public float followSpeed = 10f;

    [Header("카메라 각도")]
    public Vector3 fixedRotation = new Vector3(20f, 0f, 0f);  // 항상 유지할 각도

    private void LateUpdate()
    {
        if (target == null) return;

        // 1) 목표 위치 = 플레이어 위치 + "월드 기준" 오프셋 (TransformDirection 사용 X)
        Vector3 desiredPos = target.position + offset;

        // 2) 부드럽게 따라가기
        transform.position = Vector3.Lerp(transform.position, desiredPos, followSpeed * Time.deltaTime);

        // 3) 카메라의 회전은 항상 고정
        transform.rotation = Quaternion.Euler(fixedRotation);
    }
}
