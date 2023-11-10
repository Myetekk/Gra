using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NewGame : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Image _img;
    [SerializeField] private Sprite _on, _off;
    [SerializeField] private AudioClip _click;
    [SerializeField] private AudioSource _source;
    [SerializeField] private Slider _slider;

    public void OnPointerDown(PointerEventData eventData)
    {
        _img.sprite = _on;
        _source.PlayOneShot(_click, _slider.value * 0.01f);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _img.sprite = _off;
    }
}
