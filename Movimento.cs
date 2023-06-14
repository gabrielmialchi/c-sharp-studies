using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

/*public class Movimento : MonoBehaviour
*{
    private CharacterController character;
    private Animator animator;
    private Vector3 inputs;
    private float speed = 4f;
    private float runningSpeed = 6.5f;
    private float crouchingSpeed = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        character = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float currentSpeed = speed;

        inputs.Set(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        character.Move(inputs * Time.deltaTime * speed);
        character.Move(Vector3.down * Time.deltaTime * 25f);

        if(inputs != Vector3.zero)
        {
            animator.SetBool("isWalking", true);
            transform.forward = Vector3.Slerp(transform.forward, inputs, Time.deltaTime * 10);
        } 
        
        else
        {
            animator.SetBool("isWalking", false);
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            animator.SetBool("isRunning", true);
            currentSpeed *= runningSpeed;

        }

        else
        {
            animator.SetBool("isRunning", false);
        }

        if (Input.GetKey(KeyCode.LeftControl))
        {
            animator.SetBool("isCrouching", true);
            currentSpeed *= crouchingSpeed;
        }

        else
        {
            animator.SetBool("isCrouching", false);
        }


    }
}*/


public class Movimento : MonoBehaviour
{
    private CharacterController character;
    private Animator animator;
    private Vector3 inputs;
    private bool isPicking = false;
    private float speed = 2.5f;
    private float runningSpeedModifier = 2.9f; // Modificador de velocidade para correr
    private float crouchingSpeedModifier = 1.1f; // Modificador de velocidade para agachar
    private float damping = 5f; // Valor de desaceleração ao girar mais de 90 graus durante a corrida



    // Start is called before the first frame update
    void Start()
    {
        character = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        inputs.Set(Input.GetKey(KeyCode.D) ? 1 : Input.GetKey(KeyCode.A) ? -1 : 0, 0, Input.GetKey(KeyCode.W) ? 1 : Input.GetKey(KeyCode.S) ? -1 : 0);
        inputs = inputs.normalized; // Normaliza o vetor de entrada

        float currentSpeed = speed;
        float turnAngleThreshold = 75f; // Ângulo de mudança de direção em que ocorre a desaceleração
        float turnSpeedModifier = 0.001f; // Modificador de velocidade durante a desaceleração
        float acceleration = 1f; // Aceleração do movimento

        if (Input.GetKey(KeyCode.LeftShift))
        {
            animator.SetBool("isRunning", true);
            currentSpeed *= runningSpeedModifier;

            // Aceleração gradual ao iniciar o movimento
            currentSpeed = Mathf.Lerp(currentSpeed, currentSpeed * runningSpeedModifier, Time.deltaTime * acceleration);

            if (inputs != Vector3.zero)
            {
                float angle = Vector3.Angle(transform.forward, inputs);

                if (angle >= turnAngleThreshold)
                {
                    // Aplica a desaceleração gradual reduzindo a velocidade atual
                    currentSpeed *= turnSpeedModifier;
                }
            }
        }
        else
        {
            animator.SetBool("isRunning", false);
        }


        if (Input.GetKey(KeyCode.LeftControl))
        {
            animator.SetBool("isCrouching", true);
            currentSpeed *= crouchingSpeedModifier; // Aplica o modificador de velocidade para agachar
        }
        else
        {
            animator.SetBool("isCrouching", false);
        }

        character.Move(inputs * Time.deltaTime * currentSpeed);
        character.Move(Vector3.down * Time.deltaTime * 15f);

        if (inputs != Vector3.zero)
        {
            animator.SetBool("isWalking", true);
            transform.forward = Vector3.Slerp(transform.forward, inputs, Time.deltaTime * 10);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }

        if (Input.GetKey(KeyCode.E))
        {
            // Define a variável de controle isPicking como true para ativar a transição de animação
            animator.SetBool("isPicking", true);
        }
        else
        {
            // Define a variável de controle isPicking como false para interromper a animação de picking
            animator.SetBool("isPicking", false);
        }


    }
}

