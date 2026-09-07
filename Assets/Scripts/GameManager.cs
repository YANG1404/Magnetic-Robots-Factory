using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // 어디서든 점수를 올릴 수 있게 싱글톤 처리
    public static GameManager Instance { get; private set; }

    [Header("--- Game Settings ---")]
    [Tooltip("게임 제한 시간 (초 단위). 1분 30초 = 90")]
    [SerializeField] private float _gameDuration = 90f;

    private float _currentTime;
    private int _currentScore;
    private bool _isGameActive = false;

    [Header("--- References ---")]
    [Tooltip("로봇을 생성하는 스포너 오브젝트 (게임 끝나면 꺼짐)")]
    [SerializeField] private GameObject _robotSpawner;
    [SerializeField] private Slider _speedSlider;
    [SerializeField] private ConveyorBelt[] _conveyors;

    [Tooltip("게임 종료 시 띄울 UI 캔버스 오브젝트")]
    [SerializeField] private GameObject _gameOnUI;
    [SerializeField] private GameObject _gameOverUI;

    [Header("--- UI Elements (TextMeshPro) ---")]
    [SerializeField] private TextMeshProUGUI _timerText;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _finalScoreText; // 게임 오버 창에 뜰 최종 점수
    [SerializeField] private TextMeshProUGUI _SpeedText;

    [Header("--- 속도 범위 설정 ---")]
    [SerializeField] private float _minSpeed = 1.0f; // 슬라이더 0일 때 (최저 속도)
    [SerializeField] private float _maxSpeed = 8.0f;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        StartGame();
    }

    private void Update()
    {
        if (!_isGameActive) return;

        // 시간 감소
        _currentTime -= Time.deltaTime;

        // UI 업데이트 (매 프레임)
        UpdateTimerUI();
        UpdateSpeedUI();

        // 시간 종료 체크
        if (_currentTime <= 0)
        {
            EndGame();
        }
    }

    public void StartGame()
    {
        _currentTime = _gameDuration;
        _currentScore = 0;
        _isGameActive = true;

        // 스포너 켜기
        if (_robotSpawner != null) _robotSpawner.SetActive(true);

        // 게임 오버 UI 숨기기
        if (_gameOverUI != null) _gameOverUI.SetActive(false);

        if (_speedSlider != null)
        {
            _speedSlider.onValueChanged.AddListener(OnSliderChanged);
            OnSliderChanged(_speedSlider.value);
        }

        // UI 초기화
        UpdateScoreUI();
    }

    public void OnSliderChanged(float value)
    {
        float newSpeed = Mathf.Lerp(_minSpeed, _maxSpeed, value);

        // ⭐ 배열에 들어있는 모든 컨베이어 벨트에게 하나씩 명령 전달
        if (_conveyors != null)
        {
            foreach (ConveyorBelt belt in _conveyors)
            {
                if (belt != null)
                {
                    belt.SetSpeed(newSpeed);
                }
            }
        }
    }

    // 외부(로봇)에서 호출할 점수 추가 함수
    public void AddScore(int points)
    {
        if (!_isGameActive) return;

        _currentScore += points;
        UpdateScoreUI();
    }

    private void EndGame()
    {
        _isGameActive = false;
        _currentTime = 0;
        UpdateTimerUI(); // 00:00으로 맞춤

        Debug.Log("게임 종료!");

        // 1. 로봇 생성 중단
        if (_robotSpawner != null) _robotSpawner.SetActive(false);

        // 2. 게임 오버 UI 띄우기
        if (_gameOnUI != null) _gameOnUI.SetActive(false);
        if (_gameOverUI != null) _gameOverUI.SetActive(true);

        // 3. 최종 점수 표시
        if (_finalScoreText != null)
            _finalScoreText.text = $"Final Score: {_currentScore}";
    }

    // --- UI 업데이트 헬퍼 함수들 ---
    private void UpdateTimerUI()
    {
        if (_timerText != null)
        {
            // 분:초 포맷으로 변환 (예: 01:30)
            int minutes = Mathf.FloorToInt(_currentTime / 60F);
            int seconds = Mathf.FloorToInt(_currentTime % 60F);
            _timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    private void UpdateScoreUI()
    {
        if (_scoreText != null)
        {
            _scoreText.text = $"Score: {_currentScore}";
        }
    }

    private void UpdateSpeedUI()
    {
        if (_speedSlider != null)
        {
            _SpeedText.text = $"Speed: {Mathf.Lerp(_minSpeed, _maxSpeed, _speedSlider.value):0.0}";
            // 슬라이더 값에 따라 속도 텍스트 업데이트 (선택 사항)
            // 예: "Speed: 5.0"
            // 이 부분은 필요에 따라 구현하세요.
        }
    }




    public void Restart()
    {
        // 1. 현재 활성화된 씬(Scene) 정보를 가져옵니다.
        Scene currentScene = SceneManager.GetActiveScene();

        // 2. 해당 씬을 다시 로드합니다. (초기화와 동일한 효과)
        SceneManager.LoadScene(currentScene.buildIndex);
    }
}
