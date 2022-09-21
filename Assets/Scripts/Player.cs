using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using Sequence = Unity.VisualScripting.Sequence;

public class Player : MonoBehaviour
{
    [SerializeField] private Sprite[] _doodleShot;
    [SerializeField] private Sprite[] _doodleShotNose;
    [SerializeField] private GameObject ground;
    [SerializeField] private Transform _shotDir;
    [SerializeField] private Transform _pause;
    
    // Переменная нужна для запрета стрельбы при работе с кнопками UI
    public bool isCanShot = true;
    
    // Bulled
    [SerializeField] private GameObject _bullet;
    private GameObject _tempBullet;

    public static Player PlayerScript;
    public Rigidbody2D playerRigidbody;
    private SpriteRenderer playerSpriteRenderer;
    private SpriteRenderer playerNouseSpriteRenderer;

    private float horizontal;
    private float xMin;
    private float xMax;
    private Vector2 cameraMin;
    private Vector2 cameraMax;
    
    public bool isGameOver;
    public bool isShowGameOverMenu = false;
    public bool isRestartLevel;
    public bool isFinish = false;
    
    private GameObject tempGround;
    private GameObject tempNullZone;

    public bool PlayerCanMoveX = true;
    
    // MonsterCounter
    public int monsterAllCounter = 0;
    
    // Items counter
    public int itemAllCounter = 0;
    public int itemSpringCounter = 0;
    public int itemTrampolineCounter = 0;
    public int itemCapPropellerCounter = 0;
    public int itemJetPackCounter = 0;
    public int itemStarCounter = 0;
    
    // Platforms counter
    public int platformAllCounter = 0;
    public int platformStandartCounter = 0;
    public int platformFragileCounter = 0;
    public int platformCrazyCounter = 0;
    public int platformDisposableCounter = 0;
    public int platformDragCounter = 0;
    public int platformMobileCounter = 0;
    public int platformTeleportingCounter = 0;
    public int platformExplodingCounter = 0;

    private Camera _camera;
    public Vector3 gameOverCameraPosition;
    private float deltaY;
    private GameObject tempBullet;
    public AudioSource _audioSource;
    private Transform shotDir;
    public bool isDragPlatform = false;
    private GameObject _nouse;
    private Sprite _currentPlayerSkin;

    // Перечисление итемов, один из которых мы будем использовать в данный момент
    public enum  CurrentItem
    {
        None,
        Spring,
        Trampoline,
        Cap,
        JetPack
    } 
    public CurrentItem currentItem;
    
    private void Start()
    {
        // Получение ссылок на компоненты игрока
        _audioSource = GetComponent<AudioSource>();
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerSpriteRenderer = GetComponent<SpriteRenderer>();
        playerNouseSpriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        // В момент старта мы не используем никакой итем
        currentItem = CurrentItem.None;
        // Определяем и запоминаем координаты крайней левой и правой точек экрана
        _camera = Camera.main;
        cameraMin = _camera.ViewportToWorldPoint (new Vector2 (0,0)); // Нижний левый угол
        cameraMax = _camera.ViewportToWorldPoint (new Vector2 (1,1)); // Верзний правый угол
        xMin = cameraMin.x;
        xMax = cameraMax.x;
        // Получение ссылки на нос персонажа
        _nouse = transform.GetChild(0).gameObject;
        
        _currentPlayerSkin = playerSpriteRenderer.sprite;
        
        isRestartLevel = false;
        if (PlayerScript == null) PlayerScript = this;
    }

    public void Update()
    {
        itemAllCounter = itemSpringCounter + itemTrampolineCounter + itemCapPropellerCounter + itemJetPackCounter;
        
        if (isGameOver)
        {
            _camera.transform.position = new Vector3(_camera.transform.position.x,
                transform.position.y + deltaY, 
                _camera.transform.position.z);
        }

        if (isRestartLevel)
        {
            isRestartLevel = false;
            Destroy(tempGround);
            _camera.transform.position = gameOverCameraPosition;
            transform.position = new Vector3(0, _camera.transform.position.y, transform.position.z);
            playerRigidbody.AddForce(Vector2.up * 8f, ForceMode2D.Impulse);
            currentItem = CurrentItem.Spring;
            tempNullZone.gameObject.GetComponent<BoxCollider2D>().enabled = true;
        }

        if(isCanShot) Shot();
    }

    private void FixedUpdate()
    {
        if (Application.platform == RuntimePlatform.Android && PlayerCanMoveX)
        {
            horizontal = Input.acceleration.x;
            playerRigidbody.velocity = new Vector2(Input.acceleration.x * 10f, playerRigidbody.velocity.y);
        }
        else if (Application.isEditor && PlayerCanMoveX)
        {
            horizontal = Input.GetAxis("Horizontal");
            playerRigidbody.velocity = new Vector2(Input.GetAxis("Horizontal") * 10f, playerRigidbody.velocity.y);
        }

        HorizontalOutOfBoundsMonitor();
    }

    
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name == "NullZone")
        {
            _audioSource.Play();
            // Запоминаем поизицию камеры при проигрыше для следующего возврата за монеты 
            gameOverCameraPosition = Camera.main.transform.position;
            
            // Создаём землю, на которую упадёт герой
            tempGround = Instantiate(ground, new Vector3(0, transform.position.y - 10f, 1), Quaternion.identity);
            
            // Отключаем коллайдер у нулевой зоны
            tempNullZone = col.gameObject;
            tempNullZone.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            
            deltaY = _camera.transform.position.y - transform.position.y;
            isGameOver = true;
            isRestartLevel = false;
            
        }

        if (col.gameObject.tag == "GameOver")
        {
            if(PlayerPrefs.GetInt("Vibro") == 1) Handheld.Vibrate();
            tempGround.transform.GetChild(1).GetComponent<ParticleSystem>().Play();
            this.gameObject.SetActive(false);
            isGameOver = false;
            isShowGameOverMenu = true;
            tempGround.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
    
    private void HorizontalOutOfBoundsMonitor()
    {
        if (horizontal > 0f) playerSpriteRenderer.flipX = true;
        else if (horizontal < 0f) playerSpriteRenderer.flipX = false;
        
        if (transform.position.x < xMin)
            transform.position = new Vector3(xMax, transform.position.y, transform.position.z);
        
        if (transform.position.x > xMax)
            transform.position = new Vector3(xMin, transform.position.y, transform.position.z);
    }

    private bool isShot = false;

    // ---------- Метод, реализующий выстрел ----------
    private void Shot()
    {
        if (Input.GetButtonDown("Fire1") && currentItem == CurrentItem.None && !isDragPlatform)
        {
            // Определяем позицию нажатия
            Vector3 endPosition = _camera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            // Смещаем позицию Z, чтобы в последствии создать снаряд на переднем слое относительно игрового мира
            endPosition.z = -2f;
            // Исключаем из обработки нажатий по экрану кнопку паузы
            if (_camera.ScreenToViewportPoint(Input.mousePosition).y > 0.92 && _camera.ScreenToViewportPoint(Input.mousePosition).x > 0.85)
            {
                //Debug.Log("Pause zone");
            }
            else
            {
                if (_camera.ScreenToWorldPoint(Input.mousePosition).y > transform.position.y)
                {
                    // Запоминаем, какой у персонажа был скин до выстрела и меняем его на соответствующий скин при выстреле
                    if(!isShot) _currentPlayerSkin = playerSpriteRenderer.sprite;
                    isShot = true;
                    playerSpriteRenderer.sprite = _doodleShot[PlayerPrefs.GetInt("CurrentSkin")];
                    // Показываем нос и разворачиваем его в сторону выстрела
                    playerNouseSpriteRenderer.sprite = _doodleShotNose[PlayerPrefs.GetInt("CurrentSkin")];
                    _nouse.SetActive(true);
                    var angle = Mathf.Atan2(endPosition.y, endPosition.x) * Mathf.Rad2Deg;
                    _nouse.transform.rotation = Quaternion.Euler(0, 0, angle - 90);
                    // Создаём пулю c учётом разварота носа
                    _tempBullet = Instantiate(_bullet, _shotDir.position, _nouse.transform.rotation);
                    // Запускаем карутину для автоматического возврата носа в исходное положение после выстрела
                    StartCoroutine(ResetAfterShort());
                }
            }
        }
    }

    // ---------- Карутина для автоматического возврата носа в исходное положение после выстрела ----------
    IEnumerator ResetAfterShort()
    {
        yield return new WaitForSeconds(0.3f);
        _nouse.SetActive(false);
        playerSpriteRenderer.sprite = _currentPlayerSkin;
        isShot = false;
    }
}
