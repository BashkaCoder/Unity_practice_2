using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public static class UiAnimations
{
    private static Image _image;

    public static void Initialize(Image image)
    {
        _image = image;
    }

    public static IEnumerator AnimateFadeIn(float duration, Action callback)
    {
        _image.gameObject.SetActive(true);
        return AnimateFade(duration, 0.0f, 1.0f, callback);
    }
    
    public static IEnumerator AnimateFadeOut(float duration, Action callback)
    {
        return AnimateFade(duration, 1.0f, 0.0f, callback);
    }
    
    private static IEnumerator AnimateFade(float duration, float startAlpha, float targetAlpha,  Action callback)
    {
        SetImageAlpha(startAlpha);
        float timer = 0;
        while ((timer += Time.deltaTime) < duration)
        {
            SetImageAlpha(Mathf.Lerp(startAlpha, targetAlpha, timer / duration));
            yield return null;
        }
        SetImageAlpha(targetAlpha);
        callback?.Invoke();
    }
    
    private static void SetImageAlpha(float alpha)
    {
        var color = _image.color;
        color.a = alpha;
        _image.color = color;
    }
}