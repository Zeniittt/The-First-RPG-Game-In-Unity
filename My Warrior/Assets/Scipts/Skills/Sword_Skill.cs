using UnityEngine;

public enum SwordType
{
    Regular,
    Bounce,
    Pierce,
    Spin
}

public class Sword_Skill : Skill
{
    public SwordType swordType = SwordType.Regular;

    [Header("Bounce Information")]
    [SerializeField] private int amountOfBounce;
    [SerializeField] private float bounceGravity;


    [Header("Skill Information")]
    [SerializeField] private GameObject swordPrefab;
    [SerializeField] private Vector2 launchForce;
    [SerializeField] private float swordGravity;

    private Vector2 finalDirection;

    [Header("Aim dots")]
    [SerializeField] private int numberOfDots;
    [SerializeField] private float spaceBetweenDots;
    [SerializeField] private GameObject dotPrefab;
    [SerializeField] private Transform dotsParent;

    private GameObject[] dots;

    protected override void Start()
    {
        base.Start();

        GenerateDots();
    }

    protected override void Update()
    {
        if (Input.GetKeyUp(KeyCode.Mouse1))
            finalDirection = new Vector2(AimDirection().normalized.x * launchForce.x, AimDirection().normalized.y * launchForce.y);

        /*if(Input.GetKey(KeyCode.Mouse1))
        {
            for(int i = 0; i < dots.Length; i++)
            {
                dots[i].transform.position = DotsPosition(i * spaceBetweenDots); 
            }
        }*/

        if (Input.GetKey(KeyCode.Mouse1))
        {
            if (player.facingDirection == 1)
            {
                for (int i = 0; i < dots.Length; i++)
                {
                    dots[i].transform.position = DotsPosition(i * spaceBetweenDots) + new Vector2(1f, 1f);
                }
            }
            else if (player.facingDirection == -1)
            {
                for (int i = 0; i < dots.Length; i++)
                {
                    dots[i].transform.position = DotsPosition((dots.Length - 1 - i) * spaceBetweenDots) - new Vector2(1f, 1f);
                }
            }
        }
    }

    public void CreateSword()
    {
        /*GameObject newSword = Instantiate(swordPrefab, player.transform.position + new Vector3(1f, 1f, 0), transform.rotation);
        Sword_Skill_Controller newSwordScript = newSword.GetComponent<Sword_Skill_Controller>();

        newSwordScript.SetupSword(finalDirection, swordGravity, player);

        player.AssignNewSword(newSword);

        DotsActive(false);*/

        Vector3 swordPositionOffset = Vector3.zero;

        if (player.facingDirection == 1)
        {
            swordPositionOffset = new Vector3(1f, 1f, 0);
        }
        else if (player.facingDirection == -1)
        {
            swordPositionOffset = new Vector3(-1f, 1f, 0);
        }

        GameObject newSword = Instantiate(swordPrefab, player.transform.position + swordPositionOffset, transform.rotation);
        Sword_Skill_Controller newSwordScript = newSword.GetComponent<Sword_Skill_Controller>();


        if(swordType == SwordType.Bounce)
        {
            swordGravity = bounceGravity;
            newSwordScript.SetupBounce(true, amountOfBounce);
        }


        newSwordScript.SetupSword(finalDirection, swordGravity, player);

        player.AssignNewSword(newSword);

        DotsActive(false);
    }

    public Vector2 AimDirection()
    {
        Vector2 playerPosition = player.transform.position;
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePosition - playerPosition;

        return direction;
    }

    public void DotsActive(bool _isActive)
    {
        for (int i = 0; i < dots.Length; i++)
        {
            dots[i].SetActive(_isActive);
        }
    }

    private void GenerateDots()
    {
        dots = new GameObject[numberOfDots];
        for (int i = 0; i < numberOfDots; i++)
        {
            dots[i] = Instantiate(dotPrefab, player.transform.position, Quaternion.identity, dotsParent);
            dots[i].SetActive(false);
        }
    }

    private Vector2 DotsPosition(float t)
    {
        /*Vector2 position = (Vector2)player.transform.position + new Vector2(
            AimDirection().normalized.x * launchForce.x,
            AimDirection().normalized.y * launchForce.y) * t + .5f * (Physics2D.gravity * swordGravity) * (t * t);

        return position;*/

        Vector2 position = (Vector2)player.transform.position + new Vector2(
                AimDirection().normalized.x * launchForce.x,
                AimDirection().normalized.y * launchForce.y) * t + .5f * (Physics2D.gravity * swordGravity) * (t * t);

        if (player.facingDirection == -1)
        {
            position += new Vector2(0f, 2f); // T?ng v? trí theo chi?u cao
        }

        return position;
    }
}
