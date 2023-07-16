using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using System.IO;
using TMPro;
using UnityEngine.UI;
using System.Threading.Tasks;

public class MenuController : MonoBehaviour
{
    //Constantes de menu
    public int play = 1;

    public static MenuController instance = null;
    private int previousSceneIndex;
    private string sceneName;

    private Button btnPlay;
    private Button btnCreditos;
    private Button btnQuit;

    private Button avancar;
    private Button sair;

    private Button continuarBtn;
    private Button configuracoesBtn;
    private Button menuInicialBtn;
    private Button menuDePauseBtn;

    private GameObject configMenuPausa;
    private GameObject menuPausa;
    private GameObject buttonContinuar;

    public GameObject pause;

    //private AudioController controleDeAudio;
    public int indexCena;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        //menuDePauseBtn = GameObject.Find("VoltarBtn").GetComponent<Button>();
        //continuarBtn = GameObject.Find("ContinuarBtn").GetComponent<Button>();
        configuracoesBtn = GameObject.Find("ConfiguracoesBtn").GetComponent<Button>();
        //menuInicialBtn = GameObject.Find("MenuBtn").GetComponent<Button>();

        configMenuPausa = GameObject.Find("configMenu");
        //menuPausa = GameObject.Find("pauseMenu");
        //buttonContinuar = GameObject.Find("ContinuarBtn");

        //pause.SetActive(false);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    //Define coisas para cenas especificas quando estas sao carregadas - util demais
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        sceneName = SceneManager.GetActiveScene().name;
        if (scene.name == "Derrota" || scene.name == "Vitoria")
        {
            // Encontra o bot?o pelo nome ou atrav?s de uma busca na hierarquia
            avancar = GameObject.Find("btn1").GetComponent<Button>();
            sair = GameObject.Find("btn2").GetComponent<Button>();
            avancar.onClick.AddListener(loadScene);
            sair.onClick.AddListener(mainMenu);

            if (scene.name == "Placar" && previousSceneIndex == 17)
            {
                GameObject.Find("btn1").SetActive(false);
                GameObject buttonSair = GameObject.Find("btn2");
                EventSystem.current.SetSelectedGameObject(buttonSair);
            }
        }


        if (scene.name == "MenuInicial")
        {
            btnPlay = GameObject.Find("PlayBtn").GetComponent<Button>();
            //PlayerPrefs.SetInt("FaseAtual", 0);
            int save = PlayerPrefs.GetInt("FaseAtual");
            if (save != 0)
            {
                play = save;
                btnPlay.GetComponentInChildren<TextMeshProUGUI>().text = "Continuar";
            }
            else
            {
                btnPlay.GetComponentInChildren<TextMeshProUGUI>().text = "Novo Jogo";
            }
            btnPlay.onClick.AddListener(PlayGame);
            btnQuit = GameObject.Find("ExitBtn").GetComponent<Button>();
            btnQuit.onClick.AddListener(QuitGame);
            btnCreditos = GameObject.Find("CreditosBtn").GetComponent<Button>();
        }


        //setar menu de pause
        if (sceneName != "MenuInicial" && sceneName != "Derrota" && sceneName != "Vitoria" && sceneName != "Placar")
        {
            //Salva fase atual pra continuar
            if (sceneName != "MLevelOne" && sceneName != "MLevelTwo" && sceneName != "MLevelTree" && sceneName != "MLevelFour")
            {
                PlayerPrefs.SetInt("FaseAtual", SceneManager.GetActiveScene().buildIndex);
            }
            //controleDeAudio = GameObject.Find("AudioController").GetComponent<AudioController>();
            //pause.SetActive(true);
            configMenuPausa.SetActive(true);
            GameObject volume = GameObject.Find("SliderVolume");
            Slider slider = volume.GetComponent<Slider>();
            //slider.value = controleDeAudio.GetVolume();

            //while (!pause.activeSelf && !buttonContinuar.activeSelf)
            //{ // Aguarda ate que o menu de pause esteja ativo

            //}
            //menuPause(buttonContinuar);
            //continuarBtn.onClick.AddListener(resume);
            //configuracoesBtn.onClick.AddListener(configPause);
            //menuInicialBtn.onClick.AddListener(mainMenu);
            //menuDePauseBtn.onClick.AddListener(menuPause);
            //configMenuPausa.SetActive(false);
            //pause.SetActive(false);
        }

    }


    // Update is called once per frame
    void Update()
    {

        //Coloca o jogo em pause
        //if (sceneName != "MenuInicial" && sceneName != "Derrota" && sceneName != "Vitoria")
        //{
        //    float pausar = Input.GetAxisRaw("Pause");
        //    if (pausar > 0)
        //    {
        //        pause.SetActive(true);
        //        EventSystem.current.SetSelectedGameObject(buttonContinuar);
        //        Time.timeScale = 0f;
        //        menuPause();
        //    }
        //    //B pra voltar
        //    if (pause.activeSelf == true)
        //    {
        //        float back = Input.GetAxisRaw("Fire2");
        //        if (back > 0)
        //        {
        //            GameObject configDePausa = GameObject.Find("configMenu");
        //            if (configDePausa != null)
        //            {
        //                configMenuPausa.SetActive(false);
        //                menuPausa.SetActive(true);
        //            }
        //            resume();
        //        }
        //    }

        //}

        if (sceneName == "MenuInicial")
        {
            //B pra voltar
            float back = Input.GetAxisRaw("Fire2");
            if (back > 0)
            {
                // && ConfiguracoesMenu.activeSelf
                GameObject menuPrincipal = GameObject.Find("UIController");
                UIController uiController = menuPrincipal.GetComponent<UIController>();
                uiController.menu();
                uiController.selectMain();
            }
        }

        if (sceneName == "Derrota" || sceneName == "Vitoria" )
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                GameObject button = GameObject.Find("btn1");
                EventSystem.current.SetSelectedGameObject(button);
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                GameObject button = GameObject.Find("btn2");
                EventSystem.current.SetSelectedGameObject(button);
            }

            float A = Input.GetAxisRaw("Fire1");
            float B = Input.GetAxisRaw("Fire2");

            if (A > 0)
            {
                //Reinicia ou avanca fase
                loadScene();
            }
            else if (B > 0)
            {
                //Vai pro menu
                mainMenu();
            }
        }
    }
    public void loadScene()
    {
        if (SceneManager.GetActiveScene().name.Equals("Vitoria"))
        {
            SceneManager.LoadScene(previousSceneIndex + 1);
        }
    }

    public void PreviousScene(int index)
    {
        previousSceneIndex = index;
    }

    public void mainMenu()
    {
        if (SceneManager.GetActiveScene().name.Equals("Vitoria"))
        {
            PlayerPrefs.SetInt("FaseAtual", PlayerPrefs.GetInt("FaseAtual") + 1);
        }
        //resume();
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void resume()
    {
        pause.SetActive(false);
        Time.timeScale = 1f;
    }

    public void configPause()
    {
        GameObject button = GameObject.Find("VolumeBtn");
        EventSystem.current.SetSelectedGameObject(button);
    }

    public void menuPause(GameObject buttonContinuar)
    {
        GameObject button = GameObject.Find("ContinuarBtn");
        EventSystem.current.SetSelectedGameObject(buttonContinuar);
    }

    public void menuPause()
    {
        GameObject button = GameObject.Find("ContinuarBtn");
        EventSystem.current.SetSelectedGameObject(button);
    }

    //public async void FadeOut()
    //{
    //    GameObject.Find("CanvasFade").GetComponent<Canvas>().sortingOrder = 5;
    //    Animator animator = GameObject.Find("ImageFadeOut").GetComponent<Animator>();
    //    animator.SetBool("isFadeOut", true);
    //    await Task.Delay(1000);
    //    PlayGame();
    //}

    public void PlayGame()
    {
        SceneManager.LoadScene(play, LoadSceneMode.Single);
        SceneManager.LoadScene(play);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
