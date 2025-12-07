using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AppleTree : MonoBehaviour
{
    [Header("Inscribed")]                                                  // a
    // Prefab for instantiating apples
    public GameObject applePrefab;
    public GameObject goldApplePrefab;
    public GameObject poisonApplePrefab;

    [Range(0f, 1f)] public float normalAppleChance = 0.6f;
    [Range(0f, 1f)] public float goldAppleChance = 0.2f;
    [Range(0f, 1f)] public float poisonAppleChance = 0.2f;


    [Header("Base Difficulty Values")]
    // These are the values for level 0 (easiest)
    public float baseSpeed = 1f;
    public float baseChangeDirChance = 0.1f;
    public float baseAppleDropDelay = 1f;

    [Header("Scaling Per Level")]
    // How much harder each level gets
    public float speedIncreasePerLevel = 0.3f;
    public float changeDirIncreasePerLevel = 0.3f;
    public float dropDelayDecreasePerLevel = 0.2f;

    [Header("Difficulty Settings")]
    // Each index = one difficulty level (0 = easiest)
    public float[] speedLevels;
    public float[] changeDirChanceLevels;
    public float[] appleDropDelayLevels;

    public int level = 0;
    public int maxLevel = 4;
    public float timePerLevel = 30f;
    private float levelTimer = 0f;

    // Speed at which the AppleTree moves
    public float speed = 1f;
 
    // Distance where AppleTree turns around
    public float leftAndRightEdge = 10f;
 
    // Chance that the AppleTree will change directions
    public float changeDirChance = 0.1f;
 
    // Seconds between Apples instantiations
    public float appleDropDelay = 1f;

    [Header("UI")]
    public Text levelText;

    void Start()
    {
        ApplyDifficultyLevel();

        // Start dropping apples                                           // b
        Invoke("DropApple", appleDropDelay);
    }
    void DropApple()
    {
        GameObject prefabToSpawn = ChooseApplePrefab();

        GameObject apple = Instantiate(prefabToSpawn);
        apple.transform.position = transform.position;

        Invoke("DropApple", appleDropDelay);
    }

    GameObject ChooseApplePrefab()
    {
        // Get a random value between 0 and 1
        float r = Random.value;

        // Normal apple
        if (r < normalAppleChance)
        {
            return applePrefab;
        }
        // Gold apple
        else if (r < normalAppleChance + goldAppleChance)
        {
            return goldApplePrefab;
        }
        // Poison apple (fallback)
        else
        {
            return poisonApplePrefab;
        }
    }


    void Update()
    {
        levelTimer += Time.deltaTime;

        if (level < maxLevel && levelTimer >= timePerLevel)
        {
            level++;
            levelTimer = 0f;
            ApplyDifficultyLevel();
        }


        // Basic Movement                                                  // b
        Vector3 pos = transform.position;
        pos.x += speed * Time.deltaTime;
        transform.position = pos;

        // Changing Direction                                              // b
        if (pos.x < -leftAndRightEdge)
        {
            speed = Mathf.Abs(speed); // Move right
        }
        else if (pos.x > leftAndRightEdge)
        {
            speed = -Mathf.Abs(speed); // Move left
        }
        // else if (Random.value < changeDirChance)
        // {
        //     speed *= -1; // Change direction
        // }
    }

    private void FixedUpdate()
    {
        if (Random.value < changeDirChance)
        {
            speed *= -1; // Change direction
        }
    }

    void ApplyDifficultyLevel()
    {
        // speed scales up
        speed = baseSpeed * (1f + speedIncreasePerLevel * level);

        // changeDirChance scales up (clamped to 1)
        changeDirChance = baseChangeDirChance * (1f + changeDirIncreasePerLevel * level);
        changeDirChance = Mathf.Clamp01(changeDirChance);

        // appleDropDelay scales down (faster drops)
        appleDropDelay = baseAppleDropDelay - dropDelayDecreasePerLevel * level;
        appleDropDelay = Mathf.Max(0.1f, appleDropDelay); // don’t let it hit 0

        UpdateLevelText();
    }

    void UpdateLevelText()
    {
        if (levelText != null)
        {
            // +1 so the player sees Level 1,2,3 instead of 0,1,2
            levelText.text = "Level: " + (level + 1);
        }
    }
}

