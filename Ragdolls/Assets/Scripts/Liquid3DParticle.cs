using UnityEngine;
using System.Collections;

//-------------------------------------------------------------------------------------
// Class:
//      LiquidParticle:
//          Class contains a circle with multiple states to represent different liquid types.
//          Particles will need to scale in size over time with scaling effecting overall velocity of the sprite.
//          Code has been provided to get you started. You will need to fill in the missing information from each function.
//-------------------------------------------------------------------------------------

public class Liquid3DParticle : MonoBehaviour
{
    //-------------------------------------------------------------------------------------
    // This ENUM stores whether the liquid is lava or water.
    //-------------------------------------------------------------------------------------
    public enum LiquidStates
    {
        Water,
        Lava	//2 States
    };

    //-------------------------------------------------------------------------------------
    // Setting the current state to water.
    //-------------------------------------------------------------------------------------
    LiquidStates currentState = LiquidStates.Water;

    //-------------------------------------------------------------------------------------
    // Different liquid types.
    //-------------------------------------------------------------------------------------
    public Material waterMaterial;
    public Material lavaMaterial;

    //-------------------------------------------------------------------------------------
    // Start time of the particle.
    //-------------------------------------------------------------------------------------
    float startTime = 0.0f;
    //-------------------------------------------------------------------------------------
    // How long in seconds the particle will last for.
    //-------------------------------------------------------------------------------------
    float particleLifeTime = 3.0f;

    //-------------------------------------------------------------------------------------
    // The different gravity scales dependent on their liquidStates.
    //-------------------------------------------------------------------------------------
    const float WATER_GRAVITYSCALE = 1.0f;
    const float LAVA_GRAVITYSCALE = 0.3f;

    //-------------------------------------------------------------------------------------
    // storing the rigidBody in a variable for ease of use.
    //-------------------------------------------------------------------------------------
    private Rigidbody _rigidbody = null;

    //-------------------------------------------------------------------------------------
    // This runs whenthis game object is awake.
    //-------------------------------------------------------------------------------------
    void Awake()
    {
        // Setting the rigidbody to store the correct rigidbody.
        _rigidbody = GetComponent<Rigidbody>();

        // Initialize the start time to 0
        startTime = 0.0f;

        // Set the state to the current state.
        SetState(currentState);
    }

    //-------------------------------------------------------------------------------------
    // This runs every frame.
    //-------------------------------------------------------------------------------------
    void Update()
    {

        //-------------------------------------------------------------------------------------
        // Does different code dependent on which liquid state is in use.
        //-------------------------------------------------------------------------------------
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

    //-------------------------------------------------------------------------------------
    // SetState:  Change an existing particle to a new type (eg water to lava)
    //      params:
    //          newState: The new particle type to be passed in eg. LiquidStates.Lava 
    //------------------------------------------------------------------------------------- 
    public void SetState(LiquidStates newState)
    {
        // Getting the mesh renderer.
        MeshRenderer renderer = gameObject.GetComponent<MeshRenderer>();

        // Assign the correct material for each state.
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
        movementScale.z += _rigidbody.velocity.z / 30.0f;
        transform.localScale = movementScale;
    }

    //-------------------------------------------------------------------------------------
    // ScaleDown: Scales the size of the particle based on how long it has been alive. 
    //            Gives the impression of a dying particle.
    //-------------------------------------------------------------------------------------
    void ScaleDown()
    {
        // Makes the particle appear smaller gradually until it disappears.
        float scaleValue = 1.0f - ((Time.time - startTime) / particleLifeTime);
        Vector3 particleScale = Vector3.one;
        // Once the particle is too small to see, destroy it.
        if (scaleValue <= 0)
        {
            Destroy(gameObject);
        }

        else
        {
            particleScale.x = scaleValue;
            particleScale.y = scaleValue;
            particleScale.z = scaleValue;
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
    void OnCollisionEnter(Collision a_otherParticle)
    {


    }

}
