using UnityEngine;

public class ConveyorController : MonoBehaviour
{
    [Header("Object")]
    [SerializeField] private PoolView _object;
    [Header("Conveyor Speed")]
    [SerializeField] private ConveyorMove[] moves;
    [SerializeField] private float movespeed;
    [Header("Factory")]
    [SerializeField] private int howManyFactories = 0;
    private Factory[] _Factories;
    [SerializeField] private int Price;
    [SerializeField] private float TimeBFrNext;
    [SerializeField] private Transform[] _spawnposes;
    private float TBNT;
    [Header("FactoryAnimations")]
    [SerializeField] private Animator[] spawnAnimators;
    private bool AS;
    [SerializeField] private float TimeBFrAnim = 1.9f;
    private float TBAE;

    void Awake()
    {
        _Factories = new Factory[howManyFactories];
        howManyFactories = 0;
        foreach (var move in moves)
        {
            move.SetSpeed(movespeed);
        }
    }

    private void Update()
    {
        if (TBNT > 0)
        {
            TBNT -= Time.deltaTime;
        }
        else
        {
            if (AS == false)
            {
                foreach (var factory in spawnAnimators)
                {
                    if (factory.isActiveAndEnabled == true)
                    {
                        factory.SetTrigger("spawn");
                        AS = true;
                    }
                }
            }
            else
            {
                if (TBAE > 0)
                {
                    TBAE -= Time.deltaTime;
                }
                else
                {
                    foreach (var factory in _Factories)
                    {
                        if (factory != null)
                        {
                            factory.Give();
                        }
                    }
                    AS = false;
                    TBAE = TimeBFrAnim;
                    TBNT = TimeBFrNext;
                }
            }
        }
    }

    private void ReturnAction(PoolView pool)
    {
        pool.gameObject.SetActive( false );
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        foreach (var pose in _spawnposes)
        {
            Gizmos.DrawSphere(pose.position, 0.5f);
        }
    }

    public void AddFactory()
    {
        if (howManyFactories < _Factories.Length)
        {
            howManyFactories++;
            _Factories[howManyFactories - 1] = new StandartFactory(_object, ReturnAction);
            _Factories[howManyFactories - 1].SetPrice(Price);
            _Factories[howManyFactories - 1].SetSpawnpos(_spawnposes[howManyFactories - 1]);
        }
    }
}
