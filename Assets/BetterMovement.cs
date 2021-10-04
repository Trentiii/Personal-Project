using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BetterMovement : MonoBehaviour
{
    public Animator animator;

    public float coolDownTime = 0.75f;
    float timeOfSceneEnter;

    public SpriteRenderer sR;
    public Sprite newSprite;
    public bool objectToggle;
    [SerializeField]
    LayerMask groundLayer;
    [SerializeField]
    float fJumpVelocity = 5;

    Rigidbody2D rigid;

    float fJumpPressedRemember = 0;
    [SerializeField]
    float fJumpPressedRememberTime = 0.2f;

    

    float fGroundedRemember = 0;
    [SerializeField]
    float fGroundedRememberTime = 0.25f;

    [SerializeField]
    float velocityScaler = 1;

    [SerializeField]
    float fHorizontalAcceleration = 1;
    [SerializeField]
    [Range(0, 1)]
    float fHorizontalDampingBasic = 0.5f;
    [SerializeField]
    [Range(0, 1)]
    float fHorizontalDampingWhenStopping = 0.5f;
    [SerializeField]
    [Range(0, 1)]
    float fHorizontalDampingWhenTurning = 0.5f;

    [SerializeField]
    [Range(0, 1)]
    float fCutJumpHeight = 0.5f;

    void Start()
    {
        sR = gameObject.GetComponent<SpriteRenderer>();

        rigid = GetComponent<Rigidbody2D>();
        Debug.Log("RVelocity" + GameManager.playerVelocity);
        rigid.velocity = GameManager.playerVelocity;

        timeOfSceneEnter = Time.time;
    }

    void ChangeSprite()
    {
        sR.sprite = newSprite;
    }

    void Update()
    {
        Vector2 v2GroundedBoxCheckPosition = (Vector2)transform.position + new Vector2(0, 0.7f);
        Vector2 v2GroundedBoxCheckScale = (Vector2)transform.localScale + new Vector2(-3, 3);
        bool bGrounded = Physics2D.OverlapBox(v2GroundedBoxCheckPosition, v2GroundedBoxCheckScale, 3, groundLayer);
        

        fGroundedRemember -= Time.deltaTime;
        if (bGrounded)
        {
            fGroundedRemember = fGroundedRememberTime;
        }

        fJumpPressedRemember -= Time.deltaTime;
        if (Input.GetButtonDown("Jump"))
        {
            fJumpPressedRemember = fJumpPressedRememberTime;
        }

        if (Input.GetButtonUp("Jump"))
        {
            if (rigid.velocity.y > 0)
            {
                rigid.velocity = new Vector2(rigid.velocity.x, rigid.velocity.y * fCutJumpHeight);
            }
        }

        if ((fJumpPressedRemember > 0) && (fGroundedRemember > 0))
        {
            fJumpPressedRemember = 0;
            fGroundedRemember = 0;
            rigid.velocity = new Vector2(rigid.velocity.x, fJumpVelocity);
        }

        float fHorizontalVelocity = rigid.velocity.x;
        fHorizontalVelocity += Input.GetAxisRaw("Horizontal");

        if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) < 0.01f)
            fHorizontalVelocity *= Mathf.Pow(1f - fHorizontalDampingWhenStopping, Time.deltaTime * 10f);
        else if (Mathf.Sign(Input.GetAxisRaw("Horizontal")) != Mathf.Sign(fHorizontalVelocity))
            fHorizontalVelocity *= Mathf.Pow(1f - fHorizontalDampingWhenTurning, Time.deltaTime * 10f);
        else
            fHorizontalVelocity *= Mathf.Pow(1f - fHorizontalDampingBasic, Time.deltaTime * 10f);



        rigid.velocity = new Vector2(fHorizontalVelocity * velocityScaler, rigid.velocity.y);

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            ObjectEnabling.disabled = true;
            objectToggle = !objectToggle;
        }







        if (Input.GetKeyDown(KeyCode.LeftShift) && (Time.time - timeOfSceneEnter > coolDownTime))
        {

            GameManager.playerLoc = transform.position;
            GameManager.playerVelocity = rigid.velocity;
            Debug.Log(GameManager.playerVelocity);
            //Debug.Log(GameManager.playerLoc);
            //SceneManager.LoadScene(GameManager.nextScene);
            SceneChanger.LoadScene(GameManager.nextScene);
        }

        animator.SetFloat("Speed", Mathf.Abs(fHorizontalVelocity));

    }
    


     
}