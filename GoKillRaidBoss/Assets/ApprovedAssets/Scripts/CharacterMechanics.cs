using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMechanics : MonoBehaviour {

    //Основные параметры
    public float speedMove;
    public float jumpPower;
    public float x;
    public float y;

    //Параметры геймплея для персонажа
    private float gravityForce; //гравитация персонажа
    private Vector3 moveVector; //Направление движения персонажа
    public bool isDive = false; //Перекаты, включаются из Аниматор скрипта

    //Ссылки на компоненты
    private CharacterController ch_controller;
    private Animator ch_animator;
    private MobileController mContr;

    private void Start() {
        ch_controller = GetComponent<CharacterController>();
        ch_animator = GetComponent<Animator>();
        mContr = GameObject.FindGameObjectWithTag("Joystick").GetComponent<MobileController>();

        StartCoroutine(SpeedCalculator());
    }

    private void Update() {
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");
        CharacterMove();
        GamingGravity();
        Dive();
    }

    public float speedometr;
    IEnumerator SpeedCalculator() {
        for (;;) {
            Vector3 startPoint = transform.position;
            yield return new WaitForSeconds(0.1f);
            Vector3 finishPoint = transform.position;
            speedometr = Vector3.Distance(startPoint, finishPoint) / 0.1f;
        }
    }

    //метод перемещения
    private void CharacterMove() {
        //перемещение по поверхности
        if (ch_controller.isGrounded) {
            ch_animator.ResetTrigger("Jump");
            ch_animator.SetBool("Falling", false);

            moveVector = Vector3.zero;
            moveVector.x = mContr.Horizontal() * speedMove;
            moveVector.z = mContr.Vertical() * speedMove;


            //Делает скорость по диагонали такой же как и вперед - назад и лево-право (Теорема пифагора)
            float pythagoras = ((moveVector.x * moveVector.x) + (moveVector.z * moveVector.z));
            if (pythagoras > (speedMove * speedMove)) {
                float magnitude = Mathf.Sqrt(pythagoras);
                float multiplier = speedMove / magnitude;
                moveVector.x *= multiplier;
                moveVector.z *= multiplier;
            }

            //анимация передвижения персонажа
            if ((moveVector.x != 0 || moveVector.z != 0)
                && !ch_animator.GetCurrentAnimatorStateInfo(0).IsTag("Dive")) {
                ch_animator.SetBool("Move", true);
            } else {
                ch_animator.SetBool("Move", false);
            }


            //поворот персонажа в сторону направления перемещения
            if ((Vector3.Angle(Vector3.forward, moveVector) > 1f ||
                Vector3.Angle(Vector3.forward, moveVector) == 0) &&
                ch_animator.GetCurrentAnimatorClipInfo(0)[0].clip.name != "StandingAimRecoil" &&
                !ch_animator.GetBool("Attack2")) {
                Vector3 direct = Vector3.RotateTowards(transform.forward, moveVector, speedMove, 0.0f);
                transform.rotation = Quaternion.LookRotation(direct);
            }
        } else {
            if (gravityForce < -8f /*&& !ch_animator.GetBool("Move")*/) ch_animator.SetBool("Falling", true);
        }

        moveVector.y = gravityForce;
        if (ch_animator.GetCurrentAnimatorClipInfo(0)[0].clip.name != "StandingAimRecoil"
            && !ch_animator.GetCurrentAnimatorStateInfo(0).IsTag("Dive")) {
            ch_controller.Move(moveVector * Time.deltaTime);//Метод передвижения по направлению
        }
    }

    //метод гравитации
    private void GamingGravity() {
        if (!ch_controller.isGrounded) gravityForce -= 40f * Time.deltaTime;
        else gravityForce = -1f;
        if (Input.GetKeyDown(KeyCode.LeftAlt) && ch_controller.isGrounded) {
            gravityForce = jumpPower;
            ch_animator.SetTrigger("Jump");
        }
    }


    public void Dive() {
        ch_animator.ResetTrigger("Dive");
        if (Input.GetKeyDown(KeyCode.Space) && ch_controller.isGrounded) {
            ch_animator.SetTrigger("Dive");
        }

        if (isDive) {
            ch_controller.Move(20f * gameObject.transform.forward * Time.deltaTime);
        }
    }
}
