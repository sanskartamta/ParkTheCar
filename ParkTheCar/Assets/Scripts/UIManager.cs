using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    [SerializeField] LineDrawer lineDrawer;

    [Space]
    [SerializeField] private CanvasGroup availLineCanvasGroup;
    [SerializeField] private GameObject availLineHolder;
    [SerializeField] private Image availLineFill;
    private bool isAvailLineUIActive = false;

    [Space]
    [SerializeField] Image fadePanel;
    [SerializeField] float fadeDuration;

    private Route activeRoute;
    private void Start()
    {
        fadePanel.DOFade(0f, fadeDuration).From(1f);

        availLineCanvasGroup.alpha = 0f;

        lineDrawer.OnBeginDraw += OnBeginDrawHandler;
        lineDrawer.OnDraw += OnDrawHandler;
        lineDrawer.OnEndDraw += OnEndDrawHandler;
    }

    private void OnBeginDrawHandler(Route route)
    {
        activeRoute = route;

        availLineFill.color = activeRoute.carColor;
        availLineFill.fillAmount = 1f;
        availLineCanvasGroup.DOFade(1f, .3f).From(0f);
        isAvailLineUIActive = true;
    }

    private void OnDrawHandler()
    {
        if (isAvailLineUIActive)
        {
            float maxLineLength = activeRoute.MaxLineLength;
            float linelength = activeRoute.line.length;

            availLineFill.fillAmount = 1 - (linelength / maxLineLength);
        }
    }

    private void OnEndDrawHandler()
    {
        if (isAvailLineUIActive)
        {
            isAvailLineUIActive = false;
            activeRoute = null;

            availLineCanvasGroup.DOFade(0f, .3f).From(1f);
        }
    }
}
