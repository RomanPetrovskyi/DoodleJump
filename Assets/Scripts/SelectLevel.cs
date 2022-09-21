using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class SelectLevel : MonoBehaviour
{
    [SerializeField] private Button _buttonStartLevel;
    [SerializeField] private Image _imageLock;
    [SerializeField] private Image _imageUnLock;
    [SerializeField] private TextMeshProUGUI _textLevel;
    [SerializeField] private int _levelNumber;
    void Start()
    {
        _buttonStartLevel.onClick.AddListener(ButtonStartLevelOnClick);
        
        PlayerPrefs.SetInt("Level1", 1);
        
        _textLevel.text = "Level " + _levelNumber.ToString();
        String playerPrefKey = "Level" + _levelNumber.ToString();
        
        if (PlayerPrefs.HasKey(playerPrefKey))
        {
            if (PlayerPrefs.GetInt(playerPrefKey) == 1)
            {
                _imageLock.enabled = false;
                _imageUnLock.enabled = true;
            }
            else
            {
                _imageLock.enabled = true;
                _imageUnLock.enabled = false;
                _buttonStartLevel.enabled = false;
            }
        }
        else
        {
            PlayerPrefs.SetInt("playerPrefKey", 0);
            _imageLock.enabled = true;
            _imageUnLock.enabled = false;
            _buttonStartLevel.enabled = false;
        }
        
    }

    private void ButtonStartLevelOnClick()
    {
        PlayerPrefs.SetInt("CurrentLevel", _levelNumber);
        SceneManager.LoadScene("Game");
    }
}
