//Esse script usa as bibliotecas abaixo
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Função ???
public class PlayerMovement : MonoBehaviour
{
    //As linhas abaixo criam variáveis com os Components usados no personagem
    private Rigidbody2D rb; //Atribui "rb" como uma variável do Component: Rigidbody2D
    private SpriteRenderer sprite; //Atribui "sprite" como uma variável do Component: SpriteRenderer
    private Animator animator; //Atribui "animator" como uma variável do Component: Animator

    //As linhas abaixo criam variáveis de movimento do personagem
    private float dirX; //Cria "dirX" como uma variável do tipo float
    [SerializeField] private float moveSpeed = 7f; //[Unity cria um campo customizável no Editor] Cria "moveSpeed" como uma variável do tipo float e aplica o valor base de 7f (float)
    [SerializeField] private float jumpForce = 10f; //[Unity cria um campo customizável no Editor] Cria "jumpForce" como uma variável do tipo float e aplica o valor base de 10f

    //Essa função atribui valores int sequenciais em uma lista string de nome MovementState
    private enum MovementState
    {
        idle, running, jumping, falling //sempre que MovementState for idle, então MovementState.idle = 0, (...)
    }

    //Essa função é iniciada antes do primeiro frame, retorna nada para o programa e apenas pode ser acessada por esse script
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); //A variável "rb" acessa o Component "Rigidbody2D"
        sprite = GetComponent<SpriteRenderer>();//A variável "sprite" acessa o Component "SpriteRenderer"
        animator = GetComponent<Animator>();//A variável "animator" acessa o Component "Animator"
    }

    //Essa função é atualizada a cada frame, retorna nada para o programa e apenas pode ser acessada por esse script
    private void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");//Define à variável "dirX" como Input (entrada do usuário) o eixo "Horizontal" do teclado : por padrão, a Unity define "eixo horizontal" como as setas esquerda e direita ou A e D, sendo esquerda/A = -1 e direita/D = +1; nenhuma tecla acionada = 0
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);//Define velocidade linear à variável "rb" como uma nova entrada de vetor 2D, onde X = "dirX" multiplicado pela variável "moveSpeed", e Y= velocidade do eixo Y no momento que o eixo é acionado

        if (Input.GetButtonDown("Jump")) //Se o usuário pressionar a tecla "Barra de Espaço" ; o programa manda apenas uma entrada mesmo se o usuário segurar a tecla
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce); //a velocidade linear da variável "rb" passa a ser: X = velocidade do "rb" no eixo X no momento em que a tecla é apertada e Y = variável "jumpForce"
        }

        UpdateAnimationState(); //Executa a função de Estado da Animação

    }

    //Essa função cria os estados de movimento e animação, retorna nada para o programa e apenas pode ser acessada por esse script
    private void UpdateAnimationState()
    {
        MovementState state; //Define a função enum MovimentState como a variável local "state", usada apenas nessa função do código

        if (dirX > 0f) //se a variável "dirX" for maior que 0, portando, direita/D
        {
            state = MovementState.running; //Define a variável "state" como MovementState.running, portanto, state = 1
            sprite.flipX = false; //No Component "SpriteRenderer" define a boolean flipX como "false" (box desclicado)
        }

        else if (dirX < 0f) //também se a variável "dirX" for menor que 0, portanto, esquerda/A
        {
            state = MovementState.running; //Define a variável "state" como MovementState.running, portanto, state = 1
            sprite.flipX = true; //No Component "SpriteRenderer" define a boolean flipX como "true" (box clicado)
        }

        else //e se dirX = 0
        {
            state = MovementState.idle; //Define a variável "state" como MovementState.running, portanto, state = 0
        }

        if (rb.velocity.y > 0.01f) //se a velocidade do eixo Y do "rb" for maior que 0.01f, portando, pra cima
        {
            state = MovementState.jumping; //Define a variável "state" como MovementState.running, portanto, state = 2
        }
        else if (rb.velocity.y < -0.01f) //se a velocidade do eixo Y do "rb" for menor que 0.01f, portando, pra baixo
        {
            state = MovementState.falling; //Define a variável "state" como MovementState.running, portanto, state = 3
        }

        animator.SetInteger("state", (int)state); //o Component "Animator" ajusta o parâmetro int "state" (criado na aba Animator do Editor), para o valor atual do int "state" (MovementState)
    }
}
