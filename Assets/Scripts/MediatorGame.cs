using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MediatorGame : MonoBehaviour
{
    public AudioMixer audioMixer;
    
    [SerializeField] private Camera _camera;
    [SerializeField] private Image _currentBackground;
    [SerializeField] private Sprite[] _backgroundSprites;
    [SerializeField] private SpriteRenderer _hero;
    [SerializeField] private Sprite[] _heroSprites;
    // Pause panel
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _pauseButton;
    [SerializeField] private Button _buttonHome;
    // Score panel
    [SerializeField] private TMP_Text _textScore;
    [SerializeField] private TMP_Text _textTopScore;
    [SerializeField] private TMP_Text _textStars;
    // Task slider
    [SerializeField] private Slider _sliderTask;
    [SerializeField] private TMP_Text _textTask;
    // Game over panel
    [SerializeField] private GameObject _gameOver;
    [SerializeField] private Button _buttonContinuePlay;
    [SerializeField] private Button _buttonBackToMenu;
    // Level completed panel
    [SerializeField] private GameObject _levelCompleted;
    [SerializeField] private Button _buttonContinueLevel;
    [SerializeField] private Button _buttonMainMenu;

    [SerializeField] private Image _arrowUp;
    
    private bool _isMenuShow;
    private int tempPoints = 0;
    private int points = 0;
    private int topPoints;
    public int stars;
    private int currentLevel;
    
    private int sliderMaxValue;
    

    private void Start()
    {
        Time.timeScale = 1;
        AudioListener.pause = false;
        
        float volume;
        if (PlayerPrefs.HasKey("Volume"))
        {
            volume = PlayerPrefs.GetFloat("Volume");
            if(volume == 0f) audioMixer.SetFloat("SoundVolume", -80f);
            else audioMixer.SetFloat("SoundVolume", Mathf.Log10(volume) * 30f);
        }
        else
        {
            PlayerPrefs.SetFloat("Volume", 0.5f);
            audioMixer.SetFloat("SoundVolume", Mathf.Log10(0.5f) * 30f);
        }
        
        _buttonContinuePlay.onClick.AddListener(ButtonContinuePlayOnClick);
        _buttonBackToMenu.onClick.AddListener(ButtonBackToMenuOnClick);
        _buttonContinueLevel.onClick.AddListener(ButtonContinueLevelOnClick);
        _buttonMainMenu.onClick.AddListener(ButtonMainMenuOnClick);
        _buttonHome.onClick.AddListener(ButtonHomeOnClick);

        if (PlayerPrefs.HasKey("TopScore")) topPoints = PlayerPrefs.GetInt("TopScore");
        else
        {
            topPoints = 0;
            PlayerPrefs.SetInt("TopScore", 0);
        }
        if (PlayerPrefs.HasKey("Stars")) stars = PlayerPrefs.GetInt("Stars");
        else
        {
            stars = 0;
            PlayerPrefs.SetInt("Stars", 0);
        }

        if (PlayerPrefs.HasKey("CurrentLevel"))
        {
            currentLevel = PlayerPrefs.GetInt("CurrentLevel");
        }
        else
        {
            currentLevel = 0;
            PlayerPrefs.SetInt("CurrentLevel", 0);
        }
        
        _currentBackground.sprite = _backgroundSprites[currentLevel];
        
        if (PlayerPrefs.HasKey("CurrentSkin"))
        {
            _hero.sprite = _heroSprites[PlayerPrefs.GetInt("CurrentSkin") + 1];    
        }
        else
        {
            _hero.sprite = _heroSprites[0];
        }
        
        
        ShowTask(currentLevel);
    }

    private void PrepareLevelToStart()
    {
        currentLevel++;
        points = 0;
        Player.PlayerScript.monsterAllCounter = 0;
        Player.PlayerScript.itemAllCounter = 0;
        Player.PlayerScript.itemSpringCounter = 0;
        Player.PlayerScript.itemTrampolineCounter = 0;
        Player.PlayerScript.itemCapPropellerCounter = 0;
        Player.PlayerScript.itemJetPackCounter = 0;
        Player.PlayerScript.itemStarCounter = 0;
        Player.PlayerScript.platformAllCounter = 0;
        Player.PlayerScript.platformStandartCounter = 0;
        Player.PlayerScript.platformFragileCounter = 0;
        Player.PlayerScript.platformCrazyCounter = 0;
        Player.PlayerScript.platformDisposableCounter = 0;
        Player.PlayerScript.platformDragCounter = 0;
        Player.PlayerScript.platformMobileCounter = 0;
        Player.PlayerScript.platformTeleportingCounter = 0;
        Player.PlayerScript.platformExplodingCounter = 0;
        _currentBackground.sprite = _backgroundSprites[currentLevel];
        ShowTask(currentLevel);
        _levelCompleted.SetActive(false);
        Time.timeScale = 1;
        AudioListener.pause = false;
        
        Player.PlayerScript.isFinish = false;
        PlatformSpawner.PlatformSpawnerScript.SpawnStartBlock();
        PlatformSpawner.PlatformSpawnerScript.isSpawnFinishBlock = false;
        Player.PlayerScript.currentItem = Player.CurrentItem.None;
        Player.PlayerScript.playerRigidbody.AddForce(Vector2.up * 10f, ForceMode2D.Impulse);
        Player.PlayerScript.isCanShot = true; // Разрешаем игроку стрелять
    }

    private void ButtonHomeOnClick()
    {
        SceneManager.LoadScene("Start");
    }
    
    private void ButtonContinueLevelOnClick()
    {
        switch (currentLevel)
        {
            case 1: PlayerPrefs.SetInt("Level2", 1);  PrepareLevelToStart(); break;
            case 2: PlayerPrefs.SetInt("Level3", 1);  PrepareLevelToStart(); break;
            case 3: PlayerPrefs.SetInt("Level4", 1);  PrepareLevelToStart(); break;
            case 4: PlayerPrefs.SetInt("Level5", 1);  PrepareLevelToStart(); break;
            case 5: PlayerPrefs.SetInt("Level6", 1);  PrepareLevelToStart(); break;
            case 6: PlayerPrefs.SetInt("Level7", 1);  PrepareLevelToStart(); break;
            case 7: PlayerPrefs.SetInt("Level8", 1);  PrepareLevelToStart(); break;
            case 8: SceneManager.LoadScene("Start"); break;
        }
    }

    private void ButtonMainMenuOnClick()
    {
        switch (currentLevel)
        {
            case 1: PlayerPrefs.SetInt("Level2", 1);  break;
            case 2: PlayerPrefs.SetInt("Level3", 1);  break;
            case 3: PlayerPrefs.SetInt("Level4", 1);  break;
            case 4: PlayerPrefs.SetInt("Level5", 1);  break;
            case 5: PlayerPrefs.SetInt("Level6", 1);  break;
            case 6: PlayerPrefs.SetInt("Level7", 1);  break;
            case 7: PlayerPrefs.SetInt("Level8", 1);  break;
        }
        SceneManager.LoadScene("Start");
    }
    
    public void ButtonContinuePlayOnClick()
    {
        if (stars > 10)
        {
            Player.PlayerScript.transform.gameObject.SetActive(true);
            Player.PlayerScript.isGameOver = false;
            Player.PlayerScript.isRestartLevel = true;
            _gameOver.SetActive(false);
            _pauseButton.SetActive(true);
            _sliderTask.gameObject.SetActive(true);
            stars -= 10;
            Player.PlayerScript.currentItem = Player.CurrentItem.Spring;
        }
    }

    private void ButtonBackToMenuOnClick()
    {
        Time.timeScale = 1;
        AudioListener.pause = false;
        SceneManager.LoadScene("Start");
    }
    
    private void ShowTask(int level)
    {
        switch (level)
        {
          case 0: _textTask.text = "Endless Mode"; sliderMaxValue = 1;  break;
          case 1: _textTask.text = "Get 50 points"; sliderMaxValue = 50; break;
          case 2: _textTask.text = "Use any 10 items"; sliderMaxValue = 10; break;
          case 3: _textTask.text = "Collect 12 stars"; sliderMaxValue = 12; break;
          case 4: _textTask.text = "Touch 5 disposable platforms"; sliderMaxValue = 5; break;
          case 5: _textTask.text = "Use 7 items spring"; sliderMaxValue = 7; break;
          case 6: _textTask.text = "Touch 6 fragile platforms"; sliderMaxValue = 6; break;
          case 7: _textTask.text = "Kill 5 monsters"; sliderMaxValue = 5; break;
          case 8: _textTask.text = "Touch 5 fragile exploding platforms"; sliderMaxValue = 5; break;
        }

        _sliderTask.maxValue = sliderMaxValue;
        _sliderTask.value = 0;
    }

    private void TaskControl()
    {
        if (_sliderTask.value == sliderMaxValue)
        {
            _arrowUp.gameObject.SetActive(true);
            // Если игрок достиг финиша
            if (Player.PlayerScript.isFinish)
            {
                _arrowUp.gameObject.SetActive(false);
                _levelCompleted.SetActive(true);
            }
            
        }
    }
    

    public void AddScores(int score)
    {
        
       tempPoints = (int)_camera.transform.position.y;
       
       if (points < tempPoints) points = tempPoints;
       if (points > topPoints) topPoints = points;
       
       _textScore.text = points + "";
       _textTopScore.text = topPoints + "";
       
       _textStars.text = stars.ToString();
       
       PlayerPrefs.SetInt("TopScore", topPoints);
       PlayerPrefs.SetInt("Stars", stars);
    }

    private void Update()
    {
        if (Player.PlayerScript.isShowGameOverMenu)
        {
            Player.PlayerScript.isShowGameOverMenu = false;
            _gameOver.SetActive(true);
            _pauseButton.SetActive(false);
            _sliderTask.gameObject.SetActive(false);
        }
     
        if(Player.PlayerScript.isGameOver) _sliderTask.gameObject.SetActive(false);
        
        switch (currentLevel)
        {
            case 0: _sliderTask.value = 0; break;
            case 1: _sliderTask.value = points; break;
            case 2: _sliderTask.value = Player.PlayerScript.itemAllCounter; break;
            case 3: _sliderTask.value = Player.PlayerScript.itemStarCounter; break;
            case 4: _sliderTask.value = Player.PlayerScript.platformDisposableCounter; break;
            case 5: _sliderTask.value = Player.PlayerScript.itemSpringCounter; break;
            case 6: _sliderTask.value = Player.PlayerScript.platformFragileCounter; break;
            case 7: _sliderTask.value = Player.PlayerScript.monsterAllCounter; break;
            case 8: _sliderTask.value = Player.PlayerScript.platformExplodingCounter; break;
        }
        
        AddScores(1);
        TaskControl();
    }

    public void Pause()
    {
        if (_isMenuShow)
        {
            AudioListener.pause = false;
            _isMenuShow = false;
            Time.timeScale = 1;
        }
        else
        {
            AudioListener.pause = true;
            _isMenuShow = true;
            Time.timeScale = 0;
        }

        if (_isMenuShow)
        {
            _pauseMenu.SetActive(true);
        }
        else
        {
            _pauseMenu.SetActive(false);
        }
    }
}
