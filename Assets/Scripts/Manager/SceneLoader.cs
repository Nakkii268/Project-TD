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
        SceneManager.LoadScene("LoadingScene");
        StartCoroutine(DelayLoadMenu());    
        SceneManager.LoadScene("TestMenu");
    }
    public void LoadStage(string stagePath)
    {
        SceneManager.LoadScene("LoadingScene");
        StartCoroutine(DelayLoadStage(stagePath));
    }
    private IEnumerator DelayLoadStage(string path)
    {
        yield return new WaitForSeconds(2);
        Addressables.LoadSceneAsync(path).Completed += handler =>
        {
            if (handler.Status == AsyncOperationStatus.Succeeded)
            {
                Debug.Log("loadCompleted");
                UIManager.Instance.CloseAllUI();

            }
            else
            {
                Debug.Log("loadFailed");
            }
        };
    }
    private IEnumerator DelayLoadMenu()
    {
        yield return new WaitForSeconds(1);

    }
}
