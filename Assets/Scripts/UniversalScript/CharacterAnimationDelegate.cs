using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationDelegate : MonoBehaviour
{
    public GameObject Left_Arm_Attack_Point, Right_Arm_Attack_Point, Left_Leg_Attack_Point, Right_Leg_Attack_Point;

    public float standup_timer = 2f;
    private CharacterAnimation animScript;

    private AudioSource audioSource;
    [SerializeField]
    private AudioClip wooshSound, fallSound, groundhit_Sound, deadSound;
    private EnemyMovement enemy_Movement;
    private ShakeCamera shakeCamera;
    void Awake()
    {
        animScript = GetComponent<CharacterAnimation>();
        audioSource = GetComponent<AudioSource>();
        if (gameObject.CompareTag(Tags.ENEMY_TAG))
        {
            enemy_Movement = GetComponentInParent<EnemyMovement>();
        }

        shakeCamera = GameObject.FindWithTag(Tags.MAIN_CAMERA_TAG).GetComponent<ShakeCamera>();
    }
    void LeftArmAttackOn()
    {
        Left_Arm_Attack_Point.SetActive(true);
    }

    void leftArmAttackOff()
    {
        if (Left_Arm_Attack_Point.activeInHierarchy)
        {
            Left_Arm_Attack_Point.SetActive(false);
        }
    }

    void RightArmAttackOn()
    {
        Right_Arm_Attack_Point.SetActive(true);
    }

    void RightArmAttackOff()
    {
        if (Right_Arm_Attack_Point.activeInHierarchy)
        {
            Right_Arm_Attack_Point.SetActive(false);
        }
    }

    void LeftLegAttackOn()
    {
        Left_Leg_Attack_Point.SetActive(true);
    }

    void LeftLegAttackOff()
    {
        if (Left_Leg_Attack_Point.activeInHierarchy)
        {
            Left_Leg_Attack_Point.SetActive(false);
        }
    }

    void RightLegAttackOn()
    {
        Right_Leg_Attack_Point.SetActive(true);
    }

    void RightLegAttackOff()
    {
        if (Right_Leg_Attack_Point.activeInHierarchy)
        {
            Right_Leg_Attack_Point.SetActive(false);
        }
    }

    void TagLeftArm()
    {
        Left_Arm_Attack_Point.tag = Tags.LEFT_ARM_TAG;
    }

    void UnTagLeftArm()
    {
        Left_Arm_Attack_Point.tag = Tags.UNTAGGED_TAG;
    }

    void TagLeftLeg()
    {
        Left_Leg_Attack_Point.tag = Tags.LEFT_LEG_TAG;
    }

    void UnTagLeftLeg()
    {
        Left_Leg_Attack_Point.tag = Tags.UNTAGGED_TAG;
    }


    void Enemy_Standup()
    {
        StartCoroutine(StanUpafterTime());
    }

    IEnumerator StanUpafterTime()
    {
        yield return new WaitForSeconds(standup_timer);
        animScript.StandUp();
    }

    public void attackFXSound()
    {
        audioSource.volume = 0.2f;
        audioSource.clip = wooshSound;
        audioSource.Play();
    }
    void CharacterDied()
    {
        audioSource.volume = 1f;
        audioSource.clip = deadSound;
        audioSource.Play();
    }

    void enemy_knockdown()
    {
        audioSource.clip = fallSound;
        audioSource.Play();
    }

    void enemy_hitGround()
    {
        audioSource.clip = groundhit_Sound;
        audioSource.Play();
    }

    void disableMovement()
    {
        enemy_Movement.enabled = false;

        transform.parent.gameObject.layer = 0;
    }

    void EnableMovement()
    {
        enemy_Movement.enabled = true;
        transform.parent.gameObject.layer = 7;
    }

    void ShakeCameraOnFall()
    {
        shakeCamera.ShouldShake = true;
    }

    void characterDied()
    {
        Invoke("deActivateGameObject", 2f);
    }

    void deActivateGameObject()
    {
        EnemyManager.instance.SpawnEnemy();
        gameObject.SetActive(false);
    }

}//
