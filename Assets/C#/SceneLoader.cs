using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.EventSystems.PointerEventData;

public class SceneLoader : MonoBehaviour
{
    public string sceneName;
    public RectTransform buttonRectTransform;

    private void Start()
    {
        buttonRectTransform.transform
            .DOScale(1.15f, 1f)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InSine).Play();
    }

    public void LoadScene()
    {
        StartCoroutine(LoadSceneCoroutine());
    }

    private IEnumerator LoadSceneCoroutine()
    {
        Scene lastScene = SceneManager.GetActiveScene();
        yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        AsyncOperation unloadOp = SceneManager.UnloadSceneAsync(lastScene);
        unloadOp.completed += op => SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));
    }

    private void OnDestroy()
    {
        buttonRectTransform.transform.DOKill();
    }
}