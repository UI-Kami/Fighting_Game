using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ComboState
    {
        NONE,
        PUNCH_1,
        PUNCH_2,
        PUNCH_3,
        KICK_1,
        KICK_2
    }
public class PlayerAttack : MonoBehaviour
{

    private bool activateTimerToReset;

    private float default_combo_timer = 0.4f;
    private float current_combo_timer;

    private ComboState current_combo_state;
    private CharacterAnimation player_Anim;


    void Awake()
    {
        player_Anim = GetComponentInChildren<CharacterAnimation>();
    }
    void Start()
    {
        current_combo_timer = default_combo_timer;
        current_combo_state = ComboState.NONE;
    }

    void Update()
    {
        ComboAttack();
        resetComboState();
    }


    void ComboAttack()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (current_combo_state == ComboState.PUNCH_3 || current_combo_state == ComboState.KICK_1
            || current_combo_state == ComboState.KICK_2)
                return;
            current_combo_state++;
            activateTimerToReset = true;
            current_combo_timer = default_combo_timer;
            if (current_combo_state == ComboState.PUNCH_1)
            {
                player_Anim.Punch_1();

            }
            if (current_combo_state == ComboState.PUNCH_2)
            {
                player_Anim.Punch_2();

            }
            if (current_combo_state == ComboState.PUNCH_3)
            {
                player_Anim.Punch_3();

            }
        }//Combo Attacks
        if (Input.GetKeyDown(KeyCode.X))
        {//if current combo is punch 3 or kick 2 return 
         // No possible combos left
            if (current_combo_state == ComboState.KICK_2 ||
                current_combo_state == ComboState.PUNCH_3)
                return;
            //if the current combo state is NONE, punch2 or pucnh 2
            //then we set current combo state to kick1 to chain combos
            if (current_combo_state == ComboState.NONE ||
                current_combo_state == ComboState.PUNCH_1 || current_combo_state == ComboState.PUNCH_2)
            {

                current_combo_state = ComboState.KICK_1;
            }
            else if (current_combo_state == ComboState.KICK_1)
            {
                //move to kick2
                current_combo_state++;
            }
            //reset combos values to default
            activateTimerToReset = true;
            current_combo_timer = default_combo_timer;
            if (current_combo_state == ComboState.KICK_1)
            {
                player_Anim.Kick_1();
            }
            if (current_combo_state == ComboState.KICK_2)
            {
                player_Anim.Kick_2();
            }//if kicks combos
        }
    }//combo attacts
    void resetComboState()
        {

            if (activateTimerToReset)
            {
                current_combo_timer -= Time.deltaTime;
                if (current_combo_timer < 0f)
                {
                    current_combo_state = ComboState.NONE;
                    activateTimerToReset = false;
                    current_combo_timer = default_combo_timer;
                }

            }
        }





















}//class
