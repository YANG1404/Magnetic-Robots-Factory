using UnityEngine;
using UnityEngine.SceneManagement;

public class PipeZone : MonoBehaviour
{
    public enum PipeColor { Red, Blue } // 파이프 색상 종류 정의

    [Header("파이프 설정")]
    public PipeColor thisPipeColor; // 이 파이프의 색상을 Inspector에서 선택

    [Header("점수 설정")]
    public int scoreReward = 10;   // 맞았을 때 점수
    public int scorePenalty = -5;  // 틀렸을 때 감점

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("무언가 닿았습니다: " + other.name);
        // 1. 들어온 물체의 태그를 확인해서 색상을 판별
        bool isRedRobot = other.CompareTag("RedRobot");
        bool isBlueRobot = other.CompareTag("BlueRobot");

        // 로봇이 아니면 무시
        if (!isRedRobot && !isBlueRobot) return;

        // 2. 점수 계산 로직
        if (thisPipeColor == PipeColor.Red)
        {
            // 빨간 파이프일 때
            if (isRedRobot) GameManager.Instance.AddScore(scoreReward); // 성공
            else GameManager.Instance.AddScore(scorePenalty);           // 실패
        }
        else if (thisPipeColor == PipeColor.Blue)
        {
            // 파란 파이프일 때
            if (isBlueRobot) GameManager.Instance.AddScore(scoreReward); // 성공
            else GameManager.Instance.AddScore(scorePenalty);            // 실패
        }

        // 3. 로봇 삭제
        Destroy(other.gameObject);
    }
}
