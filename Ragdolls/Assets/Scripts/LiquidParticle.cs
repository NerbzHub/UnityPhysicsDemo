using UnityEngine;
using System.Collections;

//-------------------------------------------------------------------------------------------
// Class:
//      LiquidParticle: Class contains a circle with multiple states to represent 
//                      different liquid types. Particles will need to scale in size over 
//                      time with scaling effecting overall velocity of the sprite.
//-------------------------------------------------------------------------------------------

[RequireComponent(typeof(Rigidbody2D))]
public class LiquidParticle : MonoBehaviour
{
    // Enum for the different types of liquid states.
    public enum LiquidStates
    {
        Water,
        Lava	//2 States
    };

    //Different liquid types
    LiquidStates currentState = LiquidStates.Water;

    // The two different materials dependent on their type.
    public Material waterMaterial;
    public Material lavaMaterial;

    // The time that they start at.
    float startTime = 0.0f;
    // How long in seconds they last for.
    float particleLifeTime = 3.0f;

    // The different gravity scales that change dependent on the liquid state.
    const float WATER_GRAVITYSCALE = 1.0f;
    const float LAVA_GRAVITYSCALE = 0.3f;

    // The object's rigidbody.
    private Rigidbody2D _rigidbody = null;

    //-------------------------------------------------------------------------------------------
    // This occurrs when the object becomes awake.
    //-------------------------------------------------------------------------------------------
    void Awake()
    {
        // Assigning the rb.
        _rigidbody = GetComponent<Rigidbody2D>();

        // Initializing the start time to 0.
        startTime = 0.0f;
        // Set the state to the current state.
        SetState(currentState);
    }

    //-------------------------------------------------------------------------------------------
    // This runs every frame.
    //-------------------------------------------------------------------------------------------
    void Update()
    {
        // Switch statement to do the right behaviour for which state.
        switch (currentState)
        {
            case LiquidStates.Water:
                MovementAnimation();
                ScaleDown();
                break;
            case LiquidStates.Lava:
                MovementAnimation();
                ScaleDown();
                break;
            default:
                break;
        }
    }

    //-------------------------------------------------------------------------------------------
    // SetState: Change an existing particle to a new type (eg water to lava).
    // 
    //      params:
    //          a_newState: The new particle type to be passed in eg. LiquidStates.Lava.
    //-------------------------------------------------------------------------------------------
    public void SetState(LiquidStates newState)
    {
        // Set the correct material dependent on the state.
        MeshRenderer renderer = gameObject.GetComponent<MeshRenderer>();
        switch (newState)
        {
            case LiquidStates.Lava:
                renderer.material = lavaMaterial;
                break;

            case LiquidStates.Water:
                renderer.material = waterMaterial;
                break;
            default:
                break;
        }
        // Assign the new state as the current state.
        currentState = newState;
        // Reset the particle velocity 
        _rigidbody.velocity = Vector3.zero;
        // If the state changes (eg. water turns to lava) reset the
        // life of the particle.
        startTime = Time.time;
    }


    //-------------------------------------------------------------------------------------
    // MovementAnimation: Scales the particle based on its velocity.
    //-------------------------------------------------------------------------------------
    void MovementAnimation()
    {
        Vector3 movementScale = Vector3.one;
        movementScale.x += _rigidbody.velocity.x / 30.0f;
        movementScale.y += _rigidbody.velocity.y / 30.0f;
        transform.localScale = movementScale;
    }


    //-------------------------------------------------------------------------------------
    // ScaleDown: Scales the size of the particle based on how long it has been alive. 
    //            Gives the impression of a dying particle.
    //-------------------------------------------------------------------------------------
    void ScaleDown()
    {
        // Make the particle appear smaller until eventually it fades away and gets destroyed.
        float scaleValue = 1.0f - ((Time.time - startTime) / particleLifeTime);
        Vector2 particleScale = Vector2.one;
        if (scaleValue <= 0)
        {
            Destroy(gameObject);
        }

        else
        {
            particleScale.x = scaleValue;
            particleScale.y = scaleValue;
            transform.localScale = particleScale;
        }
    }


    //-------------------------------------------------------------------------------------
    // SetLifeTime: Function allows for the external changing of the particles lifetime.
    //  
    //      params:
    //          a_newLifetime: The new time the particle should live for. (eg. 4.0f seconds)
    //-------------------------------------------------------------------------------------
    public void SetLifeTime(float newLifetime)
    {
        particleLifeTime = newLifetime;
    }


    //-------------------------------------------------------------------------------------
    // OnCollisionEnter2D: This is where we would handle collisions between particles and 
    //                     call functions like our setState to change partcle types. 
    //                     Or we could just flat out destroy them etc..
    // 
    //      params:
    //          a_otherParticle: The collision with another particle. Obviously not 
    //                           limited to particles so do a check in the method.
    //-------------------------------------------------------------------------------------
    void OnCollisionEnter2D(Collision2D a_otherParticle)
    {


    }

}
