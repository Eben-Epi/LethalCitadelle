using HarmonyLib;
using UnityEngine;

namespace LethalCitadelle.Patches;

[HarmonyPatch(typeof(CrawlerAI))]

internal class CrawlerAIPatch
{
    [HarmonyPatch("Start")]
    [HarmonyPostfix]
    static void OverrideAudio(CrawlerAI __instance)
    {
        __instance.longRoarSFX = LethalCitadelle.GuiraudScream.ToArray();
        __instance.hitWallSFX = LethalCitadelle.GuiraudWall.ToArray();
    }
}