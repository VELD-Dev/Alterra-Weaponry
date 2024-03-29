﻿global using System;
global using System.Collections;
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
global using Nautilus.Assets;
global using Nautilus.Assets.Gadgets;
global using Nautilus.Assets.PrefabTemplates;
global using Nautilus.Commands;
global using Nautilus.Crafting;
global using Nautilus.Extensions;
global using Nautilus.FMod;
global using Nautilus.FMod.Interfaces;
global using Nautilus.Handlers;
global using Nautilus.Json;
global using Nautilus.Json.Attributes;
global using Nautilus.Json.Converters;
global using Nautilus.Json.ExtensionMethods;
global using Nautilus.Json.Interfaces;
global using Nautilus.Options;
global using Nautilus.Options.Attributes;
global using Nautilus.Utility;
global using Nautilus.Utility.MaterialModifiers;
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
global using UWE;
global using UWEScript;
global using FMOD;

// LOCAL
global using VELD.AlterraWeaponry.Items;
global using VELD.AlterraWeaponry.Patches;
global using VELD.AlterraWeaponry.Utils;
global using VELD.AlterraWeaponry.Behaviours;

// MY LIBS
global using CuddleLibs;
global using CuddleLibs.Assets;
global using CuddleLibs.Assets.Gadgets;
global using CuddleLibs.Interfaces;
global using CuddleLibs.Utility;