using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class InitialGameData
{
    public bool savedGameExists;

        public InitialGameData()
    {
        savedGameExists = false;
    }
}

//TODO: add any additional game data variables - Note, game load essential only, if save file is corrupt, defaults will be hard coded
//like if savedgameexists can't be loaded it will default to false and then continue from there.