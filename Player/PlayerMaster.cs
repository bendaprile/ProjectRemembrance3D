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

    private UIController UIControl;


    // Start is called before the first frame update
    void Start()
    {
        // Core functionality
        playerMovement = GetComponent<PlayerMovement>();
        weaponController = GetComponentInChildren<WeaponController>();
        animationUpdater = GetComponentInChildren<PlayerAnimationUpdater>();
        abilitiesController = GetComponentInChildren<AbilitiesController>();

        UIControl = GameObject.Find("UI").GetComponent<UIController>();
    }

    private void Update()
    {
        if (!UIControl.GamePaused)
        {
            abilitiesController.HandleAbilities(1f);

            // Player movement
            playerMovement.UpdatePlayerState();

            // Player animation
            animationUpdater.UpdateAnimation();

            // Item Controller
            weaponController.HandleWeapon();
        }

    }

    private void FixedUpdate()
    {

        if (playerMovement.moveState != PlayerMovement.MoveState.Melee)
        {
            playerMovement.Move();
            animationUpdater.UpdatePlayerDirection();
        }
    }
}
