using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;


/*
    Utilises the LoadSceneChannel SO's load event to perform OnLoadScene when event occurs.

    Updated: 3/12/22 - Jarid Prince
*/

public class SceneLoader : MonoBehaviour
// , ISavable<SceneData>
{
    // References to Scenes to load, current scene and the SO LoadSceneChannel whose events are subscribed to
    [SerializeField] private LoadSceneChannel m_LoadSceneChannel;
    // [SerializeField] private GameData m_GameData;
    // // Using SO handlers for invoking save and load events as variables
    // [SerializeField] private SaveDataChannel m_SaveDataChannel;
    // [SerializeField] private LoadDataChannel m_LoadDataChannel;


    private SceneReference m_SceneToLoad;
    private SceneReference m_CurrentSceneReference;

    // // Implementation of ISavable which returns new SceneData with id set as scene's id
    public SceneData data => new SceneData
    {
        id = m_CurrentSceneReference.AssetGUID
    };

    // Wakeup and Sleep - subscribe and unsubscribe from events
    private void OnEnable() 
    {
        // listening
        m_LoadSceneChannel.load += OnLoadScene;
        // m_SaveDataChannel.save += OnSaveData;
        // m_LoadDataChannel.load += OnLoadData;
    }

    private void OnDisable() 
    {
        // avoiding memory leaks
        m_LoadSceneChannel.load -= OnLoadScene;
        // m_SaveDataChannel.save -= OnSaveData;
        // m_LoadDataChannel.load -= OnLoadData;
    }

    // Loading + Saving Data based on event invocation
    // public void OnSaveData()
    // {
    //     m_GameData.Save(this);
    // }

    // public void OnLoadData()
    // {
    //    m_GameData.Load(this);     
    // }

    public void Load(SceneData sceneData)
    {
        OnLoadScene(new SceneReference(sceneData.id));
    }

    // As Scene Mounted (load event)
    private void OnLoadScene(SceneReference sceneReference)
    {
        m_SceneToLoad = sceneReference;

        if(m_CurrentSceneReference != null)
        {
            m_CurrentSceneReference.UnLoadScene();
        }

        AsyncOperationHandle<SceneInstance> handle = sceneReference.LoadSceneAsync(
            UnityEngine.SceneManagement.LoadSceneMode.Additive);
        handle.Completed += OnSceneLoaded;
    }

    // After Scene Mounted
    private void OnSceneLoaded(AsyncOperationHandle<SceneInstance> handle)
    {
        Scene scene = handle.Result.Scene;
        SceneManager.SetActiveScene(scene);
        m_CurrentSceneReference = m_SceneToLoad;
    }
}
