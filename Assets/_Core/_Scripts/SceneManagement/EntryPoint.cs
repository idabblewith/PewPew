using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

/*
    The entrypoint for the app.
    Has references to various scenes which it will load asyncronously so as to maintain functionality
    between menu and game scenes.
    Entrypoint Scene will be unloaded once main menu scene is loaded on startup.

    Entrypoint -> ManagementScene + MenuScene -> Remove Entrypoint -> Next scene

    Updated: 3/12/22 - Jarid Prince

    TODO: Make a single MainCamera in Management Scene, remove from other scenes
*/

public class EntryPoint : MonoBehaviour
{
    // References to other scenes that are marked as addressable
    [SerializeField] private SceneReference m_ManagerScene;
    [SerializeField] private SceneReference m_MainMenuScene;

    // Used as opposed to the scriptable Object directly as that would create a duplicate
    // and monobehaviours listening to events wouldn't receive them.
    [SerializeField] private AssetReferenceT<LoadSceneChannel> m_LoadSceneChannel;

    // Loads the Manager scene additively (in the background) 
    // and awaits asyncronous loading of menu scene before unloading entrypoint scene
    // hence IEnumerator
    private IEnumerator Start()
    {
        yield return m_ManagerScene.LoadSceneAsync(LoadSceneMode.Additive);
        var handle = m_LoadSceneChannel.LoadAssetAsync<LoadSceneChannel>();
        yield return handle;
        handle.Result.Load(m_MainMenuScene);
        SceneManager.UnloadSceneAsync(0); //Entrypoint scene
        // CHANGED FROM UnloadScene to UnloadSceneAsync due to Obsolete error
    }
}
