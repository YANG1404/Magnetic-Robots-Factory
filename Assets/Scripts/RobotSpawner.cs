using UnityEngine;

public class RobotSpawner : MonoBehaviour
{
    [Header("프리팹 연결")]
    public GameObject redRobotPrefab;   // 빨간 로봇 프리팹
    public GameObject blueRobotPrefab;  // 파란 로봇 프리팹

    [Header("생성 설정")]
    public float spawnInterval = 2.0f;  // 생성 간격 (초)

    void OnEnable()
    {
        // 반복 실행 시작
        InvokeRepeating("SpawnRobot", 0f, spawnInterval);
    }

    // OnDisable은 이 오브젝트가 SetActive(false)가 될 때마다 실행됩니다.
    void OnDisable()
    {
        // 반복 실행 취소 (이게 없으면 꺼져도 백그라운드에서 에러가 날 수 있음)
        CancelInvoke("SpawnRobot");
    }

    void SpawnRobot()
    {
        // (방어 코드) 만약 게임이 끝났는데 혹시라도 실행되면 막기 위해
        if (!gameObject.activeInHierarchy) return;

        // 1. 0 또는 1을 랜덤으로 뽑음
        int randomIndex = Random.Range(0, 2);

        // 2. 랜덤 결과에 따라 생성할 로봇 결정
        GameObject robotToSpawn = (randomIndex == 0) ? redRobotPrefab : blueRobotPrefab;

        // 3. 로봇 생성
        Instantiate(robotToSpawn, transform.position, transform.rotation);
    }
}
