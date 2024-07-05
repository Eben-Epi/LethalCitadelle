using HarmonyLib;
using UnityEngine;

namespace LethalCitadelle.Patches;

[HarmonyPatch(typeof(FlowerSnakeEnemy))]

internal class FlowerSnakeEnemyPatch
{
    [HarmonyPatch("Start")]
    [HarmonyPostfix]
    static void OverrideAudio(FlowerSnakeEnemy __instance)
    {
        __instance.enemyType.audioClips = LethalCitadelle.PoutouVoices.ToArray();
        __instance.enemyType.audioClips[4] = null;
        __instance.flappingAudio.clip = null;
    }
}