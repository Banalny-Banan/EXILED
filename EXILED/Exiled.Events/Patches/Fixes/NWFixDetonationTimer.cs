// -----------------------------------------------------------------------
// <copyright file="NWFixDetonationTimer.cs" company="ExMod Team">
// Copyright (c) ExMod Team. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace Exiled.Events.Patches.Fixes
{
    using System;
    using System.Linq;

    using GameCore;
    using HarmonyLib;

    /// <summary>
    /// Fixes the issue where the game was not selecting the scenario with the nearest <see cref="AlphaWarheadController.DetonationScenario.TimeToDetonate"/> value.
    /// <a href="https://git.scpslgame.com/northwood-qa/scpsl-bug-reporting/-/issues/396">Bug Report</a>
    /// </summary>
    [HarmonyPatch(typeof(AlphaWarheadController), nameof(AlphaWarheadController.Start))]
    internal class NWFixDetonationTimer
    {
        private static void Postfix()
        {
            AlphaWarheadSyncInfo networkInfo = default;
            networkInfo.ScenarioId = (byte)Array.IndexOf(AlphaWarheadController.Singleton.StartScenarios, AlphaWarheadController.Singleton.StartScenarios.OrderBy(d => Math.Abs(d.TimeToDetonate - ConfigFile.ServerConfig.GetByte("warhead_tminus_start_duration", 90))).First());

            AlphaWarheadController.Singleton.NetworkInfo = networkInfo;
            return;
        }
    }
}
