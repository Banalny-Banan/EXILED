// -----------------------------------------------------------------------
// <copyright file="CancellingItemUseEventArgs.cs" company="ExMod Team">
// Copyright (c) ExMod Team. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace Exiled.Events.EventArgs.Player
{
    using API.Features;
    using Exiled.API.Features.Items;
    using Exiled.Events.EventArgs.Interfaces;
    using InventorySystem.Items.Usables;

    /// <summary>
    /// Contains all information before a player cancels usage of an item.
    /// </summary>
    public class CancellingItemUseEventArgs : IPlayerEvent, IDeniableEvent, IUsableEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CancellingItemUseEventArgs" /> class.
        /// </summary>
        /// <param name="hub">
        /// <inheritdoc cref="Player" />
        /// </param>
        /// <param name="item">
        /// <inheritdoc cref="UsedItemEventArgs.Item" />
        /// </param>
        public CancellingItemUseEventArgs(ReferenceHub hub, UsableItem item)
        {
            Player = Player.Get(hub);
            Usable = Item.Get<Usable>(item);
        }

        /// <summary>
        /// Gets the item that the player cancelling.
        /// </summary>
        public Usable Usable { get; }

        /// <inheritdoc/>
        public Item Item => Usable;

        /// <summary>
        /// Gets the player who is cancelling the item.
        /// </summary>
        public Player Player { get; }

        /// <summary>
        /// Gets or sets a value indicating whether the player can cancelling the use of item.
        /// </summary>
        public bool IsAllowed { get; set; } = true;
    }
}