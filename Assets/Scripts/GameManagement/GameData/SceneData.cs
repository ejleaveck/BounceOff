using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SceneData", menuName = "Game Data/Scene Data", order = 0)]
public class SceneData : ScriptableObject
{
    public int sceneIndex;
    public int nextSceneIndex;
    //additional scene specific data fields.
}
