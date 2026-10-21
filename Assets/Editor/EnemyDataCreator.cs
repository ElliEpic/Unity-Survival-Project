
using UnityEngine;
using UnityEditor;
using System.IO;

public class EnemyDataCreator : EditorWindow
{
    private string enemyName = "New Enemy";
    private Sprite enemySprite;

    private float maxHealth = 10f;
    private float damage = 1f;
    private float moveSpeed = 2f;

    private int experienceToGive = 1;
    private float pushTime = 0.2f;
    private GameObject destroyEffect;

    private string saveFolder = "Assets/EnemyData";

    [MenuItem("Tools/Enemy Data Creator")]
    public static void ShowWindow()
    {
        GetWindow<EnemyDataCreator>("Enemy Data Creator");
    }

    private void OnGUI()
    {
        GUILayout.Label(
            "Create a New Enemy",
            EditorStyles.boldLabel);

        EditorGUILayout.Space();

        enemyName = EditorGUILayout.TextField(
            "Enemy Name", enemyName);

        enemySprite = (Sprite)EditorGUILayout.ObjectField(
            "Sprite",
            enemySprite,
            typeof(Sprite),
            false);

        EditorGUILayout.Space();
        EditorGUILayout.LabelField(
            "Combat Stats", EditorStyles.boldLabel);

        maxHealth = EditorGUILayout.FloatField(
            "Max Health", maxHealth);

        damage = EditorGUILayout.FloatField(
            "Damage", damage);

        moveSpeed = EditorGUILayout.FloatField(
            "Move Speed", moveSpeed);

        EditorGUILayout.Space();
        EditorGUILayout.LabelField(
            "Rewards and Effects", EditorStyles.boldLabel);

        experienceToGive = EditorGUILayout.IntField(
            "Experience Reward", experienceToGive);

        pushTime = EditorGUILayout.FloatField(
            "Push Time", pushTime);

        destroyEffect = (GameObject)EditorGUILayout.ObjectField(
            "Destroy Effect",
            destroyEffect,
            typeof(GameObject),
            false);

        EditorGUILayout.Space();

        saveFolder = EditorGUILayout.TextField(
            "Save Folder", saveFolder);

        EditorGUILayout.Space();

        if (GUILayout.Button("Create Enemy Data Asset"))
        {
            CreateEnemyAsset();
        }
    }

    private void CreateEnemyAsset()
    {
        if (string.IsNullOrWhiteSpace(enemyName))
        {
            EditorUtility.DisplayDialog(
                "Invalid Enemy Name",
                "Please enter an enemy name.",
                "OK");
            return;
        }

        if (maxHealth <= 0 || damage < 0 || moveSpeed < 0)
        {
            EditorUtility.DisplayDialog(
                "Invalid Stats",
                "Health must be positive. Damage and speed cannot be negative.",
                "OK");
            return;
        }

        if (experienceToGive < 0 || pushTime < 0)
        {
            EditorUtility.DisplayDialog(
                "Invalid Stats",
                "Experience and push time cannot be negative.",
                "OK");
            return;
        }

        if (!saveFolder.StartsWith("Assets"))
        {
            EditorUtility.DisplayDialog(
                "Invalid Folder",
                "Choose a folder inside Assets.",
                "OK");
            return;
        }

        if (!AssetDatabase.IsValidFolder(saveFolder))
        {
            CreateFolders(saveFolder);
        }

        string safeName = MakeSafeFileName(enemyName);
        string path = AssetDatabase.GenerateUniqueAssetPath(
            saveFolder + "/" + safeName + ".asset");

        EnemyData data =
            ScriptableObject.CreateInstance<EnemyData>();

        data.enemyName = enemyName;
        data.sprite = enemySprite;
        data.maxHealth = maxHealth;
        data.damage = damage;
        data.moveSpeed = moveSpeed;
        data.experienceToGive = experienceToGive;
        data.pushTime = pushTime;
        data.destroyEffect = destroyEffect;

        AssetDatabase.CreateAsset(data, path);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        EditorUtility.FocusProjectWindow();
        Selection.activeObject = data;

        Debug.Log("Created enemy data at: " + path);
    }

    private static void CreateFolders(string folderPath)
    {
        string[] folders = folderPath.Split('/');
        string currentPath = folders[0];

        for (int i = 1; i < folders.Length; i++)
        {
            string nextPath = currentPath + "/" + folders[i];

            if (!AssetDatabase.IsValidFolder(nextPath))
            {
                AssetDatabase.CreateFolder(
                    currentPath, folders[i]);
            }

            currentPath = nextPath;
        }
    }

    private static string MakeSafeFileName(string value)
    {
        foreach (char c in Path.GetInvalidFileNameChars())
        {
            value = value.Replace(c, '_');
        }

        return value.Trim();
    }
}
