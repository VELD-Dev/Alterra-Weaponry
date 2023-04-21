global using System;
global using System.Collections.Generic;
global using System.Linq;
global using System.Text;
global using System.Threading.Tasks;
global using System.Reflection;
global using System.IO;
global using System.Net;
global using System.Xml;
global using System.Xml.Serialization;

// Mod-Related
global using BepInEx;
global using BepInEx.Bootstrap;
global using BepInEx.Configuration;
global using BepInEx.Logging;
global using SMLHelper.V2.Assets;
global using SMLHelper.V2.Commands;
global using SMLHelper.V2.Crafting;
global using SMLHelper.V2.FMod;
global using SMLHelper.V2.FMod.Interfaces;
global using SMLHelper.V2.Interfaces;
global using SMLHelper.V2.Json;
global using SMLHelper.V2.Json.Attributes;
global using SMLHelper.V2.Json.Converters;
global using SMLHelper.V2.Json.ExtensionMethods;
global using SMLHelper.V2.Json.Interfaces;
global using SMLHelper.V2.Handlers;
global using SMLHelper.V2.MonoBehaviours;
global using SMLHelper.V2.Options;
global using SMLHelper.V2.Options.Attributes;
global using SMLHelper.V2.Utility;
global using HarmonyLib;
global using HarmonyLib.Public;
global using HarmonyLib.Public.Patching;
global using HarmonyLib.Tools;
global using Story;
global using Subnautica;
global using Unity;
global using UnityEngine;
global using UnityEditor;
global using UnityEngine.Events;

// LOCAL
global using VELD.AlterraWeaponry.items;
global using VELD.AlterraWeaponry.patches;
global using VELD.AlterraWeaponry.utils;
global using VELD.AlterraWeaponry.behaviours;