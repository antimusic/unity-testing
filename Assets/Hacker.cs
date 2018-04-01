using UnityEngine;

public class Hacker : MonoBehaviour {
    //game config data & init
    string menuHint = "Type menu to return to menu screen";
    string[] level1Passwords = {"books", "aisle", "shelf", "password", "font", "borrow"};
    string[] level2Passwords = {"prisoner", "handcuffs", "holster", "uniform", "arrest"};

	
    
    //game state
	int level;
  
	enum Screen {MainMenu, Password, Win};
	Screen currentScreen;

    string password;

	

	// Use this for initialization
	void Start () {
		ShowMainMenu ();
	}

	void ShowMainMenu () {
		Terminal.ClearScreen();
		Terminal.WriteLine("where do you want to hack?");
		Terminal.WriteLine("press 1 for library");
		Terminal.WriteLine("press 2 for police station");
		Terminal.WriteLine("type: ");
	}

	void OnUserInput(string input)
    {
        if (input == "menu")
		{
            ShowMainMenu();
            currentScreen = Screen.MainMenu;
        }
        else if (currentScreen == Screen.MainMenu)
		{
        RunMainMenu(input);
		}
        else if (currentScreen == Screen.Password)
		{
        PasswordGuess(input);
		}
    }

    void RunMainMenu(string input)
    {
        bool isValidLevelNumber = (input == "1" || input == "2");
        if (isValidLevelNumber)
        {
            level=int.Parse(input);
            AskPassword();
        }
        else if (input == "007")
        {
            Terminal.WriteLine("Hello mr Bond");
        }
        else
        {
            Terminal.WriteLine("please choose from 1-2");
            Terminal.WriteLine(menuHint);
        }
    }

    void AskPassword()
    {
        currentScreen=Screen.Password;
		Terminal.ClearScreen();
        switch(level)
        {
            case 1:
                password = level1Passwords[Random.Range(0, level1Passwords.Length)];
                break;
            case 2:
                password = level2Passwords[Random.Range(0, level2Passwords.Length)];
                break;
            default:
                Debug.LogError("Invalid Level  Number");
                break;
        }
        Terminal.WriteLine("Enter your password, hint: " + password.Anagram());
        Terminal.WriteLine(menuHint);
        }

    void PasswordGuess(string input) {
        if (input == password)
        {
            WinState();
        }
        else {
            AskPassword();
        }

    }

    void WinState()
    {
        currentScreen = Screen.Win;
        Terminal.ClearScreen();
        WinScreen();
    }

    void WinScreen()
    {
        switch (level) 
        {
        case 1:
            Terminal.WriteLine(@"
Volume in drive C is LibertLib
Volume Serial Number is DF97-6B80

Directory of C:\LIBERTY\city\library

29/09/2017  04:41 ££ 29.696 ztrace_maps.dll
21/11/2017  06:16 §£ <DIR>          zu-ZA
3 File(s)  2.469.978.033 bytes
5 Dir(s)  81.612.222.464 bytes free
            ");
            break;
        case 2:
            Terminal.WriteLine(@"
Volume in drive C is LCPD
Volume Serial Number is FG27-6B55

Directory of D:\LCPD\files

29/09/2017  04:41 ££ 563 @language_notification_icon.png
29/09/2017  04:41 ££ 483 @optionalfeatures.png
7 File(s)  1.523.938.023 bytes
2 Dir(s)  17.542.282.234 bytes free
            ");
            break;
        }

    }
}
