using System;
using UnityEngine;

/*
    SO responhsible for handling the scene loading

    Updated: 3/12/22 - Jarid Prince
*/

[CreateAssetMenu(fileName = "LoadSceneChennel")]
public class LoadSceneChannel : ScriptableObject
{
    // Custom Event that takes in a scene reference when invoked (utilised by SceneLoader class)
    public event Action<SceneReference> load;

    public void Load(SceneReference sceneReference)
    {
        load?.Invoke(sceneReference);
    }

}
