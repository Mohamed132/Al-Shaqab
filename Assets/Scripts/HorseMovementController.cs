using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorseMovementController : MonoBehaviour
{

    public static HorseMovementController singlton;
    public float Speed = 0;
    [SerializeField]
    private float SpeedAdder = .5f;
    [SerializeField]
    private float SpeedMultiplyer = 5;
    [SerializeField]
    private AnimationCurve StartUpCurve;
    [SerializeField]
    private AnimationCurve FinishCurve;

    private Animator[] AnimControllers;
    private GameController m_GameController;
    private bool bFinish;
    private float FinishTime;
    private void Awake()
    {
        if (!singlton)
        {
            singlton = this;
        }
        AnimControllers = GetComponentsInChildren<Animator>();
    }

    private void Start()
    {
        m_GameController = GameController.singleton;
    }

    private void Update()
    {
        if (bFinish)
        {
            Speed = Mathf.Lerp(Speed, -.5f, Time.deltaTime * .5f);
            SpeedMultiplyer = FinishCurve.Evaluate(GameController.singleton.GameTimer - FinishTime);
        }
        else
        {
            SpeedMultiplyer = StartUpCurve.Evaluate(GameController.singleton.GameTimer);
        }
        foreach (var anim in AnimControllers)
        {
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("LocoMotion"))
            {
                anim.SetFloat("Speed", Speed);
                anim.speed = 1;
            }
            else
            {
                anim.speed = 1 + Speed * 1.5f;
            }
        }


        transform.position += transform.forward * (Speed + SpeedAdder) * Time.deltaTime * SpeedMultiplyer;
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Obstacle":
                {
                    foreach (var anim in AnimControllers)
                    {
                        anim.SetTrigger("Jump");
                    }
                }
                break;
            case "Finish":
                {
                    bFinish = true;
                    FinishTime = m_GameController.GameTimer;
                }
                break;
        }

    }

    public void SetSpeed(float NewSpeed)
    {
        if (!bFinish)
            Speed = NewSpeed;
    }
}
