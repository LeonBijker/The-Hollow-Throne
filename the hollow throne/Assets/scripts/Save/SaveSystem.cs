using System;
using System.IO;
using UnityEngine;

[Serializable]
public class SaveData
{
    public int sceneBuildIndex;
    public float playerX;
    public float playerY;
    public bool doubleJumpUnlocked;
    public string[] completedGoals;
    public string savedAt;

    public SaveData()
    {
        completedGoals = Array.Empty<string>();
        savedAt = string.Empty;
    }
}

public static class SaveSystem
{
    private static readonly string fileName = "savegame.json";

    public static string SavePath => Path.Combine(Application.persistentDataPath, fileName);

    // When loading from main menu we store the loaded data here so a persistent manager
    // (GameManager) can apply it after the gameplay scene finishes loading.
    public static SaveData PendingLoad { get; set; }

    public static bool HasSave()
    {
        try
        {
            return File.Exists(SavePath);
        }
        catch (Exception)
        {
            return false;
        }
    }

    public static void Save(SaveData data)
    {
        try
        {
            data.savedAt = DateTime.UtcNow.ToString("o");
            string json = JsonUtility.ToJson(data, prettyPrint: true);
            File.WriteAllText(SavePath, json);
            Debug.Log($"SaveSystem: saved to {SavePath}");
        }
        catch (Exception ex)
        {
            Debug.LogError($"SaveSystem: failed to save - {ex}");
        }
    }

    public static SaveData Load()
    {
        try
        {
            if (!HasSave()) return null;
            string json = File.ReadAllText(SavePath);
            return JsonUtility.FromJson<SaveData>(json);
        }
        catch (Exception ex)
        {
            Debug.LogError($"SaveSystem: failed to load - {ex}");
            return null;
        }
    }

    private static SaveData LoadOrCreate()
    {
        SaveData d = Load();
        if (d != null) return d;
        return new SaveData { sceneBuildIndex = 0, playerX = 0f, playerY = 0f, doubleJumpUnlocked = false };
    }

    public static string[] GetCompletedGoals()
    {
        SaveData d = LoadOrCreate();
        return d.completedGoals ?? Array.Empty<string>();
    }

    public static bool HasCompletedGoal(string id)
    {
        if (string.IsNullOrEmpty(id)) return false;
        string[] list = GetCompletedGoals();
        foreach (string g in list) if (g == id) return true;
        return false;
    }

    public static void AddCompletedGoal(string id)
    {
        if (string.IsNullOrEmpty(id)) return;
        SaveData d = LoadOrCreate();
        System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>(d.completedGoals ?? Array.Empty<string>());
        if (!list.Contains(id))
        {
            list.Add(id);
            d.completedGoals = list.ToArray();
            Save(d);
        }
    }

    public static void Delete()
    {
        try
        {
            if (HasSave()) File.Delete(SavePath);
        }
        catch (Exception ex)
        {
            Debug.LogError($"SaveSystem: failed to delete - {ex}");
        }
    }
}
