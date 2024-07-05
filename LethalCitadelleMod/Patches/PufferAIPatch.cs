using HarmonyLib;
using UnityEngine;

namespace LethalCitadelle.Patches;

[HarmonyPatch(typeof(PufferAI))]

internal class PufferAIPatch
{
    [HarmonyPatch("Start")]
    [HarmonyPostfix]
    static void OverrideAudio(PufferAI __instance)
    {
        AudioClip[] frighten = { LethalCitadelle.Gluanksman[1] };
        
        __instance.frightenSFX = frighten;
        __instance.puff = LethalCitadelle.Gluanksman[2];
        __instance.angry = LethalCitadelle.Gluanksman[0];
    }
}