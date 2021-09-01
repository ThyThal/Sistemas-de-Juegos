using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TooltipSystem : MonoBehaviour
{
    private static TooltipSystem current;
    public Tooltip tooltip;

    private void Awake()
    {
        current = this;
    }

    public static void Show(string content, string rarity, string damage, string speed, string header)
    {
        current.tooltip.SetDataWeapon(content, rarity, damage, speed, header);
        current.tooltip.CanvasGroup.DOFade(1, 0.1f);
        //current.tooltip.gameObject.SetActive(true);
    }

    public static void Hide()
    {
        current.tooltip.CanvasGroup.DOFade(0, 0.1f);
        //current.tooltip.gameObject.SetActive(false);
    }
}
