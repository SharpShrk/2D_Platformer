using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]

public class PlayerMovement : MonoBehaviour
{
    public float MinGroundNormalY = .65f;
    public float GravityModifier = 1f;
    public Vector2 Velocity;
    public LayerMask LayerMask;

    protected Vector2 targetVelocity;
    protected bool grounded;
    protected Vector2 groundNormal;
    protected Rigidbody2D rigidbody2D;
    protected ContactFilter2D contactFilter;
    protected RaycastHit2D[] hitBuffer = new RaycastHit2D[16];
    protected List<RaycastHit2D> hitBufferList = new List<RaycastHit2D>(16);

    protected const float minMoveDistance = 0.001f;
    protected const float shellRadius = 0.01f;

    private float _speed = 5f;
    private Animator _animation;

    void OnEnable()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        _animation = GetComponent<Animator>();
    }

    void Start()
    {
        contactFilter.useTriggers = false;
        contactFilter.SetLayerMask(LayerMask);
        contactFilter.useLayerMask = true;
    }

    void Update()
    {
        targetVelocity = new Vector2(Input.GetAxis("Horizontal"), 0);

        if (Input.GetKey(KeyCode.Space) && grounded)
        {
            Velocity.y = 7;           
        }
        
        if(grounded == false)
        {
            _animation.SetBool("IsGround", false);
        }
        else
        {
            _animation.SetBool("IsGround", true);
        }

        if(targetVelocity.x > 0)
        {
            _animation.SetBool("IsRunning", true);
            rigidbody2D.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if(targetVelocity.x < 0)
        {
            _animation.SetBool("IsRunning", true);
            rigidbody2D.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            _animation.SetBool("IsRunning", false);
        }
    }

    void FixedUpdate()
    {
        Velocity += GravityModifier * Physics2D.gravity * Time.deltaTime;
        Velocity.x = targetVelocity.x;

        grounded = false;

        Vector2 deltaPosition = Velocity * Time.deltaTime;
        Vector2 moveAlongGround = new Vector2(groundNormal.y, -groundNormal.x);
        Vector2 move = moveAlongGround * deltaPosition.x *_speed;

        Movement(move, false);

        move = Vector2.up * deltaPosition.y;

        Movement(move, true);
    }

    void Movement(Vector2 move, bool yMovement)
    {
        float distance = move.magnitude;

        if (distance > minMoveDistance)
        {
            int count = rigidbody2D.Cast(move, contactFilter, hitBuffer, distance + shellRadius);

            hitBufferList.Clear();

            for (int i = 0; i < count; i++)
            {
                hitBufferList.Add(hitBuffer[i]);
            }

            for (int i = 0; i < hitBufferList.Count; i++)
            {
                Vector2 currentNormal = hitBufferList[i].normal;

                if (currentNormal.y > MinGroundNormalY)
                {
                    grounded = true;
                    if (yMovement)
                    {
                        groundNormal = currentNormal;
                        currentNormal.x = 0;
                    }
                }

                float projection = Vector2.Dot(Velocity, currentNormal);

                if (projection < 0)
                {
                    Velocity = Velocity - projection * currentNormal;
                }

                float modifiedDistance = hitBufferList[i].distance - shellRadius;
                distance = modifiedDistance < distance ? modifiedDistance : distance;
            }
        }

        rigidbody2D.position = rigidbody2D.position + move.normalized * distance;
    }
}