using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SceneData", menuName = "Scene Management/Scene Data")]
public class SceneData : ScriptableObject
{
    public int sceneIndex;
    public int nextSceneIndex;
    public string[] availablePowerups;
    //additional scene specific data fields.
}
