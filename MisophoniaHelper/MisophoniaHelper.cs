using UnityEngine;
using HarmonyLib;
using FMODUnity;
using System;
using HMLLibrary;

public class MisophoniaHelper : Mod
{
    Harmony harmony;
    static public JsonModInfo modInfo;
    public void Start()
    {
        modInfo = modlistEntry.jsonmodinfo;
        harmony = new Harmony("com.aidanamite.MisophoniaHelper");
        harmony.PatchAll();
        Log("Mod has been loaded!");
    }

    public void OnModUnload()
    {
        harmony.UnpatchAll();
        Log("Mod has been unloaded!");
    }

    public static void Log(object message)
    {
        Debug.Log("[" + modInfo.name + "]: " + message.ToString());
    }
}

[HarmonyPatch(typeof(RuntimeManager), "PlayOneShotAttached", new Type[] { typeof(string), typeof(GameObject) })]
public class Patch_PlaySound
{
    static bool Prefix(ref string path)
    {
        if (path == "event:/char/chew" || path == "event:/char/drink")
            return false;
        return true;
    }
}