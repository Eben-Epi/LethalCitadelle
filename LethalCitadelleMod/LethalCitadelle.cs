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
    internal static List<AudioClip> PoutouVoices;
    internal static List<AudioClip> GuiraudScream;
    internal static List<AudioClip> GuiraudWall;
    internal static List<AudioClip> Gluanksman;
    
    internal static AssetBundle BundleMelucheAngry;
    internal static AssetBundle BundleMelucheCalm;
    internal static AssetBundle BundlePoutou;
    internal static AssetBundle BundleGuiraudScream;
    internal static AssetBundle BundleGuiraudWall;
    internal static AssetBundle BundleGluanksman;


    private void Awake()
    {
        Logger = base.Logger;
        Instance = this;

        Patch();

        Logger.LogInfo($"{MyPluginInfo.PLUGIN_GUID} v{MyPluginInfo.PLUGIN_VERSION} has loaded!");

        MelucheVoicesAngry = new List<AudioClip>();
        MelucheVoicesCalm = new List<AudioClip>();
        PoutouVoices = new List<AudioClip>();
        GuiraudScream = new List<AudioClip>();
        GuiraudWall = new List<AudioClip>();
        Gluanksman = new List<AudioClip>();
        
        string FolderLocation = Instance.Info.Location;
        FolderLocation = FolderLocation.TrimEnd("EbenStream.LethalCitadelle.dll".ToCharArray());
        
        BundleMelucheAngry = AssetBundle.LoadFromFile(FolderLocation + "melucheangry");
        BundleMelucheCalm = AssetBundle.LoadFromFile(FolderLocation + "meluchecalm");
        BundlePoutou = AssetBundle.LoadFromFile(FolderLocation + "poutou");
        BundleGuiraudWall = AssetBundle.LoadFromFile(FolderLocation + "guiraudwall");
        BundleGuiraudScream = AssetBundle.LoadFromFile(FolderLocation + "guiraudscream");
        BundleGluanksman = AssetBundle.LoadFromFile(FolderLocation + "gluanksman");
        
        
        if (BundleMelucheAngry && BundlePoutou && BundleMelucheCalm)
        {
            MelucheVoicesAngry = BundleMelucheAngry.LoadAllAssets<AudioClip>().ToList();
            MelucheVoicesCalm = BundleMelucheCalm.LoadAllAssets<AudioClip>().ToList();
            PoutouVoices = BundlePoutou.LoadAllAssets<AudioClip>().ToList();
            GuiraudScream = BundleGuiraudScream.LoadAllAssets<AudioClip>().ToList();
            GuiraudWall = BundleGuiraudWall.LoadAllAssets<AudioClip>().ToList();
            Gluanksman = BundleGluanksman.LoadAllAssets<AudioClip>().ToList();

            Logger.LogError("Successfully loaded asset bundle");
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
        Harmony.PatchAll(typeof(FlowerSnakeEnemyPatch));
        Harmony.PatchAll(typeof(CrawlerAIPatch));
        Harmony.PatchAll(typeof(PufferAIPatch));

        Logger.LogDebug("Finished patching!");

    }

    internal static void Unpatch()
    {
        Logger.LogDebug("Unpatching...");

        Harmony?.UnpatchSelf();

        Logger.LogDebug("Finished unpatching!");
    }
}
