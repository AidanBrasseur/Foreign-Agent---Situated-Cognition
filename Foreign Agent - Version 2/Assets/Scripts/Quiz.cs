using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
	private int correctIndex = -1;
	public Text questionText;
	public GameObject[] answerObjects;
	public bool correct;
	private string[] questions = {
		"Throughout the game, these question panels will pop up from time to time to test your knowledge. To proceed, choose the right answer.",
		"",
		"",
		"",
		"What do macrophages do?",
		"",
		"",
		"What is the function of a Helper T Cell?",
		"",
		"",
		"What is the purpose of a Killer T Cell?",
		"", // 1.2
		"What do activated B cells do?",
		"What is the function of skin in the immune system?",
		"What are antibodies?",
		"Which objects bind to and lyse infected cells?",
		"When are memory cells created?",
		"Which cells secrete antibodies?"
	};
	private int index = 0;
	private string[][] answers = {
		new string[] {"correctAnswer I understand.", "Don't click me!", "I am a wrong answer!", "Incorrect!"},
		new string[] { },
		new string[] { },
		new string[] { },
		new string[] {"Transport oxygen throughout the immune system", "Defeat foreign agents by stopping them from using cell machinery to replicate", "correctAnswer Kill foreign agents by engulfing them", "All of the above"},
		new string[] { },
		new string[] { },
		new string[] {"Transport nutrients to other cells in the immune system", "correctAnswer Activate B cells and aid Killer T cells", "Stimulate growth of macrophages", "Produce antibodies to help slow down foreign particles" },
		new string[] { },
		new string[] { },
		new string[] {"correctAnswer Kill infected cells", "Kill foreign agents directly", "Produce antibodies", "Transport nutrients"},
		new string[] { },
		new string[] {"Kill infected cells", "correctAnswer Produce antibodies to slow down and make foreign agents easier to detect", "Activate Killer T cells", "Chase foreign agents and engulf them"},
		new string[] {"correctAnswer Prevents most infectious organisms from entering the body", "Nothing", "Contains macrophages and cytotoxic T cells, which exterminate infectious organisms", "Alerts the immune system and prepare it to deal with foreign organisms"},
		new string[] {"A specialized B cell", "A type of white blood cell", "A Y-shaped carbohydrate group", "correctAnswer A Y-shaped protein"},
		new string[] {"Antibodies", "correctAnswer Killer T Cells", "Macrophages", "B cells"},
		new string[] {"Before first infection", "correctAnswer After the first infection", "Immediately upon encountering a second infection", "After a second infection"},
		new string[] { "Helper T cells",  "Unactivated B Cells", "Macrophages", "correctAnswer Plasma B cells" }
	};

	public void Start()
	{
		correct = false;
	}
	public void GenerateNewQuestion()
	{
		correct = false;
		int curInd = SceneManager.GetActiveScene().buildIndex;
		string[] curAnswers = answers[curInd];
		for (int i = 0; i < curAnswers.Length; i++)
		{
			if (curAnswers[i].Contains("correctAnswer"))
			{
				curAnswers[i] = string.Join(" ", curAnswers[i].Split(' ').Skip(1).ToArray());
				correctIndex = i;
			}
		}
		string curQuestion = questions[curInd];
		questionText.text = curQuestion;
		for (int i = 0; i < answerObjects.Length; i++)
		{
			answerObjects[i].transform.GetChild(0).gameObject.GetComponent<Text>().text = curAnswers[i];
            if (i != correctIndex)
            {
                ColorBlock cb = answerObjects[i].gameObject.GetComponent<Button>().colors;
                cb.selectedColor = Color.red;
                answerObjects[i].gameObject.GetComponent<Button>().colors = cb;
            }

        }


	}
	public void Display()
	{
		gameObject.SetActive(true);
	}
	private void verifyAnswer(int answerInd)
	{
		if (correctIndex == answerInd)
		{
			correct = true;
		}
	}
	public void Answer0()
	{
		verifyAnswer(0);
	}
	public void Answer1()
	{
		verifyAnswer(1);
	}
	public void Answer2()
	{
		verifyAnswer(2);
	}
	public void Answer3()
	{
		verifyAnswer(3);
	}

}
