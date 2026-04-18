// -----------------------------------------------------------------------
// <copyright file="Shooting.cs" company="ExMod Team">
// Copyright (c) ExMod Team. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace Exiled.Events.Patches.Events.Player
{
    using System;

    using API.Features;
    using Exiled.Events.Attributes;
    using Exiled.Events.EventArgs.Player;
    using HarmonyLib;
    using InventorySystem.Items.Firearms;
    using InventorySystem.Items.Firearms.Modules.Misc;
    using PlayerRoles.FirstPersonControl;
    using RelativePositioning;
    using UnityEngine;

    /// <summary>
    /// Patches <see cref="Player" />.
    /// Adds the <see cref="Handlers.Player.Shooting" /> events.
    /// </summary>
    [EventPatch(typeof(Handlers.Player), nameof(Handlers.Player.Shooting))]

    // public void ProcessShot(Firearm firearm, Action<ReferenceHub> processingMethod)
    [HarmonyPatch(typeof(ShotBacktrackData), nameof(ShotBacktrackData.ProcessShot))]
    internal static class Shooting
    {
#pragma warning disable SA1313
        private static bool Prefix(Firearm firearm, Action<ReferenceHub> processingMethod, ShotBacktrackData __instance)
#pragma warning restore SA1313
        {
            if (!WaypointBase.TryGetWaypoint(__instance.RelativeOwnerPosition.WaypointId, out WaypointBase wp))
                return false;

            double lagCompensation = 0.0;

            if (firearm.Owner != null)
                lagCompensation = firearm.Owner.connectionToClient.rtt / 2.0;

            using (new WaypointBacktracker(wp, lagCompensation))
            {
                Vector3 worldspacePosition = wp.GetWorldspacePosition(__instance.RelativeOwnerPosition.Relative);
                Quaternion worldspaceRotation = wp.GetWorldspaceRotation(__instance.RelativeOwnerRotation);

                using (new FpcBacktracker(firearm.Owner, worldspacePosition, worldspaceRotation))
                {
                    if (__instance.HasPrimaryTarget)
                    {
                        using (new WaypointBacktracker(__instance.PrimaryTargetRelativePosition.WaypointId, lagCompensation))
                        {
                            using (new FpcBacktracker(__instance.PrimaryTargetHub, __instance.PrimaryTargetRelativePosition.Position))
                            {
                                ShootingEventArgs args = new(firearm, ref __instance);
                                Handlers.Player.OnShooting(args);

                                if (args.IsAllowed)
                                    processingMethod(__instance.PrimaryTargetHub);
                            }
                        }
                    }
                    else
                    {
                        ShootingEventArgs args = new(firearm, ref __instance);
                        Handlers.Player.OnShooting(args);

                        if (args.IsAllowed)
                            processingMethod(null);
                    }
                }
            }

            return false;
        }
    }
}