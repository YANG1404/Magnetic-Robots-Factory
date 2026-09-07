using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("--- 감시할 대상 (조건) ---")]
    [Tooltip("이 오브젝트가 켜져야 함")]
    [SerializeField] private GameObject _target1;
    [Tooltip("이 오브젝트도 켜져야 함")]
    [SerializeField] private GameObject _target2;

    [Header("--- 실행할 동작 (결과) ---")]
    [Tooltip("조건이 만족되면 사라질 오브젝트 (A)")]
    [SerializeField] private GameObject _objectToHide;
    [Tooltip("조건이 만족되면 나타날 오브젝트 (B)")]
    [SerializeField] private GameObject _objectToShow;

    

    // 중복 실행 방지용 플래그
    private bool _isTriggered = false;

    private void Update()
    {
        // 1. 두 타겟이 모두 활성화 상태인지 확인 (activeInHierarchy는 부모가 꺼져도 false로 처리되어 안전함)
        bool conditionMet = _target1.activeInHierarchy && _target2.activeInHierarchy;

        // 2. 조건이 만족되었고, 아직 실행된 적이 없다면 -> 실행
        if (conditionMet && !_isTriggered)
        {
            ActivateEffect();
            _isTriggered = true; // 실행 완료 표시
            return;
        }
    }

    private void ActivateEffect()
    {
        if (_objectToHide != null) _objectToHide.SetActive(false); // A 끄기
        if (_objectToShow != null) _objectToShow.SetActive(true);  // B 켜기
        //Debug.Log("[DualTrigger] 조건 달성! A 숨김, B 표시");
    }

    public void LoadSceneByName()
    {
        SceneManager.LoadScene("Main");
    }

    private void ResetEffect()
    {
        if (_objectToHide != null) _objectToHide.SetActive(true);  // A 복구
        if (_objectToShow != null) _objectToShow.SetActive(false); // B 숨김
        Debug.Log("[DualTrigger] 조건 해제! 원상복구");
    }
}
