// -----------------------------------------------------------------------
// <copyright file="GeneratorState.cs" company="ExMod Team">
// Copyright (c) ExMod Team. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace Exiled.API.Enums
{
    using System;

    using Features;

    /// <summary>
    /// Represents the state of a <see cref="Generator"/>.
    /// </summary>
    /// <seealso cref="Generator.State"/>
    /// <seealso cref="Generator.Get(GeneratorState)"/>
    [Flags]
    public enum GeneratorState : byte
    {
        /// <summary>
        /// Generator is doing nothing.
        /// </summary>
        None = 0,

        /// <summary>
        /// Generator is unlocked.
        /// </summary>
        Unlocked = 2,

        /// <summary>
        /// Generator is open.
        /// </summary>
        Open = 4,

        /// <summary>
        /// Generator is activating.
        /// </summary>
        Activating = 8,

        /// <summary>
        /// Generator is engaged.
        /// </summary>
        Engaged = 16,
    }
}