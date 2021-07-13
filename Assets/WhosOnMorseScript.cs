using System.Collections;
using System.Linq;
using UnityEngine;
using System.Text.RegularExpressions;
using System;

public class WhosOnMorseScript : MonoBehaviour {

    public KMAudio audio;
    public KMBombInfo bomb;
    public KMRuleSeedable ruleSeedable;
    public KMSelectable[] buttons;
    public MeshRenderer[] ledRenderers;
    public MeshRenderer[] stageRenderers;
    public GameObject morseLight;
    public Material[] materials;

    private string[] aWords = { "SHELL", "HALLS", "SLICK", "TRICK", "BOXES", "LEAKS", "STROBE", "BISTRO", "FLICK", "BOMBS", "BREAK", "BRICK", "STEAK", "STING", "VECTOR", "BEATS", "CURSE", "NICE", "VERB", "NEARLY", "CREEK", "TRIBE", "CYBER", "CINEMA", "KOALA", "WATER", "WHISK", "MATTER", "KEYS", "STUCK" };
    private string[] morseLetters = { ".-", "-...", "-.-.", "-..", ".", "..-.", "--.", "....", "..", ".---", "-.-", ".-..", "--", "-.", "---", ".--.", "--.-", ".-.", "...", "-", "..-", "...-", ".--", "-..-", "-.--", "--.." };
    private string[] positions = { "top left", "top right", "bottom left", "bottom right" };
    private char[][] bLetters =
    {
        new char[] { 'C', 'L', 'U', 'D', 'Y', 'J', 'R', 'T', 'X', 'K', 'G', 'W', 'M', 'H', 'Q', 'F', 'B', 'A', 'N', 'P', 'S', 'Z', 'E', 'V', 'I', 'O' },
        new char[] { 'F', 'R', 'P', 'O', 'M', 'X', 'Y', 'T', 'S', 'J', 'C', 'N', 'K', 'E', 'B', 'I', 'U', 'Q', 'A', 'H', 'L', 'Z', 'V', 'G', 'D', 'W' },
        new char[] { 'Y', 'O', 'Z', 'F', 'E', 'V', 'D', 'N', 'K', 'S', 'B', 'H', 'M', 'J', 'Q', 'T', 'A', 'U', 'C', 'G', 'I', 'R', 'X', 'W', 'P', 'L' },
        new char[] { 'W', 'K', 'Q', 'I', 'A', 'O', 'R', 'X', 'F', 'U', 'P', 'B', 'H', 'L', 'C', 'M', 'S', 'Y', 'E', 'Z', 'G', 'J', 'T', 'N', 'D', 'V' },
        new char[] { 'V', 'Y', 'W', 'N', 'D', 'X', 'A', 'L', 'G', 'Z', 'M', 'O', 'P', 'K', 'E', 'C', 'T', 'I', 'S', 'U', 'F', 'Q', 'R', 'B', 'J', 'H' },
        new char[] { 'V', 'B', 'T', 'L', 'J', 'X', 'I', 'W', 'Q', 'A', 'F', 'Y', 'D', 'H', 'G', 'S', 'K', 'N', 'U', 'R', 'C', 'E', 'O', 'M', 'P', 'Z' },
        new char[] { 'H', 'S', 'Z', 'M', 'J', 'P', 'E', 'D', 'Q', 'B', 'N', 'X', 'C', 'T', 'W', 'U', 'I', 'L', 'F', 'K', 'O', 'A', 'V', 'R', 'Y', 'G' },
        new char[] { 'U', 'O', 'I', 'X', 'R', 'Z', 'G', 'C', 'E', 'J', 'Q', 'N', 'L', 'T', 'S', 'Y', 'P', 'F', 'B', 'K', 'A', 'H', 'D', 'M', 'W', 'V' },
        new char[] { 'G', 'E', 'B', 'I', 'U', 'C', 'L', 'D', 'P', 'N', 'J', 'S', 'T', 'W', 'M', 'O', 'K', 'X', 'A', 'Z', 'H', 'F', 'R', 'Q', 'Y', 'V' },
        new char[] { 'B', 'L', 'J', 'H', 'T', 'W', 'P', 'C', 'G', 'X', 'Z', 'A', 'M', 'Y', 'D', 'E', 'R', 'V', 'F', 'Q', 'N', 'I', 'U', 'O', 'S', 'K' },
        new char[] { 'S', 'Q', 'B', 'W', 'A', 'I', 'P', 'E', 'L', 'D', 'V', 'T', 'C', 'N', 'R', 'K', 'X', 'M', 'G', 'O', 'J', 'U', 'Z', 'F', 'H', 'Y' },
        new char[] { 'Z', 'T', 'V', 'S', 'L', 'B', 'Y', 'W', 'U', 'N', 'Q', 'H', 'E', 'G', 'P', 'J', 'O', 'I', 'R', 'K', 'A', 'X', 'F', 'M', 'C', 'D' },
        new char[] { 'S', 'O', 'J', 'R', 'K', 'N', 'C', 'G', 'L', 'U', 'W', 'B', 'V', 'Q', 'I', 'P', 'D', 'M', 'A', 'T', 'H', 'X', 'E', 'F', 'Y', 'Z' },
        new char[] { 'F', 'I', 'Y', 'X', 'A', 'G', 'N', 'J', 'T', 'W', 'Q', 'B', 'C', 'Z', 'R', 'E', 'D', 'M', 'V', 'H', 'S', 'P', 'O', 'U', 'L', 'K' },
        new char[] { 'C', 'S', 'H', 'J', 'W', 'E', 'Z', 'G', 'B', 'A', 'K', 'X', 'U', 'V', 'T', 'D', 'M', 'R', 'F', 'L', 'Q', 'P', 'N', 'O', 'Y', 'I' },
        new char[] { 'Z', 'G', 'B', 'M', 'R', 'I', 'U', 'X', 'F', 'P', 'Y', 'V', 'K', 'S', 'T', 'A', 'N', 'D', 'L', 'H', 'E', 'C', 'O', 'W', 'J', 'Q' },
        new char[] { 'W', 'V', 'O', 'J', 'Z', 'D', 'U', 'F', 'T', 'K', 'G', 'R', 'M', 'P', 'E', 'I', 'B', 'X', 'Q', 'L', 'Y', 'A', 'H', 'S', 'N', 'C' },
        new char[] { 'S', 'G', 'N', 'D', 'U', 'K', 'M', 'P', 'A', 'T', 'V', 'H', 'C', 'I', 'R', 'X', 'L', 'B', 'O', 'Z', 'E', 'W', 'F', 'J', 'Y', 'Q' },
        new char[] { 'S', 'I', 'Z', 'G', 'C', 'W', 'A', 'Y', 'N', 'K', 'D', 'X', 'L', 'Q', 'F', 'T', 'H', 'E', 'U', 'M', 'P', 'O', 'V', 'B', 'J', 'R' },
        new char[] { 'D', 'C', 'L', 'G', 'B', 'Z', 'S', 'P', 'R', 'T', 'H', 'M', 'J', 'V', 'A', 'E', 'Y', 'I', 'X', 'O', 'N', 'W', 'K', 'F', 'U', 'Q' },
        new char[] { 'Y', 'S', 'I', 'N', 'H', 'F', 'K', 'U', 'P', 'Q', 'X', 'V', 'C', 'E', 'J', 'O', 'W', 'A', 'R', 'D', 'G', 'B', 'Z', 'L', 'T', 'M' },
        new char[] { 'E', 'K', 'A', 'H', 'R', 'P', 'M', 'D', 'X', 'V', 'S', 'O', 'Q', 'Z', 'U', 'J', 'Y', 'T', 'B', 'C', 'I', 'G', 'F', 'W', 'L', 'N' },
        new char[] { 'B', 'M', 'Z', 'C', 'W', 'E', 'X', 'S', 'N', 'R', 'K', 'Q', 'D', 'G', 'I', 'A', 'O', 'U', 'Y', 'V', 'F', 'L', 'H', 'T', 'P', 'J' },
        new char[] { 'A', 'L', 'K', 'P', 'H', 'Y', 'T', 'W', 'R', 'C', 'N', 'B', 'U', 'D', 'I', 'Z', 'O', 'F', 'S', 'J', 'X', 'G', 'E', 'M', 'Q', 'V' },
        new char[] { 'T', 'G', 'Z', 'I', 'L', 'M', 'S', 'K', 'H', 'J', 'C', 'U', 'P', 'F', 'A', 'D', 'V', 'N', 'W', 'Q', 'B', 'R', 'Y', 'X', 'E', 'O' },
        new char[] { 'J', 'N', 'M', 'Y', 'R', 'Q', 'Z', 'H', 'F', 'B', 'K', 'I', 'O', 'U', 'S', 'C', 'X', 'G', 'W', 'V', 'A', 'T', 'P', 'D', 'L', 'E' }
    };
    private char[] alphabet = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
    private int[] readPositions = new int[30];
    private int[] ledMorsePos = { -1, -1, -1, -1 };
    private int lightMorsePos;
    private int correctLED = -1;
    private int stage = 0;
    private bool delayOccurring = false;

    static int moduleIdCounter = 1;
    int moduleId;
    private bool moduleSolved;

    void Awake()
    {
        moduleId = moduleIdCounter++;
        moduleSolved = false;
        foreach (KMSelectable obj in buttons)
        {
            KMSelectable pressed = obj;
            pressed.OnInteract += delegate () { PressButton(pressed); return false; };
        }
        GetComponent<KMBombModule>().OnActivate += OnActivate;
    }

    void Start () {
        morseLight.GetComponent<Light>().range *= transform.lossyScale.x;
        var rnd = ruleSeedable.GetRNG();
        Debug.LogFormat("[Who's on Morse #{0}] Using rule seed: {1}", moduleId, rnd.Seed);
        if (rnd.Seed != 1)
        {
            for (int i = 0; i < readPositions.Length; i++)
                readPositions[i] = rnd.Next(0, 4);
            for (int i = 0; i < bLetters.Length; i++)
                bLetters[i] = rnd.ShuffleFisherYates(bLetters[i]);
        }
        else
            readPositions = new int[] { 0, 1, 3, 2, 3, 0, 0, 1, 0, 2, 0, 0, 2, 2, 3, 2, 1, 3, 3, 3, 2, 3, 1, 1, 0, 2, 1, 2, 3, 0 };
        GenerateStage(true);
    }

    void OnActivate()
    {
        if (!delayOccurring)
        {
            StartCoroutine(LightFlash());
            for (int i = 0; i < 4; i++)
                StartCoroutine(LEDFlash(i));
        }
    }

    void PressButton(KMSelectable pressed)
    {
        if (moduleSolved != true && delayOccurring != true)
        {
            pressed.AddInteractionPunch();
            audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, pressed.transform);
            StopAllCoroutines();
            if (Array.IndexOf(buttons, pressed) == correctLED)
            {
                Debug.LogFormat("[Who's on Morse #{0}] <Stage {1}> Pressed the {2} LED ('{3}'), which is correct", moduleId, stage + 1, positions[Array.IndexOf(buttons, pressed)], alphabet[ledMorsePos[Array.IndexOf(buttons, pressed)]]);
                stageRenderers[stage].material = materials[4];
                stage++;
                if (stage > 2)
                {
                    moduleSolved = true;
                    Debug.LogFormat("[Who's on Morse #{0}] Module disarmed", moduleId);
                    GetComponent<KMBombModule>().HandlePass();
                    for (int i = 0; i < 4; i++)
                        ledRenderers[i].material = materials[2];
                    ledRenderers[4].material = materials[0];
                    morseLight.SetActive(false);
                    return;
                }
                else
                {
                    GenerateStage(false);
                }
                StartCoroutine(DelayDisplay());
            }
            else
            {
                Debug.LogFormat("[Who's on Morse #{0}] <Stage {1}> Pressed the {2} LED ('{3}'), which is incorrect", moduleId, stage + 1, positions[Array.IndexOf(buttons, pressed)], alphabet[ledMorsePos[Array.IndexOf(buttons, pressed)]]);
                GetComponent<KMBombModule>().HandleStrike();
                GenerateStage(false);
                StartCoroutine(DelayDisplay());
            }
        }
    }

    void GenerateStage(bool firstTime)
    {
        if (!firstTime)
        {
            ledMorsePos = new int[]{ -1, -1, -1, -1 };
            correctLED = -1;
        }
        lightMorsePos = UnityEngine.Random.Range(0, aWords.Length);
        Debug.LogFormat("[Who's on Morse #{0}] <Stage {1}> Flashing word is {2}", moduleId, stage + 1, aWords[lightMorsePos]);
        for (int i = 0; i < 4; i++)
        {
            int choice = UnityEngine.Random.Range(0, morseLetters.Length);
            while (ledMorsePos.Contains(choice))
                choice = UnityEngine.Random.Range(0, morseLetters.Length);
            ledMorsePos[i] = choice;
        }
        Debug.LogFormat("[Who's on Morse #{0}] <Stage {1}> LEDs are flashing {2}, {3}, {4}, and {5}", moduleId, stage + 1, alphabet[ledMorsePos[0]], alphabet[ledMorsePos[1]], alphabet[ledMorsePos[2]], alphabet[ledMorsePos[3]]);
        char[] bArray = bLetters[ledMorsePos[readPositions[lightMorsePos]]];
        for (int i = 0; i < bArray.Length; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if (alphabet[ledMorsePos[j]].Equals(bArray[i]))
                {
                    correctLED = j;
                    break;
                }
            }
            if (correctLED != -1)
                break;
        }
        Debug.LogFormat("[Who's on Morse #{0}] <Stage {1}> Press the {2} LED ('{3}')", moduleId, stage + 1, positions[correctLED], alphabet[ledMorsePos[correctLED]]);
    }

    IEnumerator LightFlash()
    {
        string word = aWords[lightMorsePos];
        while (true)
        {
            foreach (char c in word)
            {
                string morse = morseLetters[Array.IndexOf(alphabet, c)];
                foreach (char symbol in morse)
                {
                    ledRenderers[4].material = materials[1];
                    morseLight.SetActive(true);
                    yield return new WaitForSeconds(symbol == '.' ? 0.25f : 0.75f);
                    ledRenderers[4].material = materials[0];
                    morseLight.SetActive(false);
                    yield return new WaitForSeconds(0.25f);
                }
                yield return new WaitForSeconds(0.75f);
            }
            yield return new WaitForSeconds(1.5f);
        }
    }

    IEnumerator LEDFlash(int led)
    {
        string morse = morseLetters[ledMorsePos[led]];
        while (true)
        {
            foreach (char symbol in morse)
            {
                ledRenderers[led].material = materials[3];
                yield return new WaitForSeconds(symbol == '.' ? 0.25f : 0.75f);
                ledRenderers[led].material = materials[2];
                yield return new WaitForSeconds(0.25f);
            }
            yield return new WaitForSeconds(1.5f);
        }
    }

    IEnumerator DelayDisplay()
    {
        delayOccurring = true;
        for (int i = 0; i < 4; i++)
            ledRenderers[i].material = materials[2];
        ledRenderers[4].material = materials[0];
        morseLight.SetActive(false);
        yield return new WaitForSeconds(1.5f);
        StartCoroutine(LightFlash());
        for (int i = 0; i < 4; i++)
            StartCoroutine(LEDFlash(i));
        delayOccurring = false;
    }

    //twitch plays
    #pragma warning disable 414
    private readonly string TwitchHelpMessage = @"!{0} press <pos> [Presses the LED in the specified position] | Valid positions are 1-4 in reading order or TL, BR, etc.";
    #pragma warning restore 414
    IEnumerator ProcessTwitchCommand(string command)
    {
        string[] parameters = command.Split(' ');
        if (Regex.IsMatch(parameters[0], @"^\s*press\s*$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant))
        {
            yield return null;
            if (parameters.Length > 2)
            {
                yield return "sendtochaterror Too many parameters!";
            }
            else if (parameters.Length == 2)
            {
                string[] pos = { "TL", "TR", "BL", "BR" };
                int temp = -1;
                if (!int.TryParse(parameters[1], out temp))
                {
                    if (!pos.Contains(parameters[1].ToUpper()))
                    {
                        yield return "sendtochaterror!f The specified position '" + parameters[1] + "' is invalid!";
                        yield break;
                    }
                    temp = Array.IndexOf(pos, parameters[1].ToUpper()) + 1;
                }
                else if (temp < 1 || temp > 4)
                {
                    yield return "sendtochaterror The specified position '" + parameters[1] + "' is invalid!";
                    yield break;
                }
                buttons[temp - 1].OnInteract();
            }
            else if (parameters.Length == 1)
            {
                yield return "sendtochaterror Please specify the position of an LED to press!";
            }
            yield break;
        }
    }

    IEnumerator TwitchHandleForcedSolve()
    {
        int start = stage;
        for (int i = start; i < 3; i++)
        {
            while (delayOccurring) yield return true;
            buttons[correctLED].OnInteract();
        }
    }
}
