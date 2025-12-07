using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplePicker : MonoBehaviour
{
    [Header("Inscribed")]
    public GameObject basketPrefab;
    public int numBaskets = 3;
    public float basketBottomY = -14f;
    public float basketSpacingY = 2f;
    public List<GameObject> basketList;

    void Start()
    {
        basketList = new List<GameObject>();
        for (int i = 0; i < numBaskets; i++)
        {
            GameObject tBasketGO = Instantiate<GameObject>(basketPrefab);
            Vector3 pos = Vector3.zero;
            pos.y = basketBottomY + (basketSpacingY * i);
            tBasketGO.transform.position = pos;
            basketList.Add(tBasketGO);
        }
    }

    public void AppleMissed()
    {
        List<GameObject> appleList = new List<GameObject>();

        appleList.AddRange(GameObject.FindGameObjectsWithTag("Apple"));
        appleList.AddRange(GameObject.FindGameObjectsWithTag("Apple_Gold"));
        appleList.AddRange(GameObject.FindGameObjectsWithTag("Apple_Poison"));

        GameObject[] appleArray = appleList.ToArray();

        foreach (GameObject tempGO in appleArray)
        {
            Destroy(tempGO);
        }

        int basketIndex = basketList.Count - 1;
        GameObject basketGO = basketList[basketIndex];

        basketList.RemoveAt(basketIndex);
        Destroy(basketGO);
        if (basketList.Count == 0)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("GameOverScreen");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
