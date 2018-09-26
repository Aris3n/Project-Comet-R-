using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    public void ShowCanvas(CanvasGroup newCanvas)
    {
        CanvasGroup currentCanvas = GetComponentInParent<CanvasGroup>();
        FadeOut(currentCanvas);
        FadeIn(newCanvas);
    }
    public void ShowCanvas(CanvasGroup currentCanvas, CanvasGroup newCanvas)
    {
        FadeOut(currentCanvas);
        FadeIn(newCanvas);
    }
    public void SwapButtons(Button button, Button targetButton)
    {
        button.gameObject.SetActive(false);
        targetButton.gameObject.SetActive(true);
    }
    void FadeIn(CanvasGroup canvasGroup)
    {
        StartCoroutine(FadeCanvasGroup(canvasGroup, canvasGroup.alpha, 1));
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }
    void FadeOut(CanvasGroup canvasGroup)
    {
        StartCoroutine(FadeCanvasGroup(canvasGroup, canvasGroup.alpha, 0));
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }
    public IEnumerator FadeCanvasGroup(CanvasGroup cg, float start, float end, float lerpTime = 0.15f)
    {
        float _timeStrapedLerping = Time.time;
        float timeSinceStarted = Time.time - _timeStrapedLerping;
        float percentageComplete = timeSinceStarted / lerpTime;
        while (true)
        {
            timeSinceStarted = Time.time - _timeStrapedLerping;
            percentageComplete = timeSinceStarted / lerpTime;
            float currentValue = Mathf.Lerp(start, end, percentageComplete);
            cg.alpha = currentValue;
            if (percentageComplete >= 1)
                break;
            yield return new WaitForEndOfFrame();
        }
    }
}
