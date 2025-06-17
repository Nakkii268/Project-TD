using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{

    public void LoadMenu()
    {
       // SceneManager.LoadScene("LoadingScene");
        StartCoroutine(DelayLoadMenu());    
        SceneManager.LoadScene("TestMenu");
        if (UIManager.Instance != null)
        {
            UIManager.Instance.OpenUI<MenuUI>();
        }
    }
    public void LoadStage(string stagePath)
    {
        SceneManager.LoadScene("LoadingScene");
        StartCoroutine(DelayLoadStage(stagePath));
    }
    private IEnumerator DelayLoadStage(string path)
    {
        yield return new WaitForSeconds(1);
        Addressables.LoadSceneAsync(path).Completed += handler =>
        {
            if (handler.Status == AsyncOperationStatus.Succeeded)
            {
                Debug.Log("loadCompleted");
                UIManager.Instance.CloseAllUI();
                UIManager.Instance.OpenUI<InGameUI>();

            }
            else
            {
                Debug.Log("loadFailed");
            }
        };
    }
    private IEnumerator DelayLoadMenu()
    {
        if (UIManager.Instance != null)
        {
            UIManager.Instance.CloseAllUI();
        }
        yield return new WaitForSeconds(1);
    }
}
