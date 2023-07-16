using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    private GameObject botaoAtual;
    public GameObject MainMenu;
    public GameObject ConfiguracoesMenu;
    public GameObject CreditosMenu;
    public bool telaDeEstadoFinal;
    public float volumeSlider = 0.8f;

    //private AudioController audioBtn;


    //Cores
    Color corBase;
    Color corDeSelecao;
    //InputController entrada = null;

    private Color colorRGB(int r, int g, int b)
    {
        return new Color(r / 255f, g / 255f, b / 255f);
    }

    void Start()
    {
        corBase = colorRGB(180, 180, 180);
        corDeSelecao = colorRGB(255, 255, 255);
        if (MainMenu == null)
        {
            return;
        }
        menu();
    }

    public void menu()
    {
        MainMenu.SetActive(true);
        ConfiguracoesMenu.SetActive(false);
        CreditosMenu.SetActive(false);
    }

    public void selectMain()
    {
        GameObject button = GameObject.Find("PlayBtn");
        EventSystem.current.SetSelectedGameObject(button);
    }

    public void selectOpcoes()
    {
        GameObject volume = GameObject.Find("SliderVolume");
        EventSystem.current.SetSelectedGameObject(volume);
        Slider slider = volume.GetComponent<Slider>();
        //slider.value = audioBtn.GetVolume();
    }

    public void selectPlay()
    {
        GameObject button = GameObject.Find("PlayBtn");
        EventSystem.current.SetSelectedGameObject(button);
    }

    public void selectCredits()
    {
        GameObject button = GameObject.Find("VoltarBtn3");
        EventSystem.current.SetSelectedGameObject(button);
    }

    public void hover(GameObject btn)
    {
        EventSystem.current.SetSelectedGameObject(btn);
    }

    private void select()
    {
        GameObject btnSelected = EventSystem.current.currentSelectedGameObject;

        //Corrige clique na tela
        if (btnSelected == null && botaoAtual != null)
        {
            btnSelected = botaoAtual;
            EventSystem.current.SetSelectedGameObject(btnSelected);
        }

        //Destaca opcao
        if (botaoAtual != btnSelected)
        {

            //Slider e Volume
            if (btnSelected != null && btnSelected.name == "SliderVolume")
            {
                btnSelected = btnSelected.transform.parent.gameObject;
                Slider slider = FindObjectOfType<Slider>();

                //Regula volume da cena
                if (slider.value != volumeSlider)
                {
                    volumeSlider = slider.value;
                    //audioBtn.ControlaVolume(volumeSlider);
                }
            }
            else
            {
                //Aqui vem o som de troca
                if (botaoAtual != null)
                {
                    //audioBtn.MoveButton();
                }
            }

            Button[] btns = FindObjectsOfType<Button>();
            //TextMeshProUGUI[] btns = FindObjectsOfType<TextMeshProUGUI>();
            //Debug.Log(btns.Length);

            //Icone de espada
            if (!telaDeEstadoFinal)
            {
                foreach (Button btn in btns)
                {
                    TextMeshProUGUI btnText = btn.GetComponentInChildren<TextMeshProUGUI>();
                    btnText.color = corBase;
                }
                //Debug.Log(btnSelected + " was selected");
                TextMeshProUGUI texto = btnSelected.GetComponentInChildren<TextMeshProUGUI>();
                texto.color = corDeSelecao;
                botaoAtual = btnSelected;
            }
            else
            {
                //if (entrada.gamepadOn == false)
                //{
                    foreach (Button btn in btns)
                    {
                        TextMeshProUGUI btnText = btn.GetComponentInChildren<TextMeshProUGUI>();
                        btnText.color = corBase;
                    }
                    //Debug.Log(btnSelected + " was selected");
                    TextMeshProUGUI texto = btnSelected.GetComponentInChildren<TextMeshProUGUI>();
                    texto.color = corDeSelecao;
                //}
                //else
                //{
                //    foreach (Button btn in btns)
                //    {
                //        TextMeshProUGUI btnText = btn.GetComponentInChildren<TextMeshProUGUI>();
                //        btnText.color = corDeSelecao;
                //    }
                //}
            }
        }
    }

    private void clique()
    {
        //audioBtn = GameObject.Find("AudioController").GetComponent<AudioController>();
        //audioBtn.Click();
    }

    void Update()
    {
        //audioBtn = GameObject.Find("AudioController").GetComponent<AudioController>();
        //if (entrada == null)
        //{
        //    entrada = FindObjectOfType<InputController>();
        //}

        //Debug.Log(tela.name);
        select();

        Button[] botoes = FindObjectsOfType<Button>();
        foreach (Button btn in botoes)
        {
            btn.onClick.AddListener(clique);
        }
    }
}
