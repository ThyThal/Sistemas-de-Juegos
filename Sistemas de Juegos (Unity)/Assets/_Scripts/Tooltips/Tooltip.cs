using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

[ExecuteInEditMode()]
public class Tooltip : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI _headerField;
    [SerializeField] public TextMeshProUGUI _rarityField;
    [SerializeField] public TextMeshProUGUI _descriptionField;
    [SerializeField] public TextMeshProUGUI _damageField;
    [SerializeField] public TextMeshProUGUI _speedField;
    [SerializeField] public LayoutElement _layoutElement;

    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] public CanvasGroup CanvasGroup;
    [SerializeField] public int _wrapLimit = 80;


    private void Update()
    {
        transform.position = mousePosition;

        float pivotX = transform.position.x / Screen.width;
        float pivotY = transform.position.y / Screen.width;
        _rectTransform.pivot = new Vector2(pivotX, pivotY);


    }

    public void SetDataWeapon(string content, string rarity, string damage, string speed, string header)
    {
        _headerField.text = header;
        _rarityField.text = rarity;
        _descriptionField.text = content;
        _damageField.text = damage;
        _speedField.text = speed;

        int headerLength = _headerField.text.Length;
        int descriptionLength = _descriptionField.text.Length;

        _layoutElement.enabled = (headerLength > _wrapLimit || descriptionLength > _wrapLimit) ? true : false;
    }

    public static Vector2 mousePosition
    {
        get
        {
            return Mouse.current.position.ReadValue();
        }
    }
}
