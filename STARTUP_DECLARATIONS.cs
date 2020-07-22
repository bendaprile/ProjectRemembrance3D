using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DamageType { True, Elemental, Regular }

public enum MoveDir { Forward, Left, Right, Backward }

public enum CoreIntrinsicSkillsEnum { Strength, Dexterity, Charisma, Intelligence, Toughness, Willpower }

public enum DerivedIntrinsicSkillsEnum { Vigor, Cerebral, Fortitude }

public enum AttributesEnum { max_health, health_regen, max_energy, max_expanded_energy, energy_regen, expanded_energy_regen, armor, plating, finesse_damage, brute_damage }

public enum SkillsEnum { Larceny, Science, Medicine, Repair, Speech, Survival, Perception }

public enum CombatSkillsEnum { ranged_proficiency, melee_proficiency, armor_proficiency }

public enum DerivedCombatSkillsEnum { Ranged, Melee, Armor }

public enum ItemTypeEnum { Misc, Armor, Weapon, Consumable }

public enum WeaponType{ Melee_1H, Melee_2H, Gun_1H, Gun_2H }

public enum ConsumableType { Restoring }

public enum ItemClass { D, C, B, A, S }

public static class STARTUP_DECLARATIONS
{
    public const int Number_of_Attributes = 8;
}
