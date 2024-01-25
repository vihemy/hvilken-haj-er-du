using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NavigationPanelController : MonoBehaviour
{
    private GameManager gameManager;
    private List<GameObject> children;
    [SerializeField] private Sprite filledCircle;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        int currentBuildIndex = SceneLoader.Instance.GetCurrentSceneBuildIndex() - 1; // minus 1 because first scene is idle

        Transform childTransform = GetChildCorrespondingToBuildIndex(currentBuildIndex); // uses transform instead of GameObject because transform is easier to work with. GameObject of transform is accesed in final function-call.
        SwitchImageToFilled(childTransform);
        ChangeTextColor(childTransform);
    }

    private Transform GetChildCorrespondingToBuildIndex(int buildIndex)
    {
        Transform childTransform = transform.GetChild(buildIndex);

        return childTransform;
    }

    private void SwitchImageToFilled(Transform child)
    {
        child.gameObject.GetComponent<Image>().sprite = filledCircle;
    }

    private void ChangeTextColor(Transform childTransform)
    {
        childTransform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().color = Color.black;
    }

}
