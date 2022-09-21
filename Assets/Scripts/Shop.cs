using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Shop : MonoBehaviour
{
  [SerializeField] private GameObject _shop;
  [SerializeField] private GameObject _mainScreen;
  
  [SerializeField] private TextMeshProUGUI _textStarCount;
  [SerializeField] private Button _buttonResume;
  
  [SerializeField] private Button _btnBuyGhost;
  [SerializeField] private Button _btnBuyDoodlestain;
  [SerializeField] private Button _btnBuyDiver;
  [SerializeField] private Button _btnBuySoccer;
  [SerializeField] private Button _btnBuyJungle;
  [SerializeField] private Button _btnBuyIce;
  [SerializeField] private Button _btnBuySpace;
  
  [SerializeField] private Button _btnChooseStandart;
  [SerializeField] private Button _btnChooseGhost;
  [SerializeField] private Button _btnChooseDoodlestain;
  [SerializeField] private Button _btnChooseDiver;
  [SerializeField] private Button _btnChooseSoccer;
  [SerializeField] private Button _btnChooseJungle;
  [SerializeField] private Button _btnChooseIce;
  [SerializeField] private Button _btnChooseSpace;
  

  private int stars;
  private int curentSkin;
  private void Start()
  {
    _buttonResume.onClick.AddListener(ButtonResumeOnClick);
      
    _btnBuyGhost.onClick.AddListener(BtnBuyGhostOnClick);
    _btnBuyDoodlestain.onClick.AddListener(BtnBuyDoodlestainOnClick);
    _btnBuyDiver.onClick.AddListener(BtnBuyDiverOnClick);
    _btnBuySoccer.onClick.AddListener(BtnBuySoccerOnClick);
    _btnBuyJungle.onClick.AddListener(BtnBuyJungleOnClick);
    _btnBuyIce.onClick.AddListener(BtnBuyIceOnClick);
    _btnBuySpace.onClick.AddListener(BtnBuySpaceOnClick);
    
    _btnChooseStandart.onClick.AddListener(BtnChooseStandartOnClick);
    _btnChooseGhost.onClick.AddListener(BtnChooseGhostOnClick);
    _btnChooseDoodlestain.onClick.AddListener(BtnChooseDoodlestainOnClick);
    _btnChooseDiver.onClick.AddListener(BtnChooseDiverOnClick);
    _btnChooseSoccer.onClick.AddListener(BtnChooseSoccerOnClick);
    _btnChooseJungle.onClick.AddListener(BtnChooseJungleOnClick);
    _btnChooseIce.onClick.AddListener(BtnChooseIceOnClick);
    _btnChooseSpace.onClick.AddListener(BtnChooseSpaceOnClick);
    
    UpdateShopInformation();
  }

  private void ButtonResumeOnClick()
  {
    _shop.SetActive(false);
    _mainScreen.SetActive(true);
  }
  
  private void BtnBuyGhostOnClick()
  {
    if (stars >= 50)
    {
      stars -= 50;
      PlayerPrefs.SetInt("Stars", stars);
      PlayerPrefs.SetInt("SkinGhost", 1);
      UpdateShopInformation();
    }
  }

  private void BtnBuyDoodlestainOnClick()
  {
    if (stars >= 50)
    {
      stars -= 50;
      PlayerPrefs.SetInt("Stars", stars);
      PlayerPrefs.SetInt("SkinDoodlestain", 1);
      UpdateShopInformation();
    }
  }

  private void BtnBuyDiverOnClick()
  {
    if (stars >= 50)
    {
      stars -= 50;
      PlayerPrefs.SetInt("Stars", stars);
      PlayerPrefs.SetInt("SkinDiver", 1);
      UpdateShopInformation();
    }
  }

  private void BtnBuySoccerOnClick()
  {
    if (stars >= 50)
    {
      stars -= 50;
      PlayerPrefs.SetInt("Stars", stars);
      PlayerPrefs.SetInt("SkinSoccer", 1);
      UpdateShopInformation();
    }
  }

  private void BtnBuyJungleOnClick()
  {
    if (stars >= 50)
    {
      stars -= 50;
      PlayerPrefs.SetInt("Stars", stars);
      PlayerPrefs.SetInt("SkinJungle", 1);
      UpdateShopInformation();
    }
  }

  private void BtnBuyIceOnClick()
  {
    if (stars >= 50)
    {
      stars -= 50;
      PlayerPrefs.SetInt("Stars", stars);
      PlayerPrefs.SetInt("SkinIce", 1);
      UpdateShopInformation();
    }
  }

  private void BtnBuySpaceOnClick()
  {
    if (stars >= 50)
    {
      stars -= 50;
      PlayerPrefs.SetInt("Stars", stars);
      PlayerPrefs.SetInt("SkinSpace", 1);
      UpdateShopInformation();
    }
  }

  private void BtnChooseStandartOnClick()
  {
    PlayerPrefs.SetInt("CurrentSkin", 0);
    _shop.SetActive(false);
    _mainScreen.SetActive(true);
  }

  private void BtnChooseGhostOnClick()
  {
    PlayerPrefs.SetInt("CurrentSkin", 1);
    _shop.SetActive(false);
    _mainScreen.SetActive(true);
  }

  private void BtnChooseDoodlestainOnClick()
  {
    PlayerPrefs.SetInt("CurrentSkin", 2);
    _shop.SetActive(false);
    _mainScreen.SetActive(true);
  }
  
  private void BtnChooseDiverOnClick()
  {
    PlayerPrefs.SetInt("CurrentSkin", 3);
    _shop.SetActive(false);
    _mainScreen.SetActive(true);
  }

  private void BtnChooseSoccerOnClick()
  {
    PlayerPrefs.SetInt("CurrentSkin", 4);
    _shop.SetActive(false);
    _mainScreen.SetActive(true);
  }

  private void BtnChooseJungleOnClick()
  {
    PlayerPrefs.SetInt("CurrentSkin", 5);
    _shop.SetActive(false);
    _mainScreen.SetActive(true);
  }

  private void BtnChooseIceOnClick()
  {
    PlayerPrefs.SetInt("CurrentSkin", 6);
    _shop.SetActive(false);
    _mainScreen.SetActive(true);
  }

  private void BtnChooseSpaceOnClick()
  {
    PlayerPrefs.SetInt("CurrentSkin", 7);
    _shop.SetActive(false);
    _mainScreen.SetActive(true);
  }


  private void UpdateShopInformation()
  {
    if (PlayerPrefs.HasKey("Stars")) stars = PlayerPrefs.GetInt("Stars");
    else stars = 0;
    _textStarCount.text = stars.ToString();

    if (PlayerPrefs.HasKey("CurrentSkin")) curentSkin = PlayerPrefs.GetInt("CurrentSkin");
    else
    {
      curentSkin = 0;
      PlayerPrefs.SetInt("CurrentSkin", 0);
    }
    
    // Skin "Ghost"
    if (PlayerPrefs.HasKey("SkinGhost")) 
    {
      if (PlayerPrefs.GetInt("SkinGhost") == 1)
      {
        _btnBuyGhost.enabled = false;  _btnBuyGhost.gameObject.SetActive(false);
        _btnChooseGhost.enabled = true;  _btnChooseGhost.gameObject.SetActive(true);
      }
      else
      {
        _btnBuyGhost.enabled = true; _btnBuyGhost.gameObject.SetActive(true);
        _btnChooseGhost.enabled = false; _btnChooseGhost.gameObject.SetActive(false);
      }
    }
    else
    {
      PlayerPrefs.SetInt("SkinGhost", 0);
      _btnBuyGhost.enabled = true; _btnBuyGhost.gameObject.SetActive(true);
      _btnChooseGhost.enabled = false; _btnChooseGhost.gameObject.SetActive(false);
    }
    
    // Skin "Doodlestain"
    if (PlayerPrefs.HasKey("SkinDoodlestain")) 
    {
      if (PlayerPrefs.GetInt("SkinDoodlestain") == 1)
      {
        _btnBuyDoodlestain.enabled = false; _btnBuyDoodlestain.gameObject.SetActive(false);
        _btnChooseDoodlestain.enabled = true; _btnChooseDoodlestain.gameObject.SetActive(true);
      }
      else
      {
        _btnBuyDoodlestain.enabled = true; _btnBuyDoodlestain.gameObject.SetActive(true);
        _btnChooseDoodlestain.enabled = false; _btnChooseDoodlestain.gameObject.SetActive(false);
      }
    }
    else
    {
      PlayerPrefs.SetInt("SkinDoodlestain", 0);
      _btnBuyDoodlestain.enabled = true; _btnBuyDoodlestain.gameObject.SetActive(true);
      _btnChooseDoodlestain.enabled = false; _btnChooseDoodlestain.gameObject.SetActive(false);
    }
    
    // Skin "Diver"
    if (PlayerPrefs.HasKey("SkinDiver")) 
    {
      if (PlayerPrefs.GetInt("SkinDiver") == 1)
      {
        _btnBuyDiver.enabled = false; _btnBuyDiver.gameObject.SetActive(false);
        _btnChooseDiver.enabled = true; _btnChooseDiver.gameObject.SetActive(true);
      }
      else
      {
        _btnBuyDiver.enabled = true; _btnBuyDiver.gameObject.SetActive(true);
        _btnChooseDiver.enabled = false; _btnChooseDiver.gameObject.SetActive(false);
      }
    }
    else
    {
      PlayerPrefs.SetInt("SkinDiver", 0);
      _btnBuyDiver.enabled = true; _btnBuyDiver.gameObject.SetActive(true);
      _btnChooseDiver.enabled = false; _btnChooseDiver.gameObject.SetActive(false);
    }
    
    // Skin "Soccer"
    if (PlayerPrefs.HasKey("SkinSoccer")) 
    {
      if (PlayerPrefs.GetInt("SkinSoccer") == 1)
      {
        _btnBuySoccer.enabled = false; _btnBuySoccer.gameObject.SetActive(false);
        _btnChooseSoccer.enabled = true; _btnChooseSoccer.gameObject.SetActive(true);
      }
      else
      {
        _btnBuySoccer.enabled = true; _btnBuySoccer.gameObject.SetActive(true);
        _btnChooseSoccer.enabled = false; _btnChooseSoccer.gameObject.SetActive(false);
      }
    }
    else
    {
      PlayerPrefs.SetInt("SkinSoccer", 0);
      _btnBuySoccer.enabled = true; _btnBuySoccer.gameObject.SetActive(true);
      _btnChooseSoccer.enabled = false; _btnChooseSoccer.gameObject.SetActive(false);
    }
    
    // Skin "Jungle"
    if (PlayerPrefs.HasKey("SkinJungle")) 
    {
      if (PlayerPrefs.GetInt("SkinJungle") == 1)
      {
        _btnBuyJungle.enabled = false; _btnBuyJungle.gameObject.SetActive(false);
        _btnChooseJungle.enabled = true; _btnChooseJungle.gameObject.SetActive(true);
      }
      else
      {
        _btnBuyJungle.enabled = true; _btnBuyJungle.gameObject.SetActive(true);
        _btnChooseJungle.enabled = false; _btnChooseJungle.gameObject.SetActive(false);
      }
    }
    else
    {
      PlayerPrefs.SetInt("SkinJungle", 0);
      _btnBuyJungle.enabled = true; _btnBuyJungle.gameObject.SetActive(true);
      _btnChooseJungle.enabled = false; _btnChooseJungle.gameObject.SetActive(false);
    }
    
    // Skin "Ice"
    if (PlayerPrefs.HasKey("SkinIce")) 
    {
      if (PlayerPrefs.GetInt("SkinIce") == 1)
      {
        _btnBuyIce.enabled = false; _btnBuyIce.gameObject.SetActive(false);
        _btnChooseIce.enabled = true; _btnChooseIce.gameObject.SetActive(true);
      }
      else
      {
        _btnBuyIce.enabled = true; _btnBuyIce.gameObject.SetActive(true);
        _btnChooseIce.enabled = false; _btnChooseIce.gameObject.SetActive(false);
      }
    }
    else
    {
      PlayerPrefs.SetInt("SkinIce", 0);
      _btnBuyIce.enabled = true; _btnBuyIce.gameObject.SetActive(true);
      _btnChooseIce.enabled = false; _btnChooseIce.gameObject.SetActive(false);
    }
    
    // Skin "Space"
    if (PlayerPrefs.HasKey("SkinSpace")) 
    {
      if (PlayerPrefs.GetInt("SkinSpace") == 1)
      {
        _btnBuySpace.enabled = false; _btnBuySpace.gameObject.SetActive(false);
        _btnChooseSpace.enabled = true; _btnChooseSpace.gameObject.SetActive(true);
      }
      else
      {
        _btnBuySpace.enabled = true; _btnBuySpace.gameObject.SetActive(true);
        _btnChooseSpace.enabled = false; _btnChooseSpace.gameObject.SetActive(false);
      }
    }
    else
    {
      PlayerPrefs.SetInt("SkinSpace", 0);
      _btnBuySpace.enabled = true; _btnBuySpace.gameObject.SetActive(true);
      _btnChooseSpace.enabled = false; _btnChooseSpace.gameObject.SetActive(false);
    }
  }
}
