using System.Collections.Generic;
using System.Linq;
using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using LethalCitadelle.Patches;
using UnityEngine;

namespace LethalCitadelle;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
public class LethalCitadelle : BaseUnityPlugin
{
    public static LethalCitadelle Instance { get; private set; } = null!;
    internal new static ManualLogSource Logger { get; private set; } = null!;
    internal static Harmony? Harmony { get; set; }

    internal static List<AudioClip> MelucheVoicesAngry;
    internal static List<AudioClip> MelucheVoicesCalm;
    internal static AssetBundle BundleAngry;
    internal static AssetBundle BundleCalm;

    private void Awake()
    {
        Logger = base.Logger;
        Instance = this;

        Patch();

        Logger.LogInfo($"{MyPluginInfo.PLUGIN_GUID} v{MyPluginInfo.PLUGIN_VERSION} has loaded!");

        MelucheVoicesAngry = new List<AudioClip>();
        MelucheVoicesCalm = new List<AudioClip>();
        
        string FolderLocation = Instance.Info.Location;
        FolderLocation = FolderLocation.TrimEnd("EbenStream.LethalCitadelle.dll".ToCharArray());
        
        BundleAngry = AssetBundle.LoadFromFile(FolderLocation + "melucheangry");
        BundleCalm = AssetBundle.LoadFromFile(FolderLocation + "meluchecalm");
        
        
        if (BundleAngry != null)
        {
            Logger.LogError("Successfully loaded asset bundle");
            MelucheVoicesAngry = BundleAngry.LoadAllAssets<AudioClip>().ToList();
            MelucheVoicesCalm = BundleCalm.LoadAllAssets<AudioClip>().ToList();
        }
        else
        {
            Logger.LogError("Failed to load asset bundle");
        }
    }

    internal static void Patch()
    {
        Harmony ??= new Harmony(MyPluginInfo.PLUGIN_GUID);

        Logger.LogDebug("Patching...");

        Harmony.PatchAll();
        Harmony.PatchAll(typeof(HoarderBugAIPatch));

        Logger.LogDebug("Finished patching!");
    }

    internal static void Unpatch()
    {
        Logger.LogDebug("Unpatching...");

        Harmony?.UnpatchSelf();

        Logger.LogDebug("Finished unpatching!");
    }
}
