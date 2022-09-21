using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
public class Options : MonoBehaviour
{
    [SerializeField] private GameObject _scene;
    [SerializeField] private Image _imageTitle;
    [SerializeField] private Image _imageSoundOn;
    [SerializeField] private Image _imageSoundOff;
    [SerializeField] private Slider _sliderVolume;
    [SerializeField] private Image _imageVibroOn;
    [SerializeField] private Image _imageVibroOff;
    [SerializeField] private Toggle _toggleVibro;
    [SerializeField] private Button _buttonResume;

    private AudioSource _audioSource;
    private void Start()
    {
        _scene.SetActive(false);

        if (PlayerPrefs.HasKey("Volume")) _sliderVolume.value = PlayerPrefs.GetFloat("Volume");
        else
        {
            _sliderVolume.value = 0.5f;
            PlayerPrefs.SetFloat("Volume", _sliderVolume.value);
        }

        if (PlayerPrefs.HasKey("Vibro"))
        {
            int flagVibro = PlayerPrefs.GetInt("Vibro");
            if (flagVibro == 1) _toggleVibro.isOn = true;
            else _toggleVibro.isOn = false;
        }
        else
        {
            _toggleVibro.isOn = true;
            PlayerPrefs.SetInt("Vibro", 1);
        }
        
        var sq = DOTween.Sequence();
        sq.Append(_imageTitle.transform.DOScale(1.2f, 10f));
        sq.Append(_imageTitle.transform.DOScale(1f, 10f));
        sq.SetLoops(-1);
        
        _buttonResume.onClick.AddListener(ButtonResumeOnClick);
        _audioSource = Camera.main.GetComponent<AudioSource>();
        
        _sliderVolume.onValueChanged.AddListener(SliderValueOnChanged);
        _audioSource.volume = _sliderVolume.value;
        if (_audioSource.volume == 0)
        {
            _imageSoundOn.enabled = false;
            _imageSoundOff.enabled = true;
        }
        else
        {
            _imageSoundOn.enabled = true;
            _imageSoundOff.enabled = false;
        }
        
        _toggleVibro.onValueChanged.AddListener(ToggleVibroOnChange);
        if (_toggleVibro.isOn)
        {
            _imageVibroOn.enabled = true;
            _imageVibroOff.enabled = false;
        }
        else
        {
            _imageVibroOn.enabled = false;
            _imageVibroOff.enabled = true;
        }
    }

    private void SliderValueOnChanged(float value)
    {
         _audioSource.volume = value;
         if (value == 0)
         {
             _imageSoundOn.enabled = false;
             _imageSoundOff.enabled = true;
         }
         else
         {
             _imageSoundOn.enabled = true;
             _imageSoundOff.enabled = false;
         }
    }

    private void ToggleVibroOnChange(bool isActive)
    {
        if (isActive)
        {
            _imageVibroOn.enabled = true;
            _imageVibroOff.enabled = false;
            Handheld.Vibrate();
        }
        else
        {
            _imageVibroOn.enabled = false;
            _imageVibroOff.enabled = true;
        }
    }
    
    private void ButtonResumeOnClick()
    {
        gameObject.SetActive(false);
        _scene.SetActive(true);

        PlayerPrefs.SetFloat("Volume", _sliderVolume.value);
        if(_toggleVibro.isOn) PlayerPrefs.SetInt("Vibro", 1);
        else PlayerPrefs.SetInt("Vibro", 0);
    }
}
