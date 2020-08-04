using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMaster : MonoBehaviour
{

    PlayerMovement playerMovement;
    PlayerAnimationUpdater animationUpdater;
    WeaponController weaponController;
    AbilitiesController abilitiesController;
    LightningBoltCast lightningBolt;
    LightningSphereThrow lightningSphere;
    ConsumableController consumableController;

    private UIController UIControl;


    // Start is called before the first frame update
    void Start()
    {
        // Core functionality
        playerMovement = GetComponent<PlayerMovement>();
        weaponController = GetComponentInChildren<WeaponController>();
        animationUpdater = GetComponentInChildren<PlayerAnimationUpdater>();
        abilitiesController = GetComponentInChildren<AbilitiesController>();
        consumableController = GetComponentInChildren<ConsumableController>();

        UIControl = GameObject.Find("UI").GetComponent<UIController>();
    }

    private void Update()
    {
        if (!UIControl.GamePaused)
        {
            abilitiesController.HandleAbilities(1f);

            // Item Controller
            weaponController.HandleWeapon();

            //Consumable Controller
            consumableController.HandleConsumables();

            // Player movement
            playerMovement.UpdatePlayerState();

            // Player animation
            animationUpdater.UpdateAnimation();
        }

    }

    private void FixedUpdate()
    {

        if (playerMovement.GetMoveState() != MoveState.Melee)
        {
            playerMovement.Move();
        }
        animationUpdater.UpdatePlayerDirection();
    }
}
