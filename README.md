# Morse Code Trainer  
![The GUI](https://github.com/Mins0o/MorseCodeTrainer/raw/master/forGitHub/GUI.png "The GUI")![The GUI](https://github.com/Mins0o/MorseCodeTrainer/raw/master/forGitHub/GUI_Input.png "The GUI")  
This program is a graphical interface to translate between text and Morse code bidirectionally, that is:  
1. Translate text to Morse code audio  
2. Tap in Morse code to produce text  
--- 
1. Text to Morse code  
  - After filling in the text,   
  - press Play button to play the code in the background.   
  - You can choose which device to output the sound by using the lower left panel.  
  - Press Save button to save the morse code audio in .wav file.  
  - Playback speed can be modified for play and save audio.  
  
2. Tap Morse code to text  
  - Get into the Input Mode  
  - tap the buttons and the text will appear when you leave the Input Mode.  
  - or tap 'a' or 'k' key like how you would operate a Morse code switch.  
  - 'a' will produce alphabet letter and 'k' will produce Korean letter.  
  - If you mix them in one letter, it will be translated to ?  
  - The timing window is not so generous, so the input timings for tapping or breaking should be regular.  
  - When you are done tapping, press return(or enter) or press the Input Mode button to exit and produce the text.  
  - The text field will be replaced with your text.  
---  
---
# Source Code  
![The Model](https://github.com/Mins0o/MorseCodeTrainer/raw/master/forGitHub/TheModel.png "The Model")  
There are four classes in total: MorseToSound.cs, BeepGenerator.cs, OutputGenerator.cs, and UpDownToMorse.  
  
- MorseToSound.cs  
This class takes the text from the text field of the UI and maps it into Morse code encoded by '1'(short beep), '2'(long dash), '0'(short pause).  
Korean characters are disassembled and translated by individual characters.  
Vietnamese characters are also considered as alpahbets and is translated to Morse code  
This class has 1 public method:  
  - A constructor - Takes in ordinary string of text consisted of alphabet, Korean character and Vietnamese characters and creates an instance of this class.  
  - translateToMorse() - This method returns Morse code encoded with '1','2', and '0'.  
  
- BeepGenerator.cs  
This class is responsible of generating the sound signal.  
The class is initialized with one integer argument, which is used for its speed.  
This class has 3 public methods:  
  - A constructor - Takes and integer value between 1 to 10 and sets it as speed value. Higher the value, faster the signal.  
  - shortBeep() - Returns a sin wave of 500Hz with shorter duration.  
  - longBeep() - Returns a sin wave of 500Hz with longer duration.  
  - stringInput(string,int) - The string here must be in format of the Morse code. The integer value is the speed value.  

- OutputGenerator.cs  
This class is responsible of the playing or saving of the sound signal.
The class is initialized with the sound signal<ISampleProvider> and device number. Device number is used to choose the playback device. 0 will mostly get the default playback, and 0 is the default value.  
This class has 3 public method(one more for raising event, but it isn't used)  
  - play(object<Button>) - This will play the sound signal the class was initiated with in the background(Async).  
  - stop() - This will send cancel to the Async, making the play to stop.  
  - save() - This will prompt a save dialog and will save the sound signal in .wav format,  
  
- UpDownToMorse.cs
This class doesn't take any arguments for new instance. A new instance is created when user enters the input mode.  
This class has 2 public methods:  
  - AddToBuffer(long,char,char) - This method takes in the interval between button press/release and adds a letter of Morse code to the buffer '0','1','2' along with the language information. The integer is the interval button the button press/release or release/press. The first char is the type of button manipulation, telling if it was a hold or a release. The last char tells which language input it got.  
  - TranslatedText() - This method returns the Morse -> text translated text of the input. This method is called when user exits the input mode.  
---
# Future Plan
With the tap-to-Morse input mode, getting the timings right is being a problem.  When the input is not timed well, the code missing the necessary gap('0') or having additional gap messes up the translation.  
Currently there are two ways to tackle this problem.  
- Give user the feedback of until when they should wait in release to register a space('0').
- Algorithm to correct ill-spaced(opposite of well-spaced) code.  
This improvement will be done in later future.
