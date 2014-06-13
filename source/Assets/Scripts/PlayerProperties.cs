using UnityEngine;
using System.Collections;

public enum CharacterState
{
    Dead,
    Idle,
    IdleShot,
    Walk,
    Run,
    Shot,
    Jump,
    JumpShot
}

[AddComponentMenu("Tools / Scripts / Player Properties")]
public class PlayerProperties : MonoBehaviour
{
    #region Fields

    public CharacterState characterState = CharacterState.Idle;

    public GUISkin newSkin;
    public Texture[] healthBar;

    private const int totalLives = 3;
    private const int totalHealth = 3;

    public int lives = 0;
    public int health = 0;
    public int bodyCount = 0;

    public bool changeCharacter = false;

    //private CharacterController controller;
    //private PlayerControl playerControl;
    public Transform graphics;

    public Material dieMaterial;
    public Material idleMaterial;
    public Material idleShotMaterial;
    public Material walkRunMaterial;
    public Material shotMaterial;
    public Material jumpMaterial;
    public Material jumpShotMaterial;

    private Material lastMaterial;

    #endregion

    #region Methods

    void Start()
    {
        lastMaterial = graphics.renderer.material;

        lives = totalLives;
        health = totalHealth;

        //controller = GetComponent<CharacterController>();
        //playerControl = GetComponent<PlayerControl>();
    }

    void OnChangeScreen(string levelName)
    {
        AutoFade.LoadLevel(levelName, 1.5f, 1.5f, Color.black);
    }

    void Update()
    {
        if (lives <= 0)
        {
            OnChangeScreen("Level1");
        }

        if (changeCharacter)
        {
            SetCharacterState();
        }
    }

    void OnGUI()
    {
        GUI.skin = newSkin;

        float leftPadding = -5;
        float topPadding = -5;

        // HEALTH
        Rect healthRect = new Rect(leftPadding, topPadding, healthBar[health].width, healthBar[health].height);
        GUI.DrawTexture(healthRect, healthBar[health]);

        leftPadding = 62f;
        topPadding = 35f;

        // LIVES
        string livesText = lives.ToString();
        Vector2 sizeLivesHUD = GUI.skin.GetStyle("Label").CalcSize(new GUIContent(livesText));
        Rect livesRect = new Rect(leftPadding, topPadding, sizeLivesHUD.x, sizeLivesHUD.y);
        GUI.Label(livesRect, livesText);

        // BODYCOUNT
        string livesBodyCount = "BODYCOUNT: " + bodyCount.ToString();
        Vector2 sizeBodyCountHUD = GUI.skin.GetStyle("Label").CalcSize(new GUIContent(livesBodyCount));
        Rect bodyCountRect = new Rect(Screen.width - sizeBodyCountHUD.x - leftPadding, topPadding, sizeBodyCountHUD.x, sizeBodyCountHUD.y);
        GUI.Label(bodyCountRect, livesBodyCount);
    }

    public void AddLife(int numberLives)
    {
        lives += numberLives;
    }

    public void AddHealth(int numberHealth)
    {
        if (health > 0)
        {
            health += numberHealth;
        }
    }

    public void AddBodyCount(int numberBodyCount)
    {
        bodyCount += numberBodyCount;
    }

    public void RemoveHealth(int numberLives)
    {
        health -= numberLives;

        if (health <= 0)
        {
            lives--;

            if (lives > 0)
            {
                health = totalHealth;
            }
        }
    }

    private void SetCharacterState()
    {
        switch (characterState)
        {
            case CharacterState.Dead:
                {
                    if (lastMaterial.name != dieMaterial.name)
                    {
                        graphics.renderer.material = dieMaterial;
                    }

                    //Destroy(gameObject);
                    changeCharacter = false;

                    break;
                }
            case CharacterState.Idle:
                {
                    if (lastMaterial.name != idleMaterial.name)
                    {
                        graphics.renderer.material = idleMaterial;
                    }

                    changeCharacter = false;
                    break;
                }
            case CharacterState.IdleShot:
                {
                    if (lastMaterial.name != idleShotMaterial.name)
                    {
                        graphics.renderer.material = idleShotMaterial;
                    }

                    changeCharacter = false;
                    break;
                }
            case CharacterState.Shot:
                {
                    if (lastMaterial.name != shotMaterial.name)
                    {
                        graphics.renderer.material = shotMaterial;
                    }

                    changeCharacter = false;
                    break;
                }
            case CharacterState.Jump:
                {
                    if (lastMaterial.name != jumpMaterial.name)
                    {
                        graphics.renderer.material = jumpMaterial;
                    }

                    changeCharacter = false;
                    break;
                }
            case CharacterState.JumpShot:
                {
                    if (lastMaterial.name != jumpShotMaterial.name)
                    {
                        graphics.renderer.material = jumpShotMaterial;
                    }

                    changeCharacter = false;
                    break;
                }
            case CharacterState.Walk:
            case CharacterState.Run:
            default:
                {
                    if (lastMaterial.name != walkRunMaterial.name)
                    {
                        graphics.renderer.material = walkRunMaterial;
                    }

                    changeCharacter = false;
                    break;
                }
        }

        lastMaterial = graphics.renderer.material;
    }


    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Debug.Log("OI");

        //if (perdeVIda)
        {
            if (hit.transform.tag == ("Enemy"))
            {
                RemoveHealth(1);
                perdeVIda = false;
                Invoke("TimerToRecover", 2f);
            }
        }
    }

    private bool perdeVIda = true;
    void TimerToRecover()
    {
        perdeVIda = true;
    }

    #endregion
}