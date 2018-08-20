using System.Collections;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

public class ApiService : MonoBehaviour
{
    public UnityEngine.UI.Text DescriptionText;
    public UnityEngine.UI.Text TagNameText;
    public UnityEngine.UI.Text UnitText;
    public UnityEngine.UI.Text ErrorText;

    void Start()
    {
        //StartCoroutine(RequestApi());
    }

    // Update is called once per frame
    void Update()
    {

    }

    //public IEnumerator GetAllSignals(string sim)
    //{
    //    sim = sim.Split('_')[0];
    //    UnityWebRequest webRequest = UnityWebRequest.Get("http://localhost:57281/api/Temas/GetSignal/" + sim);
    //    //UnityWebRequest webRequest = UnityWebRequest.Get("http://localhost:57281/api/Temas/GetAllSignals");

    //    yield return webRequest.SendWebRequest();

    //    List<Signal> signals = JsonConvert.DeserializeObject<List<Signal>>(webRequest.downloadHandler.text);

    //    //foreach (var signal in signals)
    //    //{
    //    //    Debug.Log(signal.Description);
    //    //}

    //    Description.text = signals.First().Description;
    //}

    private IEnumerator GetSignalByTagNameServiceCall(string tagName)
    {
        ErrorText.text = string.Empty;
        UnityWebRequest webRequest = UnityWebRequest.Get("http://localhost:57281/api/Temas/GetSignal/" + tagName.ToLower());

        yield return webRequest.SendWebRequest();

        Signal signal = JsonConvert.DeserializeObject<Signal>(webRequest.downloadHandler.text);

        if (signal != null)
        {
            DescriptionText.text = signal.Description;
            TagNameText.text = signal.TagName;
            UnitText.text = signal.Unit;
        }
        else
        {
            DescriptionText.text = string.Empty;
            TagNameText.text = string.Empty;
            UnitText.text = string.Empty;
            ErrorText.text = "Unable retrive value for " + tagName + "from service";
        }
    }

    public void GetSignalByTagName(string trackableName, string name)
    {
        if (name.ToLower().StartsWith(trackableName.ToLower()))
            StartCoroutine(GetSignalByTagNameServiceCall(trackableName));
    }
}
