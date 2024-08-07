using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public ModelMonkey playerGM;
    public List<Rewind> rewinds;
    [Header("Character Swap")]
    public Characters[] players = new Characters[2];
    public GameObject[] camerasPlayers = new GameObject[2];
    private bool _inChange = false;
    private bool _controllerMonkey = true; //Si se puede usar al mono
    public bool ContollerMonkey {  get { return _controllerMonkey; } }
    //[HideInInspector] public Animator _animCamMonkey;
    //[HideInInspector] public Animator _animCamBanana;
    //private Animator _animSeparationCameras;
    //public Animator AnimSeparationCameras { set { _animSeparationCameras = value; } }
    private float _duration;

    //public CameraTracker cam;
    public CameraTarget cam;


    public List<Enemy> enemies = new();

    [Range(0f, 4f)]
    public float weightSeparation, weightAlignment, weightSeek;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    private void Start()
    {

        //AnimatorStateInfo stateInfo = _animSeparationCameras.GetCurrentAnimatorStateInfo(0);
        //_duration = stateInfo.length;

        //Activo los controles del Mono
        _controllerMonkey = true;
        foreach (var item in rewinds)
        {
            item.Save();
        }
        //Desactivo la banana
        players[1].GetComponent<ModelBanana>().enabled = false;
    }



    private void Update()
    {
        if(_controllerMonkey) players[1].GetComponent<ModelBanana>().enabled = false;

        if (Input.GetKeyDown(KeyCode.Q)) Swap();

    }

    public void RemoveAll()
    {
        foreach (var item in rewinds)
        {
            rewinds.Remove(item);
        }
    }

    public void Swap()
    {
        //if(_inChange || !players[1].GetComponent<BananaGuide>().ChangeCharacter) return;

        if(_controllerMonkey)
        {
            SwitchCamBanana();
        }
        else
        {
            SwitchCamMonkey();
        }
    }


    void SwitchCamBanana()
    {
        //Time.timeScale = 0;
        _inChange = true;
        //_animCamMonkey.SetTrigger("Exit");
        //camerasPlayers[1].GetComponent<Camera>().enabled = true;
        //_animCamBanana.SetTrigger("Enter");
        //_animSeparationCameras.SetTrigger("Enter");
        _controllerMonkey = false;
        //yield return new WaitForSecondsRealtime(1);

        //camerasPlayers[0].GetComponent<Camera>().enabled = false;
        cam.Target = players[1].transform;
        players[1].GetComponent<BananaGuide>().enabled = false;
        players[1].GetComponent<ModelBanana>().enabled = true;
        //players[1].GetComponent<ModelBanana>().ActivarVisor();

        //Time.timeScale = 1;

        //yield return new WaitForSecondsRealtime(_duration);
        _inChange = false;

    }

    void SwitchCamMonkey()
    {
        //Time.timeScale = 0;
        players[1].GetComponent<ModelBanana>().enabled = false;
        _inChange = true;
        //_animCamBanana.SetTrigger("Exit");
        //camerasPlayers[0].GetComponent<Camera>().enabled = true;
        //_animCamMonkey.SetTrigger("Enter");
        //_animSeparationCameras.SetTrigger("Exit");
        //yield return new WaitForSecondsRealtime(1);

        _controllerMonkey = true;
        //camerasPlayers[1].GetComponent<Camera>().enabled = false;

        cam.Target = players[0].transform;

        players[1].GetComponent<BananaGuide>().enabled = true;
        //players[1].GetComponent<ModelBanana>().enabled = false;

        //Time.timeScale = 1;
        //yield return new WaitForSecondsRealtime(_duration);
        _inChange = false;
    }

    
}
