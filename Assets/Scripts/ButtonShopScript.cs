using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ButtonShopScript : MonoBehaviour
{
    [SerializeField] private GameObject ShopPanel;
    private bool _onOff;
    [SerializeField] private Button _shopButton;

    public void Shop()
    {
        _onOff = !_onOff;
        if (_onOff)
        {
            ScaleShopOn();
        }
        else
        {
            ScaleShopOff();
        }
    }
    private void ScaleShopOn()
    {
        var sq = DOTween.Sequence();
        sq.AppendCallback(OnShop);
        sq.Append(ShopPanel.transform.DOScale(1f, 1f));
       
    }

    private void ScaleShopOff()
    {
        var sq = DOTween.Sequence();
        sq.Append(ShopPanel.transform.DOScale(0f, 1f));
        sq.AppendCallback(OffShop);
    }
    private void OnShop()
    {
        ShopPanel.SetActive(true);
    }
    private void OffShop()
    {
        ShopPanel.SetActive(false);
    }
}
