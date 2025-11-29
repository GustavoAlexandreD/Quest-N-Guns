using UnityEngine;

public enum Dificuldade {Facil, Medio, Dificil}

[CreateAssetMenu(fileName = "NovaQuestão", menuName = "Quiz")]
public class DadosDaQuestao : ScriptableObject
{
    [Header("Configuração")]
    public string tema;
    public Dificuldade dificuldade;

    [Header("Conteúdo")]
    [TextArea] public string enunciado;
    public string[] alternativas;
    public int indiceCorreto;
}
