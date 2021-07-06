# Hangman-Application
Hangman Application made with C# and WPF


- This project is an implementation of Hangman in C# using WPF in Visual Studio.
- This app is structured on the Model-View-ViewModel architecture where the user data, like name and profile picture - selected by user -  is stored persistently using XML serilization. (xmlfile.xml)  

| ![](https://github.com/BogdanKocsis/Hangman-Application/blob/master/Hangman/Images/0.png) | ![](https://github.com/BogdanKocsis/Hangman-Application/blob/master/Hangman/Images/1.png) |
|:---:|:---:|
| ![](https://github.com/BogdanKocsis/Hangman-Application/blob/master/Hangman/Images/2.png) |  |  

From Home Screen the user can choose either to Sign In or to Sign Up. The Login process is trivial - the user choose a nickname   and then presses Play. The user can also Add another account or Delete an existing one. When adding a new account, the user has to choose a nickname and also a profile picture.

After registration/authentification the user will choose a category of words.  

In PreGame menu you will find some actions like New Game, Open Game, Exit Game and Statistics. Categories menu let you change the category of words you play. Help menu leads to About window.

| ![](https://github.com/BogdanKocsis/Hangman-Application/blob/master/Hangman/Images/3.png)  | ![](https://github.com/BogdanKocsis/Hangman-Application/blob/master/Hangman/Images/4.png) |
|:---:|:---:|  
| ![](https://github.com/BogdanKocsis/Hangman-Application/blob/master/Hangman/Images/6.png)  |


In Game window some information about the user are displayed, - like nickname, profile picture - some information about the game - like time left, level- and save game button.

| ![](https://github.com/BogdanKocsis/Hangman-Application/blob/master/Hangman/Images/7.png)  | ![](https://github.com/BogdanKocsis/Hangman-Application/blob/master/Hangman/Images/8.png) |
|:---:|:---:|   

When in game you can choose any letter to find if appears in the word or not. If the letter appears, the button will be locked and marked with green, and also will appear in the word below. If the letter doesn't appear, the button will be locked and marked with red, and also the Hangman will be updated
  
| ![](https://github.com/BogdanKocsis/Hangman-Application/blob/master/Hangman/Images/9.png)  | ![](https://github.com/BogdanKocsis/Hangman-Application/blob/master/Hangman/Images/10.png)  |
|:---:|:---:|   


If the time is up, a warning will be shown and the game will be lost. If 6 mistakes are made the game will also be lost. When you guess a word the level will increase and at 5 levels won in a row you will win a game.  
In Statistics window you will find information about how many games you won and lost at any category and in total, and how many games you played in total.  

![](https://github.com/BogdanKocsis/Hangman-Application/blob/master/Hangman/Images/5.png)
