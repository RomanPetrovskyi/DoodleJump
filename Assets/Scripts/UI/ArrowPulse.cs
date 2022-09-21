using System;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class ArrowPulse : MonoBehaviour
{
  private void Start()
  {
    var sq = DOTween.Sequence();
    sq.Append(transform.DOScale(0.7f, 0.2f));
    sq.Append(transform.DOScale(1f, 0.2f));
    sq.SetLoops(-1);
  }
}
