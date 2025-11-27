using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class EstadoBotao : MonoBehaviour
{
    private readonly Color corAtivo = new Color32(0xD1, 0x00, 0x00, 0xFF);
    private readonly Color corInativo = new Color32(0x65, 0x30, 0x30, 0xFF);

    [Header("Botões de Tema")]
    [SerializeField] private Button Matematica;
    [SerializeField] private Button Historia;
    [SerializeField] private Button Geografia;
    [SerializeField] private Button Ingles;
    [SerializeField] private Button Biologia;
    [SerializeField] private Button Fisica;
    [SerializeField] private Button Quimica;
    [SerializeField] private Button Sociologia;
    [SerializeField] private Button Filosofia;

    // Estado interno de cada botão
    private bool matAtivo = true;
    private bool hisAtivo = true;
    private bool geoAtivo = true;
    private bool ingAtivo = true;
    private bool bioAtivo = true;
    private bool fisAtivo = true;
    private bool quiAtivo = true;
    private bool socAtivo = true;
    private bool filAtivo = true;

    private void Start()
    {
        // Sempre inicializa com as cores corretas
        SetCor(Matematica, matAtivo);
        SetCor(Historia, hisAtivo);
        SetCor(Geografia, geoAtivo);
        SetCor(Ingles, ingAtivo);
        SetCor(Biologia, bioAtivo);
        SetCor(Fisica, fisAtivo);
        SetCor(Quimica, quiAtivo);
        SetCor(Sociologia, socAtivo);
        SetCor(Filosofia, filAtivo);
    }

    // Função genérica para mudar cor do botão
    private void SetCor(Button btn, bool ativo)
    {
        if (btn == null) return;

        Image img = btn.image; // cor do botão
        if (img != null)
            img.color = ativo ? corAtivo : corInativo;
    }

    // Agora cada alternância também atualiza o TemaManager

    public void AlternarMatematica()
    {
        matAtivo = !matAtivo;
        SetCor(Matematica, matAtivo);
        TemaManager.Instance.SetMateria("Matemática", matAtivo);
    }

    public void AlternarHistoria()
    {
        hisAtivo = !hisAtivo;
        SetCor(Historia, hisAtivo);
        TemaManager.Instance.SetMateria("História", hisAtivo);
    }

    public void AlternarGeografia()
    {
        geoAtivo = !geoAtivo;
        SetCor(Geografia, geoAtivo);
        TemaManager.Instance.SetMateria("Geografia", geoAtivo);
    }

    public void AlternarIngles()
    {
        ingAtivo = !ingAtivo;
        SetCor(Ingles, ingAtivo);
        TemaManager.Instance.SetMateria("Inglês", ingAtivo);
    }

    public void AlternarBiologia()
    {
        bioAtivo = !bioAtivo;
        SetCor(Biologia, bioAtivo);
        TemaManager.Instance.SetMateria("Biologia", bioAtivo);
    }

    public void AlternarFisica()
    {
        fisAtivo = !fisAtivo;
        SetCor(Fisica, fisAtivo);
        TemaManager.Instance.SetMateria("Física", fisAtivo);
    }

    public void AlternarQuimica()
    {
        quiAtivo = !quiAtivo;
        SetCor(Quimica, quiAtivo);
        TemaManager.Instance.SetMateria("Química", quiAtivo);
    }

    public void AlternarSociologia()
    {
        socAtivo = !socAtivo;
        SetCor(Sociologia, socAtivo);
        TemaManager.Instance.SetMateria("Sociologia", socAtivo);
    }

    public void AlternarFilosofia()
    {
        filAtivo = !filAtivo;
        SetCor(Filosofia, filAtivo);
        TemaManager.Instance.SetMateria("Filosofia", filAtivo);
    }
    
    // Ainda disponível caso queira consultar diretamente
    public List<string> GetMateriasAtivas(){
        List<string> materias = new List<string>();

        if (matAtivo) materias.Add("Matemática");
        if (hisAtivo) materias.Add("História");
        if (geoAtivo) materias.Add("Geografia");
        if (ingAtivo) materias.Add("Inglês");
        if (bioAtivo) materias.Add("Biologia");
        if (fisAtivo) materias.Add("Física");
        if (quiAtivo) materias.Add("Química");
        if (socAtivo) materias.Add("Sociologia");
        if (filAtivo) materias.Add("Filosofia");

        return materias;
    }
}
