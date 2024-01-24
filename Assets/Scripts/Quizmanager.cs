using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


// SYSTEM INSPIRED BY: https://www.youtube.com/watch?v=G9QDFB2RQGA
// DESIGN INSPIRED BY: https://www.youtube.com/watch?v=asOScgc0bmo

public class Quizmanager : MonoBehaviour
{
    public List<QuestionsAndAnswers> QnA;
    public int currentQuestion;
    public TextMeshProUGUI QuestionText;
    public GameObject answerButtonPrefab;
    public GameObject answerButtonParent;
    public GameOver gameOver;

    private void Start()
    {
        if(SceneManager.GetActiveScene().buildIndex == 1) // if QuestionScene, generateQuestion (eliminates bug in answerScene)
        {
            generateQuestion();
        }
        
    }

    public void nextQuestion()
    {
        
        foreach (Transform child in answerButtonParent.transform) // deletes all former answers from answerButtonParent
        {
            GameObject.Destroy(child.gameObject);
        }

        QnA.RemoveAt(currentQuestion); // removes current question from list, so it doesn't repeat.

        if (QnA.Count == 0) // if no more questions = load result scene
        {
            SceneManager.LoadScene(1);
        }
        else // if still more questions = generate new question + set answers
        {
            generateQuestion();   // Generates new random question through generateQuestion-Method
        }
    }

    void generateQuestion()
    {
        currentQuestion = Random.Range(0, QnA.Count); // Generates random integer corresponding to elements on QnA-list
        QuestionText.text = QnA[currentQuestion].Question; // Sets Question to relevant element from QnA-list
        SetAnswers();
    }


    void SetAnswers()
    {
        for (int i = 0; i<QnA[currentQuestion].Answers.Length; i++) // for every Answer in CurrentQuestions...
        {
            GameObject thisInstant = Instantiate(answerButtonPrefab, new Vector3(0, 0, 0), Quaternion.identity, answerButtonParent.transform); // ... instantiate an answerButtonPrefab under parent, and save as var thisInstant...
            thisInstant.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = QnA[currentQuestion].Answers[i]; // ... , inserts text from answers in CurrentQuestion in TMP-component in Child "Text (TMP)"

            thisInstant.GetComponent<Button>().onClick.RemoveAllListeners(); // resets onclick events on Answer Buttons' Button-component

            string thisAnswersPointPointBinCorrespondent = QnA[currentQuestion].pointBinCorrespondent[i]; // sets var thisAnswersPointBinCorrespondent
            thisInstant.GetComponent<Button>().onClick.AddListener(delegate {gameObject.GetComponent<AnswerScript>().Answer(thisAnswersPointPointBinCorrespondent); } ); // Adds onclick event on the instantiated prefab's button-script, that calls AnswerScript's Answer-function with This AnswersPointBinCorrepondent as argument
        }
    }

}
