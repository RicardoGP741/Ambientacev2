using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemyBehaviur : MonoBehaviour
{
    //Variables de componentes
    private Rigidbody2D EntityPhysics; 
    private Collider2D EntityCollider; 
    [SerializeField] private  LayerMask ground; 

    private Animator EntityAnim; 

    //Variables de comportamiento
     private float pointleft; 
     private float pointright; 
    [SerializeField] private float jumpheight; 
    [SerializeField] private float jumpwidth; 

  
    
    public bool facingleft= true; 
    
    // Start is called before the first frame update
    void Start()
    {
        EntityPhysics = GetComponent<Rigidbody2D>(); 
        EntityCollider = GetComponent<Collider2D>(); 
        EntityAnim = GetComponent<Animator>(); 

        pointleft = transform.position.x - 6;  //GameObject.Find("PointLeft").transform.position.x; 
        pointright = transform.position.x + 6;

        jumpwidth = 2; 
        jumpheight = 6; 

        
    }

    // Update is called once per frame
    void Update()
    {
        if(!(EntityPhysics.IsTouchingLayers(ground))){
            if(EntityPhysics.velocity.y < .1f){
                EntityAnim.SetBool("Jump", false); 
                EntityAnim.SetBool("Fall", true);     
            }else{
                EntityAnim.SetBool("Jump", true); 
            }
        }else{
            EntityAnim.SetBool("Fall", false); 
        }
    }

    private void moves()
    {

        if (facingleft)
        {

            if (transform.position.x > pointleft)
            {
                               
                if (EntityCollider.IsTouchingLayers(ground) )
                {
                    transform.localScale = new Vector3(1, 1, 1);
                    EntityPhysics.velocity = new Vector2(jumpwidth * -1, jumpheight);
                }
            }
            else
            {
                facingleft = false;
            }
        }
        else
        {
            if (transform.position.x < pointright)
            {
                if (EntityCollider.IsTouchingLayers(ground))
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                    EntityPhysics.velocity = new Vector2(jumpwidth, jumpheight);
                }
            }
            else
            {
                facingleft = true;
            }
        }
        //EntityPhysics.velocity = new Vector2(0,  EntityPhysics.velocity.y); 
    }

}
