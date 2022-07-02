using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //Test

public class PlayerControler : MonoBehaviour
{


    //Variables de unity
    private Rigidbody2D rb; 
    private Animator anim; //Cambiable
    private Collider2D colliderPlayer; 
    [SerializeField] private LayerMask ground; 

    [SerializeField] private Text text;  //Test

    //Variables de estado (infinite state machine)
    private enum States {idle, run, jump, falling} //States of character
    private States state = States.idle; //Actual State

    //Variables UI      //test
     public int collecct;  //test

    
    //Variables de comportamiento
    [SerializeField] private float Speed; //velocity of character
    [SerializeField] private float JumpForce;  
    [SerializeField] private float fall; 
    private  float horizontalmove; //move of character right and left 

 

    void Start()
    {
        //Inicializacion de variables de comportamiento
        fall = 8; 
        Speed = 5;  
        JumpForce = 15f;        

        //Inicializacion de variables de componentes
        rb = GetComponent<Rigidbody2D>();   
        anim = GetComponent<Animator>(); 
        colliderPlayer = GetComponent<Collider2D>();
        text.text = "0"; 
        
    }
    
    void Update()
    {
        ManageInputMovement(); 
        StateCharacter(); 
        anim.SetInteger("state", (int)state); 

    }

    

    private void ManageInputMovement() //Moves of characters
    {
        horizontalmove = Input.GetAxis("Horizontal");  // (left right)  edit -> project settings -> InputManager Horizontal        
        if (horizontalmove < 0)
        { //move left
            rb.velocity = new Vector2(-Speed, rb.velocity.y);
            transform.localScale = new Vector2(-1, 1);

        }
        else if (horizontalmove > 0)
        { // move right 
            rb.velocity = new Vector2(Speed, rb.velocity.y);
            transform.localScale = new Vector2(1, 1);

        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

        //Jump
        if (Input.GetKeyDown(KeyCode.W) && colliderPlayer.IsTouchingLayers(ground))
        { 
            rb.velocity = new Vector2(rb.velocity.x, JumpForce); //rb.AddForce(Vector2.up * 15, ForceMode2D.Impulse); 
            state = States.jump;
        }
 
        //Better Jump //Esto es opcional, solo permite definir la intensidad del salto por medio de la pulsacion de tecla
        if(!Input.GetKey(KeyCode.W)  && rb.velocity.y >0){ 
            rb.velocity += (Vector2.up * Physics2D.gravity * fall * Time.deltaTime); 
        }
    }

    private void StateCharacter(){ //MoveAnimationState


        if(state == States.jump ){ //Iniciar saltando
            if(rb.velocity.y < 2){
                state = States.falling; //Justo cuando deja de elevarse
            }
        }else if (state == States.falling){
            if(colliderPlayer.IsTouchingLayers(ground)){
                state = States.idle; //Cuando llega al suelo
            }
        }
        else if((Mathf.Abs(rb.velocity.x) > Mathf.Epsilon) && colliderPlayer.IsTouchingLayers(ground) ){
            state = States.run; //Correr
        }else if(!colliderPlayer.IsTouchingLayers(ground)){
            state = States.falling; //Caer cuando no hay suelo
        }else{
            state = States.idle; //Parado
        }
    }
    

    ////////////////////////////////////////////////Test//////////////////////////////////////////
    //Lineas de codigo fuera de este segmento test que tengan al final de la linea 'test' pueden ser borradas junto con estas
    

   
    private void OnTriggerEnter2D(Collider2D colliderObject){
        if(colliderObject.tag == "Collectable"){
            Destroy(colliderObject.gameObject);
            collecct++; 
            text.text = collecct.ToString(); 
        }
    }


}
