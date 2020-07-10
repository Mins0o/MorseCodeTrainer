using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MorseCodeTrainer
{
    class UpDownToMorse
    {
        private string buffer;
        private string _text;
        private int shortThreshold = 150;
        private int oneLetterThreshold = 350;
        private int oneWordThreshold = 600;
        private int oneSentenceThreshold = 1000;
        private Dictionary<string, char> alphaMorseMap = new Dictionary<string, char>(){
            {"12",'A'},{"2111",'B'},{"2121",'C'},{"211",'D'},{"1",'E'},{"1121",'F'},{"221",'G'},{"1111",'H'},{"11",'I'},{"1222",'J'},{"212",'K'},{"1211",'L'},{"22",'M'},{"21",'N'},{"222",'O'},{"1221",'P'},{"2212",'Q'},{"121",'R'},{"111",'S'},{"2",'T'},{"112",'U'},{"1112",'V'},{"122",'W'},{"2112",'X'},{"2122",'Y'},{"2211",'Z'},{"22222",'0'},{"12222",'1'},{"11222",'2'},{"11122",'3'},{"11112",'4'},{"11111",'5'},{"21111",'6'},{"22111",'7'},{"22211",'8'},{"22221",'9'}
            };
        private Dictionary<string, char> korMorseMap = new Dictionary<string, char>(){
            { "22222",'0'},{"12222",'1'},{"11222",'2'},{"11122",'3'},{"11112",'4'},{"11111",'5'},{"21111",'6'},{"22111",'7'},{"22211",'8'},{"22221",'9'},{"1211",'ㄱ'},{"1121",'ㄴ'},{"2111",'ㄷ'},{"1112",'ㄹ'},{"22",'ㅁ'},{"122",'ㅂ'},{"221",'ㅅ'},{"212",'ㅇ'},{"1221",'ㅈ'},{"2121",'ㅊ'},{"2112",'ㅋ'},{"2211",'ㅌ'},{"222",'ㅍ'},{"1222",'ㅎ'},{"1",'ㅏ'},{"2212",'ㅐ'},{"11",'ㅑ'},{"2",'ㅓ'},{"2122",'ㅔ'},{"111",'ㅕ'},{"12",'ㅗ'},{"21",'ㅛ'},{"1111",'ㅜ'},{"121",'ㅠ'},{"211",'ㅡ'},{"112",'ㅣ'}
        };
        public UpDownToMorse()
        {
            buffer = "";
        }
        public string TranslatedText(){
            _text = Decode(buffer+"0000");
            return _text;
        }
        public void AddToBuffer(long interval,char type,char lang)
        {
            if (type == 'n')
            {
                if (interval > oneSentenceThreshold)
                {
                    buffer += "0000";
                }
                else if (interval > oneWordThreshold)
                {
                    buffer += "00";
                }
                else if (interval > oneLetterThreshold)
                {
                    buffer += "0";
                }
            }
            else if (type == 'p')
            {
                if (interval > shortThreshold)
                {
                    buffer += lang+"2";
                }
                else
                {
                    buffer += lang+"1";
                }
            }
            else
            {
                System.Windows.MessageBox.Show(type.ToString());
            }   
        }
        private string[] CheckLang(string code)
        {
            char lang = code[0];
            string desugared = ""+code[1];
            for (int i = 2; i < code.Length; i += 2)
            {
                if (code[i] != lang)
                {
                    return new string[]{"?","Not same language"};
                }
                desugared += code[i + 1];
            }
            return new string[]{lang.ToString(),desugared};
        }
        private string Decode(string buffer)
        {
            //parse words by 0
            int nextStart = 0;
            string decoded = "";
            System.Diagnostics.Debug.Print(buffer);
            for (int i = 0; i < buffer.Length; i++)
            {
                if (buffer[i] == '0')
                {
                    string[] temp = CheckLang(buffer.Substring(nextStart, i - nextStart));
                    char lang = temp[0][0];
                    string code = temp[1];
                    System.Diagnostics.Debug.Print("\nogcd:" + buffer.Substring(nextStart, i - nextStart) + "\ncode" + code + "\nlang:" + lang);
                    char letter;
                    char punctuation=' ';
                    int j = 1;
                    try
                    {
                        if (lang == 'a')
                        {
                            letter = alphaMorseMap[code];
                        }
                        else if (lang == 'k')
                        {
                            letter = korMorseMap[code];
                        }
                        else
                        {
                            letter = '?';
                        }
                    }
                    catch
                    {
                        letter = '?';
                    }
                    while (i+j<buffer.Length && buffer[i + j] == '0')
                    {
                        j++;
                    }
                    if (j == 1)
                    {
                        decoded += letter;
                        i += j - 1;
                        nextStart = i+1;
                        continue;
                    }
                    else if (j == 2)
                    {
                        //do nothing cuz default punctuation is already ' '
                    }
                    else if (j == 4)
                    {
                        punctuation = '.';
                    }
                    decoded += letter.ToString()+punctuation.ToString();
                    i +=j - 1;
                    nextStart = i+1;
                }
            }
            System.Diagnostics.Debug.Print(decoded);
            return decoded;
        }
    }
}
