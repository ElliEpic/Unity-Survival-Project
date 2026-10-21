
using UnityEngine;
using UnityEditor;

public class PlayerUpgradeCreator : EditorWindow
{
    private string upgradeName = "New Player Upgrade";
    private string description = "Upgrade description";
    private Sprite icon;

    private PlayerUpgradeType upgradeType =
        PlayerUpgradeType.PermanentDamageBonus;

    private int amount = 1;
    private string saveFolder = "Assets/PlayerUpgrades";

    [MenuItem("Tools/Player Upgrade Creator")]
    public static void ShowWindow()
    {
        GetWindow<PlayerUpgradeCreator>(
            "Player Upgrade Creator");
    }

    private void OnGUI()
    {
        GUILayout.Label(
            "Create Player Upgrade",
            EditorStyles.boldLabel);

        EditorGUILayout.Space();

        upgradeName = EditorGUILayout.TextField(
            "Upgrade Name", upgradeName);

        EditorGUILayout.LabelField("Description");

        description = EditorGUILayout.TextArea(
            description, GUILayout.MinHeight(45));

        icon = (Sprite)EditorGUILayout.ObjectField(
            "Upgrade Icon",
            icon,
            typeof(Sprite),
            false);

        EditorGUILayout.Space();

        GUILayout.Label(
            "Upgrade Effect",
            EditorStyles.boldLabel);

        upgradeType =
            (PlayerUpgradeType)EditorGUILayout.EnumPopup(
                "Upgrade Type", upgradeType);

        amount = EditorGUILayout.IntField(
            "Amount", amount);

        EditorGUILayout.Space();

        saveFolder = EditorGUILayout.TextField(
            "Save Folder", saveFolder);

        EditorGUILayout.Space();

        if (GUILayout.Button("Create Player Upgrade"))
        {
            CreateUpgradeAsset();
        }
    }

    private void CreateUpgradeAsset()
    {
        if (string.IsNullOrWhiteSpace(upgradeName))
        {
            EditorUtility.DisplayDialog(
                "Invalid Name",
                "Enter an upgrade name.",
                "OK");
            return;
        }

        if (amount <= 0)
        {
            EditorUtility.DisplayDialog(
                "Invalid Amount",
                "The upgrade amount must be greater than zero.",
                "OK");
            return;
        }

        if (string.IsNullOrWhiteSpace(saveFolder) ||
            !(saveFolder == "Assets" ||
              saveFolder.StartsWith("Assets/")))
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

        string safeName = MakeSafeName(upgradeName);

        string path = AssetDatabase.GenerateUniqueAssetPath(
            saveFolder + "/" + safeName + ".asset");

        PlayerUpgradeData upgrade =
            ScriptableObject.CreateInstance<PlayerUpgradeData>();

        upgrade.upgradeName = upgradeName;
        upgrade.description = description;
        upgrade.icon = icon;
        upgrade.upgradeType = upgradeType;
        upgrade.amount = amount;

        AssetDatabase.CreateAsset(upgrade, path);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Selection.activeObject = upgrade;
        EditorGUIUtility.PingObject(upgrade);

        Debug.Log("Created player upgrade: " + path);
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

    private static string MakeSafeName(string value)
    {
        foreach (char c in
                 System.IO.Path.GetInvalidFileNameChars())
        {
            value = value.Replace(c, '_');
        }

        return value.Trim();
    }
}
