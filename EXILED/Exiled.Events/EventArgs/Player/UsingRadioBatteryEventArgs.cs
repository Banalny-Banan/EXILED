// -----------------------------------------------------------------------
// <copyright file="UsingRadioBatteryEventArgs.cs" company="ExMod Team">
// Copyright (c) ExMod Team. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace Exiled.Events.EventArgs.Player
{
    using API.Features;
    using API.Features.Items;

    using Interfaces;

    using InventorySystem.Items.Radio;

    /// <summary>
    /// Contains all information before radio battery charge is changed.
    /// </summary>
    public class UsingRadioBatteryEventArgs : IPlayerEvent, IDeniableEvent, IItemEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UsingRadioBatteryEventArgs" /> class.
        /// </summary>
        /// <param name="radio">
        /// <inheritdoc cref="Radio" />
        /// </param>
        /// <param name="drain">
        /// <inheritdoc cref="Drain" />
        /// </param>
        /// <param name="isAllowed">
        /// <inheritdoc cref="IsAllowed" />
        /// </param>
        public UsingRadioBatteryEventArgs(RadioItem radio, float drain, bool isAllowed = true)
        {
            Radio = Item.Get<Radio>(radio);
            Player = Radio.Owner;
            Drain = drain;
            IsAllowed = isAllowed;
        }

        /// <summary>
        /// Gets the <see cref="API.Features.Items.Radio" /> which is being used.
        /// </summary>
        public Radio Radio { get; }

        /// <inheritdoc/>
        public Item Item => Radio;

        /// <summary>
        /// Gets or sets the radio battery drain per second.
        /// </summary>
        public float Drain { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the radio battery charge can be changed.
        /// </summary>
        public bool IsAllowed { get; set; }

        /// <summary>
        /// Gets the player who's using the radio.
        /// </summary>
        public Player Player { get; }
    }
}