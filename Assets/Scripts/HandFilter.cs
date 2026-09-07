using UnityEngine;
using Oculus.Interaction;
using Oculus.Interaction.Input; // IHand를 쓰기 위해 필요

public class HandFilter : MonoBehaviour, IGameObjectFilter
{
    public enum HandSide { Left, Right }

    [Header("허용할 손 (Inspector에서 설정)")]
    public HandSide allowedHand;

    public bool Filter(GameObject interactorGameObject)
    {
        // 1. 가장 정확한 방법: ISDK의 'IHand' 컴포넌트를 부모에서 찾기
        // (이게 있으면 확실하게 왼손/오른손 구분이 됨)
        IHand hand = interactorGameObject.GetComponentInParent<IHand>();
        if (hand != null)
        {
            if (allowedHand == HandSide.Left) return hand.Handedness == Handedness.Left;
            else return hand.Handedness == Handedness.Right;
        }

        // 2. 차선책: 이름으로 확인 (부모의 부모의 부모... 끝까지 검색)
        // IHand 컴포넌트가 없는 구조일 경우를 대비해 이름을 끝까지 추적합니다.
        Transform t = interactorGameObject.transform;
        while (t != null)
        {
            string name = t.name;
            // 이름에 Left가 포함되면 -> 왼손으로 판별
            if (name.Contains("Left") || name.Contains("left"))
            {
                return allowedHand == HandSide.Left;
            }
            // 이름에 Right가 포함되면 -> 오른손으로 판별
            if (name.Contains("Right") || name.Contains("right"))
            {
                return allowedHand == HandSide.Right;
            }

            // 부모로 이동해서 계속 검사
            t = t.parent;
        }

        // 3. 만약 끝까지 갔는데도 Left/Right를 못 찾았다면?
        // 보통 이런 경우는 없지만, 안전하게 false(차단) 혹은 true(허용) 처리
        // 여기서는 "확실하지 않으면 차단" 하겠습니다.
        return false;
    }
}
