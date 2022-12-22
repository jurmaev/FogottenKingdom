using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ArtifactInformationController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI artifactName;
    [SerializeField] private TextMeshProUGUI artifactDescription;
    [SerializeField] private float informationDisplayTime;
    private bool isShowInformation;
    private IEnumerator coroutine;

    void Start()
    {
        EventManager.OnArtifactSelection.AddListener(ShowArtifactInformation);
    }


    void Update()
    {
    }

    private void ShowArtifactInformation(Artifact artifact)
    {
        if (!isShowInformation)
        {
            isShowInformation = true;
            StartShowInformation(artifact);
        }
        else
        {
            StopCoroutine(coroutine);
            StartShowInformation(artifact);
        }
    }

    private void StartShowInformation(Artifact artifact)
    {
        artifactName.text = artifact.ArtifactName;
        artifactDescription.text = artifact.Description;
        coroutine = CloseInformation();
        StartCoroutine(coroutine);
    }

    private IEnumerator CloseInformation()
    {
        yield return new WaitForSeconds(informationDisplayTime);
        artifactName.text = "";
        artifactDescription.text = "";
        isShowInformation = false;
    }
}