using UnityEngine;

public class DestroyZone : MonoBehaviour
{
    public int scorePenalty = -5;
    // 이 구역에 무언가 들어오면 실행
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("무언가 닿았습니다: " + other.name);
        Destroy(other.gameObject);
        GameManager.Instance.AddScore(scorePenalty);

        // 만약 태그 상관없이 닿는 모든 걸 지우고 싶다면 조건문 없이 바로 Destroy 쓰면 됩니다.
        // Destroy(other.gameObject); 
    }
}
