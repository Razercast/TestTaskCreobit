using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectButton : MonoBehaviour
{
    public string sceneName;

    public void LoadLevel()
    {
        LevelSelectManager.instance.LoadLevel(sceneName);
    }
}
