using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            char[,] tab = new char[3, 10] { { 's', 'i', 'o', 'e', 'r', 'a', 't', 'n', '~', '~' },
                { 'c', 'x', 'u', 'd', 'j', 'p', 'z', 'b', 'k', 'q' },
                { '.', 'w', 'f', 'l', '/', 'g', 'm', 'y', 'h', 'v' }};
            string text = richTextBox1.Text;
            text = text.ToLower();
            // заменим пробелы на /
            text = text.Replace(' ', '/');
            char[] tx = new char[text.Length];
            string cip = null;
            for (int i = 0; i < text.Length; i++)
            {
                tx[i] = Convert.ToChar(text[i]);
                if (i - 1 != -1 && (Convert.ToInt32(tx[i - 1]) <= Convert.ToInt32('9') && 
                    Convert.ToInt32(tx[i - 1]) >= Convert.ToInt32('0')) && (Convert.ToInt32(tx[i]) > Convert.ToInt32('9')
                    || Convert.ToInt32(tx[i]) < Convert.ToInt32('0')))
                    cip = cip + "94";               
                if (Convert.ToInt32(tx[i]) <= Convert.ToInt32('9') && Convert.ToInt32(tx[i]) >= Convert.ToInt32('0'))
                {
                    if (i - 1 == -1)
                         cip = cip + "94" + tx[i] + tx[i];
                    if (i - 1 != -1 && (Convert.ToInt32(tx[i - 1]) > Convert.ToInt32('9') || 
                        Convert.ToInt32(tx[i - 1]) < Convert.ToInt32('0')))
                         cip = cip + "94" + tx[i] + tx[i];
                    if (i - 1 != -1 && Convert.ToInt32(tx[i - 1]) <= Convert.ToInt32('9') && 
                        Convert.ToInt32(tx[i - 1]) >= Convert.ToInt32('0'))
                         cip = cip + tx[i] + tx[i];
                    if (i + 1 == text.Length)
                         cip = cip + "94";
                }
                if ((Convert.ToInt32(tx[i]) >= Convert.ToInt32('a') && Convert.ToInt32(tx[i]) <= Convert.ToInt32('z'))
                || Convert.ToInt32(tx[i]) == Convert.ToInt32('.') ||
                Convert.ToInt32(tx[i]) == Convert.ToInt32('/'))
                {
                    int x = -1, y = -1;
                    for (int k = 0; k < 3; k++)
                        for (int l = 0; l < 10; l++)
                        {
                            if (tx[i] == tab[k, l])
                            {
                                x = k;
                                y = l;
                                break;
                            }
                        }
                    if (x == 0)
                        cip += y;
                    if (x == 1)
                        cip += "8" + y;
                    if (x == 2)
                        cip += "9" + y;
                }                
            }
            int[] cip1 = new int[cip.Length]; //клер
            int[] cip2 = new int[cip.Length]; //гамма
            for (int l = 0; l < cip.Length; l++)
            {
                cip1[l] = Convert.ToInt32(cip[l]) - Convert.ToInt32('0');
                cip2[l] = Convert.ToInt32(richTextBox4.Text[l]) - Convert.ToInt32('0');
            }
            //сложение гаммы и клера
            int[] cip3 = new int[cip.Length]; // шифр
            for (int i = 0; i < cip.Length; i++)
            {
                if (cip1[i] + cip2[i] >= 10)
                    cip3[i] = cip1[i] + cip2[i] - 10;
                else cip3[i] = cip1[i] + cip2[i];
                richTextBox2.Text += cip3[i];
            }
        }
        private void Button2_Click(object sender, EventArgs e)
        {
            char[,] tab = new char[3, 10] { { 's', 'i', 'o', 'e', 'r', 'a', 't', 'n', '~', '~' },
                { 'c', 'x', 'u', 'd', 'j', 'p', 'z', 'b', 'k', 'g' },
                { '.', 'w', 'f', 'l', '/', 'g', 'm', 'y', 'h', 'v' }};
            char[] tx = new char[richTextBox2.Text.Length];
            // Расшифровка
            int[] cip1 = new int[richTextBox2.Text.Length]; //клер
            int[] cip2 = new int[richTextBox2.Text.Length]; //гамма
            int[] cip3 = new int[richTextBox2.Text.Length]; // шифр
            for (int l = 0; l < richTextBox2.Text.Length; l++)
            {
                cip3[l] = Convert.ToInt32(richTextBox2.Text[l]) - Convert.ToInt32('0');
                cip2[l] = Convert.ToInt32(richTextBox4.Text[l]) - Convert.ToInt32('0');
            }
            for (int i = 0; i < cip3.Length; i++)
            {
                if (cip3[i] - cip2[i] < 0)
                    cip1[i] = 10 + cip3[i] - cip2[i];
                else cip1[i] = cip3[i] - cip2[i];                
            }
            // разбиваем текст на части              
            string str1 = null;
            for (int i = 0; i < cip1.Length; i++)
                str1 += cip1[i];            
            str1 = str1.Replace("94", "*");
            char[] str = str1.ToCharArray();
            for (int i = 0; i < str.Length; i++)
                if (str[i] == '*')
                {
                    int k = -1;
                    for ( k = i; k < str.Length; k++)
                        if (str[k] == '*')
                        {
                            if ((k - i) % 2 == 0)
                                continue;
                            bool p = false;
                            for (int t = i + 2; t < k; t += 2)
                            {
                                if (str[t] == str[t - 1])
                                    p = true;
                                else p = false;
                            }
                            if (p)
                            {
                                str[i] = '#';
                                str[k] = '#';
                            }
                            i = k;
                        }
                }
            // расшифруем клер
            string s = null;
            for (int i=0; i< str.Length; i++)
            {
                int a = Convert.ToInt32(str[i]) - Convert.ToInt32('0');
                if (a == 8)
                {
                    i++;
                    int p= Convert.ToInt32(str[i]) - Convert.ToInt32('0');
                    s += tab[1, p].ToString();
                    continue;
                }
                if (a == 9 )
                {
                    i++;
                    int p = Convert.ToInt32(str[i]) - Convert.ToInt32('0');
                    s += tab[2, p].ToString();
                    continue;
                }
                if (a >= 0 && a <= 7)
                {
                    s += tab[0, a].ToString();
                    continue;
                }
                if (str[i] == '*' )
                {
                    s += ' '.ToString();
                    continue;
                }                
                if (str[i] == '#')
                {
                    for ( i++; str[i]!='#'; i+=2 )
                    s += str[i].ToString();                    
                }
            }
            s = s.ToUpper();
            richTextBox3.Text += s; 
        }
    }
}
