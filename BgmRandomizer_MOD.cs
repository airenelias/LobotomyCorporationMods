using Harmony;
using System;
using System.IO;
using System.Reflection;
using UnityEngine;

namespace BgmRandomizer_MOD
{
    public class Harmony_Patch
    {
        public Harmony_Patch()
        {
            try
            {
                var harmony = HarmonyInstance.Create("BgmRandomizer");
                var assembly = Assembly.GetExecutingAssembly();
                harmony.PatchAll(assembly);
            }
            catch (Exception ex)
            {
                File.WriteAllText(Application.dataPath + "/BaseMods/BgmRandomizer_MOD/error.txt", ex.Message);
            }
        }

        [HarmonyPatch(typeof(BgmManager), "Awake")]
        private class BgmRandomizerPatch
        {
            public static void Postfix(BgmManager __instance)
            {
                try
                {
                    string[] bgmNames = File.ReadAllLines(Application.dataPath + "/BaseMods/BgmRandomizer_MOD/bgm_list.txt");
                    foreach (string bgmName in bgmNames)
                    {
                        __instance.normal.list.Add(UnityEngine.Resources.Load<AudioClip>(bgmName));
                    }
                }
                catch (Exception ex)
                {
                    File.WriteAllText(Application.dataPath + "/BaseMods/BgmRandomizer_MOD/error.txt", ex.Message);
                }
            }
        }
    }
}