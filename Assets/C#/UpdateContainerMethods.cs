using System;
using System.Collections;
using System.Timers;
using UnityEngine;
using UnityButton = UnityEngine.UI.Button;

public class UpdateContainerMethods : MonoBehaviour
{
    [SerializeField] CanvasGroup canvasgroup;
    [SerializeField] RectTransform rectTransformPanel;
    [SerializeField] RectTransform buttonRectTransform;
    [SerializeField] float animationTime;
    [SerializeField] UnityButton toggleButton;
    Vector2 initialSize;
    Vector2 collapsedSize;
    Vector2 buttonInitialPosition;
    Vector2 buttonCollapsedPosition;
    bool hidden;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        initialSize = rectTransformPanel.sizeDelta;
        collapsedSize = new Vector2(0, initialSize.y);
        buttonInitialPosition = buttonRectTransform.anchoredPosition;
        buttonCollapsedPosition = rectTransformPanel.anchoredPosition;
        hidden = false;
    }

    private IEnumerator TogglePanel()
    {
        float elapsed = 0f;
        float limit = 1f;
        Vector2 startSize = hidden ? collapsedSize : initialSize;
        Vector2 targetSize = hidden ? initialSize : collapsedSize;
        Vector2 buttonStartPosition = hidden ? buttonCollapsedPosition : buttonInitialPosition;
        Vector2 buttonTargetPosition = hidden ? buttonInitialPosition : buttonCollapsedPosition;
        toggleButton.interactable = canvasgroup.interactable = false;

        while (elapsed < limit)
        {
            rectTransformPanel.sizeDelta = Vector2.Lerp(startSize, targetSize, elapsed);
            buttonRectTransform.anchoredPosition = Vector2.Lerp(buttonStartPosition, buttonTargetPosition, elapsed);
            elapsed += Time.deltaTime/animationTime;
            yield return new WaitForEndOfFrame();
        }
        rectTransformPanel.sizeDelta = targetSize;
        buttonRectTransform.anchoredPosition = buttonTargetPosition;
        hidden = !hidden;
        canvasgroup.interactable = !hidden;
        toggleButton.interactable = true;
    }

    public void OnTogglePanel()
    {
        StartCoroutine(TogglePanel());
    }
}
