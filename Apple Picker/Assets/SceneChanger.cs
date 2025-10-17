using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChanger : MonoBehaviour
{
    // Start is called before the first frame update
    public string sceneName;

    public virtual void ChangeScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
}
