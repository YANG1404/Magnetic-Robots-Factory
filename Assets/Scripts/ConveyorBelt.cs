using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    [Header("설정")]
    public float speed = 1.0f; // 벨트 이동 속도
    public Vector3 direction = Vector3.right; // 이동 방향 (기본: 오른쪽/X축)

    

    void Start()
    {
       
    }

    void Update()
    {
        
    }

    // 2. 물리적 효과: 충돌 중인 물체를 이동시킴
    void OnCollisionStay(Collision other)
    {
        // 벨트 위에 있는 물체(other)의 위치를 강제로 이동
        // transform.right는 로컬 X축 기준, Vector3.right는 월드 X축 기준

        // 물체를 "현재 위치 + (방향 * 속도 * 시간)" 위치로 이동
        // MovePosition을 사용해야 물리 충돌을 유지하면서 부드럽게 이동함
        if (other.rigidbody != null && !other.rigidbody.isKinematic)
        {
            // 월드 좌표 X축으로 이동하고 싶다면 Vector3.right 사용
            // 벨트가 회전해도 벨트 방향으로 보내고 싶다면 transform.right 사용
            Vector3 targetPosition = other.transform.position + (direction.normalized * speed * Time.fixedDeltaTime);
            other.rigidbody.MovePosition(targetPosition);
        }
    }

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }
}
