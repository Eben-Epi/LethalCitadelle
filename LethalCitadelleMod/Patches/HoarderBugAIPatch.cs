using HarmonyLib;

namespace LethalCitadelle.Patches;

[HarmonyPatch(typeof(HoarderBugAI))]

internal class HoarderBugAIPatch
{
    [HarmonyPatch("Start")]
    [HarmonyPostfix]
    static void OverrideAudio(HoarderBugAI __instance)
    {
        __instance.chitterSFX = LethalCitadelle.MelucheVoicesCalm.ToArray();
        __instance.angryScreechSFX = LethalCitadelle.MelucheVoicesAngry.ToArray();
        __instance.bugFlySFX = null;
        __instance.hitPlayerSFX = null;
    }
}