using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BFInterpreter
{
    public partial class Form1 : Form
    {

        char[] mainAr = new char[900];
        char[] code = new char[900];
        int[] input = new int[900];

        private string text = "";

        public Form1()
        {
            InitializeComponent();
        }

        private void start(String code, String input)
        {
            clearMainAr();
            getCode(code);
            getInput(input);
            translate();
            writeText();
        }

        private void translate()
        {
            bool loopOpen = false;
            int position = 0;
            int z = 0;
            //Main-Loop
            for (int i = 0; i < code.Length; i++)
            {
                switch (code[i])
                {
                    case '<':
                        if (position > 0)
                        {
                            position--;
                        }
                        else
                        {
                            //ERROR
                        }
                        break;
                    case '>':
                        if (position < 900)
                        {
                            position++;
                        }
                        else
                        {
                            //ERROR
                        }
                        break;
                    case '+':
                        mainAr[position]++;
                        break;
                    case '-':
                        if (mainAr[position] > 0)
                        {
                            mainAr[position]--;
                        }
                        else
                        {
                            //ERROR
                        }
                        break;
                    case ',':
                        mainAr[position] = (char)input[z];
                        z++;
                        break;
                    case '.':
                        if (mainAr[position] > (int)31)
                        {
                            text += mainAr[position].ToString();
                        }
                        else
                        {
                            int a = (int)mainAr[position];
                            text += a;
                        }
                        break;
                    case '[':
                        if (mainAr[position] == 0)
                        {
                            //Jump to closing brackets if value of current field is 0. If brackets inside brackets jump over these too.
                            i++;
                            int y = 0;
                            for (int x = i; x < code.Length; x++)
                            {
                                if (code[x] == '[')
                                {
                                    y++;
                                }
                                else if (code[x] == ']' && y > 0)
                                {
                                    y--;
                                }
                                else if (code[x] == ']' && y == 0)
                                {
                                    i = x;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            loopOpen = true;
                        }
                        break;
                    case ']':
                        if (!loopOpen)
                        {
                            //ERROR
                        }
                        else if (loopOpen && mainAr[position] != 0)
                        {
                            int y = 0;
                            int x = i;
                            while (x != 0)
                            {
                                x--;
                                if (code[x] == ']')
                                {
                                    y++;
                                }else if(code[x] == '[' && y > 0){
                                    y--;
                                }
                                else if (code[x] == '[' && y == 0)
                                {
                                    i = x;
                                    break;
                                }
                            }
                        }
                        else if (loopOpen && mainAr[position] == 0)
                        {
                            loopOpen = false;
                        }
                        break;
                }
            }
        }

        private void getInput(string input2)
        {
            if (input2 != "")
            {
                char[] in2 = input2.ToCharArray();
                for (int i = 0; i < in2.Length; i++)
                {
                    input[i] = (int)Char.GetNumericValue(in2[i]);
                }
            }
            else
            {
                return;
            }
        }

        private void clearMainAr()
        {
            for (int i = 0; i < mainAr.Length; i++)
            {
                mainAr[i] = (char)0;
            }
            text = "";
        }

        private void getCode(string code2)
        {
            code = code2.ToCharArray();
        }

        private void writeText()
        {
            txt_output.Text = text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txt_output.Clear();
            start(txt_code.Text, txt_input.Text);
        }

    }
}
