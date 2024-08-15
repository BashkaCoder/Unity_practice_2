using UnityEngine;
using UnityEngine.UI;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private float _duration;
    
    [Header("UI Components")]
    [SerializeField] private Image _image;
    [SerializeField] private Button _hideButton;
    [SerializeField] private Button _revealButton;

    private void Start()
    {
        //Named method
        _hideButton.onClick.AddListener(() =>
        {
            StopAllCoroutines();
            StartCoroutine(UiAnimations.AnimateFadeOut(_duration, HideImage));
        });
        
        //Anonymous method
        _revealButton.onClick.AddListener(() =>
        {
            StopAllCoroutines();
            StartCoroutine(UiAnimations.AnimateFadeIn(_duration, () => Debug.Log("Image has been revealed")));
            //OR with raw delegate
            // StartCoroutine(UiAnimations.AnimateFadeIn(_duration, delegate()
            // {
            //     Debug.Log("Image has been revealed");
            // }));
        });
        
        UiAnimations.Initialize(_image);
    }

    private void HideImage()
    {
        _image.gameObject.SetActive(false);
        Debug.Log("Image has been hidden");
    }
}