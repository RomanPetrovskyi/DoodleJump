using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class StartSceneScript : MonoBehaviour
{
    [SerializeField] private GameObject _startScene;
    [SerializeField] private GameObject _options;
    [SerializeField] private GameObject _challenge;
    [SerializeField] private GameObject _exit;
    [SerializeField] private GameObject _shop;
    
    [SerializeField] private Image imageGameName;
    [SerializeField] private GameObject hero;
    [SerializeField] private GameObject platform;
    [SerializeField] private GameObject ufo;
    [SerializeField] private Image monster;
    
    [SerializeField] private Button buttonPlay;
    [SerializeField] private Button buttonChallenge;
    [SerializeField] private Button buttonOptions;
    
    [SerializeField] private Button buttonExit;
    [SerializeField] private Button buttonExitYes;
    [SerializeField] private Button buttonExitNo;
    
    [SerializeField] private Button buttonShop;
    [SerializeField] private Button _buttomResumeChallenge;

    [SerializeField] private Sprite[] _heroSkin;
    
    private void Start()
    {
        if (PlayerPrefs.HasKey("Volume"))
        {
            Camera.main.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("Volume");
        }
        else
        {
            PlayerPrefs.SetFloat("Volume", 0.5f);
            Camera.main.GetComponent<AudioSource>().volume = 0.5f;
        }

        if (!PlayerPrefs.HasKey("Vibro"))
        {
            PlayerPrefs.SetInt("Vibro", 1);
        }
        

        buttonPlay.onClick.AddListener(ButtonPlayOnClick);
        buttonOptions.onClick.AddListener(ButtonOptionsOnClick);
        buttonChallenge.onClick.AddListener(ButtonChallengeOnClick);
        
        buttonExit.onClick.AddListener(ButtonExitOnClick);
        buttonExitYes.onClick.AddListener(ButtonExitYesOnClick);
        buttonExitNo.onClick.AddListener(ButtonExitNoOnClick);
        
        buttonShop.onClick.AddListener(ButtonShopOnClick);
        _buttomResumeChallenge.onClick.AddListener(ButtomResumeChallengeOnClick);

        ButtonShopAnim();
        TitleAnim();
        HeroAnim();
        UfoAnim();
        MonsterAnim();
        UpdateSkin();
    }

    private void Update()
    {
        UpdateSkin();
    }

    private void UpdateSkin()
    {
        if (PlayerPrefs.HasKey("CurrentSkin"))
        {
            hero.GetComponent<SpriteRenderer>().sprite = _heroSkin[PlayerPrefs.GetInt("CurrentSkin")];
        }
        else
        {
            hero.GetComponent<SpriteRenderer>().sprite = _heroSkin[0];
        }
    }

    private void ButtomResumeChallengeOnClick()
    {
        _startScene.SetActive(true);
        _challenge.SetActive(false);
    }
    
    private void ButtonShopAnim()
    {
        Sequence shopSq = DOTween.Sequence();
        shopSq.Append(buttonShop.transform.DORotate(new Vector3(0, 0, 10), 0.1f));
        shopSq.Append(buttonShop.transform.DORotate(new Vector3(0, 0, 0), 0.1f));
        shopSq.Append(buttonShop.transform.DORotate(new Vector3(0, 0, -10), 0.1f));
        shopSq.Append(buttonShop.transform.DORotate(new Vector3(0, 0, 0), 0.1f));
        shopSq.SetLoops(-1);
    }
    private void TitleAnim()
    {
        Sequence sqGameName = DOTween.Sequence();
        sqGameName.Append(imageGameName.transform.DOScale(1.2f, 5f));
        sqGameName.Append(imageGameName.transform.DOScale(1f, 5f));
        sqGameName.SetLoops(-1);
    }

    private void HeroAnim()
    {
        Vector3 startPos = new Vector3(-1.5f, -3, 0);
        Vector3 endPos = new Vector3(-1.5f, -3, 0);
        hero.transform.position = startPos;
        Sequence sqHero = DOTween.Sequence();
        sqHero.Append(hero.transform.DOJump(endPos, 1.5f, 1, 0.8f));
        sqHero.Join(platform.transform.DOMove(
            new Vector3(platform.transform.position.x, platform.transform.position.y + 0.1f, 0), 0.1f));
        sqHero.Join(hero.transform.DORotate(new Vector3(0, 0, -360), 0.7f, RotateMode.FastBeyond360));
        sqHero.SetLoops(-1);
    }

    private void UfoAnim()
    {
        Vector3[] ufoPos = new Vector3[5];
        
        ufoPos[0] =  new Vector3(1f, 3f, 0);
        ufoPos[1] = new Vector3(1f, 2f, 0);
        ufoPos[2] = new Vector3(-1f, 3f, 0);
        ufoPos[3] = new Vector3(-1f, 2f, 0);
        ufoPos[4] = new Vector3(0f, 2.5f, 0);

        Sequence sqUfo = DOTween.Sequence();
        sqUfo.Append(ufo.transform.DOMove(ufoPos[0], 10f));
        sqUfo.Append(ufo.transform.DOMove(ufoPos[1], 10f));
        sqUfo.Append(ufo.transform.DOMove(ufoPos[2], 10f));
        sqUfo.Append(ufo.transform.DOMove(ufoPos[3], 10f));
        sqUfo.Append(ufo.transform.DOMove(ufoPos[4], 10f));
        sqUfo.SetLoops(-1);
    }

    private void MonsterAnim()
    {

        Sequence sqMonster = DOTween.Sequence();
        sqMonster.Append(monster.DOColor(new Color(0.5f,0.5f,0.5f), 21f));
        sqMonster.Append(monster.DOColor(Color.black, 7f));
        sqMonster.SetLoops(-1);
    }
    
    private void ButtonPlayOnClick()
    {
        PlayerPrefs.SetInt("CurrentLevel", 0);
        SceneManager.LoadScene("Game");
    }
    
    private void ButtonChallengeOnClick()
    {
        _startScene.SetActive(false);
        _challenge.SetActive(true);
    }
    
    private void ButtonOptionsOnClick()
    {
        _startScene.SetActive(false);
        _options.SetActive(true);
    }

    // Меню выход
    private void ButtonExitOnClick()
    {
        _startScene.SetActive(false);
        _exit.SetActive(true);
    }

    private void ButtonExitYesOnClick()
    {
        Application.Quit();
    }

    private void ButtonExitNoOnClick()
    {
        _exit.SetActive(false);
        _startScene.SetActive(true);
    }
    
    private void ButtonShopOnClick()
    {
        _startScene.SetActive(false);
        _shop.SetActive(true);
    }
}
