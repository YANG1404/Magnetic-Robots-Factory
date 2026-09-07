using Oculus.Interaction;
using Oculus.Interaction.HandGrab;
using UnityEngine;

public class ChangeManager : MonoBehaviour
{
    // 싱글톤 (선택 사항이지만 편의를 위해 유지)
    public static ChangeManager Instance;

    [Header("--- Player Hands ---")]
    [SerializeField] private GrabInteractor _leftHandInteractor;
    [SerializeField] private GrabInteractor _rightHandInteractor;

    [Header("--- Visuals & Anchors ---")]
    [SerializeField] private GameObject _leftHandVisual;
    [SerializeField] private GameObject _rightHandVisual;
    [SerializeField] private GameObject _leftMagneticVisual;
    [SerializeField] private GameObject _rightMagneticVisual;

    [Header("--- Game Object ---")]
    [SerializeField] private GameObject leftMagnetic;
    [SerializeField] private GameObject rightMagnetic;

    private void Awake()
    {
        _leftHandVisual.SetActive(true);
        _leftMagneticVisual.SetActive(false);
        _rightHandVisual.SetActive(true);
        _rightMagneticVisual.SetActive(false);
        Instance = this;
    }

    // ⭐ InteractableUnityEventWrapper에서 호출할 함수 ⭐
    // 매개변수로 잡힌 물체(GrabInteractable)를 직접 받습니다.
    public void OnLeftGrabbed(GrabInteractable grabbedObject)
    {
        // 1. 누가 잡았는지 확인 (SelectingInteractors 리스트 확인)
        foreach (var interactor in grabbedObject.SelectingInteractors)
        {
            if (IsSameInteractor(interactor, _leftHandInteractor))
            {
                _leftHandVisual.SetActive(false);
                _leftMagneticVisual.SetActive(true);
                leftMagnetic.SetActive(false);
                return;
            }
        }
    }

    public void OnRightGrabbed(GrabInteractable grabbedObject)
    {
        // 1. 누가 잡았는지 확인 (SelectingInteractors 리스트 확인)
        foreach (var interactor in grabbedObject.SelectingInteractors)
        {
            if (IsSameInteractor(interactor, _rightHandInteractor))
            {
                _rightHandVisual.SetActive(false);
                _rightMagneticVisual.SetActive(true);
                rightMagnetic.SetActive(false);
                return;
            }
        }
    }


    // 헬퍼: 인터랙터 비교
    private bool IsSameInteractor(IInteractorView input, GrabInteractor target)
    {
        return input == target || (input as MonoBehaviour).gameObject == target.gameObject;
    }
}
