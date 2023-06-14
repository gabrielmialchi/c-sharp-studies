//Esse script usa as bibliotecas abaixo
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Fun��o ???
public class PlayerMovement : MonoBehaviour
{
    //As linhas abaixo criam vari�veis com os Components usados no personagem
    private Rigidbody2D rb; //Atribui "rb" como uma vari�vel do Component: Rigidbody2D
    private SpriteRenderer sprite; //Atribui "sprite" como uma vari�vel do Component: SpriteRenderer
    private Animator animator; //Atribui "animator" como uma vari�vel do Component: Animator

    //As linhas abaixo criam vari�veis de movimento do personagem
    private float dirX; //Cria "dirX" como uma vari�vel do tipo float
    [SerializeField] private float moveSpeed = 7f; //[Unity cria um campo customiz�vel no Editor] Cria "moveSpeed" como uma vari�vel do tipo float e aplica o valor base de 7f (float)
    [SerializeField] private float jumpForce = 10f; //[Unity cria um campo customiz�vel no Editor] Cria "jumpForce" como uma vari�vel do tipo float e aplica o valor base de 10f

    //Essa fun��o atribui valores int sequenciais em uma lista string de nome MovementState
    private enum MovementState
    {
        idle, running, jumping, falling //sempre que MovementState for idle, ent�o MovementState.idle = 0, (...)
    }

    //Essa fun��o � iniciada antes do primeiro frame, retorna nada para o programa e apenas pode ser acessada por esse script
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); //A vari�vel "rb" acessa o Component "Rigidbody2D"
        sprite = GetComponent<SpriteRenderer>();//A vari�vel "sprite" acessa o Component "SpriteRenderer"
        animator = GetComponent<Animator>();//A vari�vel "animator" acessa o Component "Animator"
    }

    //Essa fun��o � atualizada a cada frame, retorna nada para o programa e apenas pode ser acessada por esse script
    private void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");//Define � vari�vel "dirX" como Input (entrada do usu�rio) o eixo "Horizontal" do teclado : por padr�o, a Unity define "eixo horizontal" como as setas esquerda e direita ou A e D, sendo esquerda/A = -1 e direita/D = +1; nenhuma tecla acionada = 0
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);//Define velocidade linear � vari�vel "rb" como uma nova entrada de vetor 2D, onde X = "dirX" multiplicado pela vari�vel "moveSpeed", e Y= velocidade do eixo Y no momento que o eixo � acionado

        if (Input.GetButtonDown("Jump")) //Se o usu�rio pressionar a tecla "Barra de Espa�o" ; o programa manda apenas uma entrada mesmo se o usu�rio segurar a tecla
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce); //a velocidade linear da vari�vel "rb" passa a ser: X = velocidade do "rb" no eixo X no momento em que a tecla � apertada e Y = vari�vel "jumpForce"
        }

        UpdateAnimationState(); //Executa a fun��o de Estado da Anima��o

    }

    //Essa fun��o cria os estados de movimento e anima��o, retorna nada para o programa e apenas pode ser acessada por esse script
    private void UpdateAnimationState()
    {
        MovementState state; //Define a fun��o enum MovimentState como a vari�vel local "state", usada apenas nessa fun��o do c�digo

        if (dirX > 0f) //se a vari�vel "dirX" for maior que 0, portando, direita/D
        {
            state = MovementState.running; //Define a vari�vel "state" como MovementState.running, portanto, state = 1
            sprite.flipX = false; //No Component "SpriteRenderer" define a boolean flipX como "false" (box desclicado)
        }

        else if (dirX < 0f) //tamb�m se a vari�vel "dirX" for menor que 0, portanto, esquerda/A
        {
            state = MovementState.running; //Define a vari�vel "state" como MovementState.running, portanto, state = 1
            sprite.flipX = true; //No Component "SpriteRenderer" define a boolean flipX como "true" (box clicado)
        }

        else //e se dirX = 0
        {
            state = MovementState.idle; //Define a vari�vel "state" como MovementState.running, portanto, state = 0
        }

        if (rb.velocity.y > 0.01f) //se a velocidade do eixo Y do "rb" for maior que 0.01f, portando, pra cima
        {
            state = MovementState.jumping; //Define a vari�vel "state" como MovementState.running, portanto, state = 2
        }
        else if (rb.velocity.y < -0.01f) //se a velocidade do eixo Y do "rb" for menor que 0.01f, portando, pra baixo
        {
            state = MovementState.falling; //Define a vari�vel "state" como MovementState.running, portanto, state = 3
        }

        animator.SetInteger("state", (int)state); //o Component "Animator" ajusta o par�metro int "state" (criado na aba Animator do Editor), para o valor atual do int "state" (MovementState)
    }
}
