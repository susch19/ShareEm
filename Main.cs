using BepInEx;
using BepInEx.Logging;
using UnityEngine;
using HarmonyLib;
using PacketHelper;
using System;
using System.IO;
using System.Reflection;


namespace ShareEmRebalanced
{
    [BepInPlugin(Id, nameof(ShareEmRebalanced), Version)]
    public class Main : BaseUnityPlugin
    {
        #region[Declarations]

        public const string
            MODNAME = "$safeprojectname$",
            AUTHOR = "",
            GUID = AUTHOR + "_" + MODNAME,
            VERSION = "1.0.0.0";

        public static ManualLogSource MainLogger { get; private set; }

        internal ManualLogSource log;
        internal readonly Harmony harmony;
        internal readonly Assembly assembly;
        public readonly string modFolder;

        #endregion


        public const string Id = "mod.susch19.ShareEmRebalanced";
        public const string Version = "1.0.0";
        public const string Name = "ShareEmRebalanced";
        public Main()
        {

            log = base.Logger;
            MainLogger = log;
            MainLogger.LogMessage("Share Em All!");
             harmony = new Harmony(Id);
            assembly = Assembly.GetExecutingAssembly();
            modFolder = Path.GetDirectoryName(assembly.Location);
        }

        public void Start()
        {
            harmony.PatchAll(assembly);

        }
    }
}
