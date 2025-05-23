// -----------------------------------------------------------------------
// <copyright file="Armor.cs" company="ExMod Team">
// Copyright (c) ExMod Team. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace Exiled.API.Features.Items
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Exiled.API.Features.Pickups;
    using Exiled.API.Interfaces;

    using InventorySystem.Items.Armor;
    using PlayerRoles;

    using Structs;
    using UnityEngine;

    /// <summary>
    /// A wrapper class for <see cref="BodyArmor"/>.
    /// </summary>
    public class Armor : Item, IWrapper<BodyArmor>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Armor"/> class.
        /// </summary>
        /// <param name="itemBase">The base <see cref="BodyArmor"/> class.</param>
        public Armor(BodyArmor itemBase)
            : base(itemBase)
        {
            Base = itemBase;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Armor"/> class.
        /// </summary>
        /// <param name="type">The <see cref="ItemType"/> of the armor.</param>
        internal Armor(ItemType type)
            : this((BodyArmor)Server.Host.Inventory.CreateItemInstance(new(type, 0), false))
        {
        }

        /// <summary>
        /// Gets the <see cref="BodyArmor"/> that this class is encapsulating.
        /// </summary>
        public new BodyArmor Base { get; }

        /// <summary>
        /// Gets a value indicating whether this item is equippable.
        /// </summary>
        public bool Equippable => Base.AllowEquip;

        /// <summary>
        /// Gets a value indicating whether this item is holsterable.
        /// </summary>
        public bool Holsterable => Base.AllowHolster;

        /// <summary>
        /// Gets a value indicating whether this is a worn item.
        /// </summary>
        public bool IsWorn => Base.IsWorn;

        /// <summary>
        /// Gets or sets a value indicating whether excess ammo should be removed when the armor is dropped.
        /// </summary>
        [Obsolete("Not functional anymore", true)]
        public bool RemoveExcessOnDrop
        {
            get => false;
            set => _ = value;
        }

        /// <summary>
        /// Gets or sets the Weight of the armor.
        /// </summary>
        public new float Weight
        {
            get => Base.Weight;
            set => Base._weight = value;
        }

        /// <summary>
        /// Gets or sets how strong the helmet on the armor is.
        /// </summary>
        public int HelmetEfficacy
        {
            get => Base.HelmetEfficacy;
            set => Base.HelmetEfficacy = value;
        }

        /// <summary>
        /// Gets or sets how strong the vest on the armor is.
        /// </summary>
        public int VestEfficacy
        {
            get => Base.VestEfficacy;
            set => Base.VestEfficacy = value;
        }

        /// <summary>
        /// Gets or sets how much faster stamina will drain when wearing this armor.
        /// </summary>
        public float StaminaUseMultiplier
        {
            get => Base._staminaUseMultiplier;
            set => Base._staminaUseMultiplier = value;
        }

        /// <summary>
        /// Gets or sets the stamina regen multiplier.
        /// </summary>
        public float StaminaRegenMultiplier { get; set; } = 1f;

        /// <summary>
        /// Gets how much the users movement speed should be affected when wearing this armor. (higher values = slower movement).
        /// </summary>
        public float MovementSpeedMultiplier => Base._movementSpeedMultiplier;

        /// <summary>
        /// Gets how much worse <see cref="RoleTypeId.ClassD"/> and <see cref="RoleTypeId.Scientist"/>s are affected by wearing this armor.
        /// </summary>
        public float CivilianDownsideMultiplier => Base.CivilianClassDownsidesMultiplier;

        /// <summary>
        /// Gets or sets the ammo limit of the wearer when using this armor.
        /// </summary>
        public IEnumerable<ArmorAmmoLimit> AmmoLimits
        {
            get => Base.AmmoLimits.Select(limit => (ArmorAmmoLimit)limit);
            set => Base.AmmoLimits = value.Select(limit => (BodyArmor.ArmorAmmoLimit)limit).ToArray();
        }

        /// <summary>
        /// Gets or sets the item caterory limit of the wearer when using this armor.
        /// </summary>
        public IEnumerable<BodyArmor.ArmorCategoryLimitModifier> CategoryLimits
        {
            get => Base.CategoryLimits;
            set => Base.CategoryLimits = value.ToArray();
        }

        /// <summary>
        /// Clones current <see cref="Armor"/> object.
        /// </summary>
        /// <returns> New <see cref="Armor"/> object. </returns>
        public override Item Clone() => new Armor(Type)
        {
            Weight = Weight,
            StaminaUseMultiplier = StaminaUseMultiplier,
            CategoryLimits = CategoryLimits,
            StaminaRegenMultiplier = StaminaRegenMultiplier,
            AmmoLimits = AmmoLimits,
            VestEfficacy = VestEfficacy,
            HelmetEfficacy = HelmetEfficacy,
        };

        /// <inheritdoc/>
        internal override void ReadPickupInfoBefore(Pickup pickup)
        {
            base.ReadPickupInfoBefore(pickup);
            if (pickup is Pickups.BodyArmorPickup armorPickup)
            {
                HelmetEfficacy = armorPickup.HelmetEfficacy;
                VestEfficacy = armorPickup.VestEfficacy;
                StaminaUseMultiplier = armorPickup.StaminaUseMultiplier;
                StaminaRegenMultiplier = armorPickup.StaminaRegenMultiplier;
                AmmoLimits = armorPickup.AmmoLimits;
                CategoryLimits = armorPickup.CategoryLimits;
            }
        }
    }
}