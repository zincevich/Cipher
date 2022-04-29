using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Cipher
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        // открываем файлик
        private void OpenFile(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                before.Text = File.ReadAllText(openFileDialog.FileName, Encoding.UTF8);
            }
            if (before.Text.Contains('�'))
            {
                before.Text = File.ReadAllText(openFileDialog.FileName, Encoding.GetEncoding(1251));
            }

        }
        // шифруем
        private void Cipher(object sender, RoutedEventArgs e)
        {
            Vigenere(before.Text, key.Text, true);
        }
        // дешифруем
        private void Decipher(object sender, RoutedEventArgs e)
        {
            Vigenere(before.Text, key.Text, false);
        }
        // сохраняем файлик
        private void Save(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";

            if (saveFileDialog.ShowDialog() == true)
            {
                using (StreamWriter streamwriter = new StreamWriter(saveFileDialog.FileName))
                {
                    streamwriter.WriteLine(after.Text);
                    streamwriter.Close();
                }

                notifications.Text = "Результат сохранен!";
            }
        }
        // сам шифр
        public static string alphabet = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";

        public string Vigenere(string original, string keyword, bool code)
        {
            string result = "";
            // проверка на пустоту у оригинального текста и ключа
            if (string.IsNullOrWhiteSpace(original))
            {
                notifications.Text = "Сначала введите текст, с которым вы хотите работать!";
                return result;
            }

            if (string.IsNullOrWhiteSpace(keyword))
            {
                notifications.Text = "Сначала введите ключ, с которым вы хотите работать!";
                return result;
            }

            original = original.ToLower();
            keyword = keyword.ToLower();
            // проверка на лишние и неправильные символы в ключе, а также на количество слов в ключе
            foreach (var sym in keyword)
            {
                if (char.IsWhiteSpace(sym))
                {
                    notifications.Text = "Ключ - это одно слово. Введите новый ключ и попробуйте снова!";
                    return result;
                } 

                if (!alphabet.Contains(sym))
                {
                    notifications.Text = "Ключ должен состоять только из кириллических символов. Введите новый ключ и попробуйте снова!";
                    return result;
                }

            }

            int i = 0;
            int c = 0;

            StringBuilder sb = new StringBuilder();

            // шифруем
            foreach (var sym in original)
            {
                if (alphabet.Contains(sym))
                {
                    if (code)
                    {
                        c = (alphabet.IndexOf(sym) + alphabet.IndexOf(keyword[i])) % alphabet.Length;
                    }
                    else
                    {
                        c = (alphabet.IndexOf(sym) - alphabet.IndexOf(keyword[i]) + alphabet.Length) % alphabet.Length;
                    }

                    sb.Append(alphabet[c]);

                    i++;

                    if (i == keyword.Length)
                    {
                        i = 0;
                    }
                }
                else
                {
                    sb.Append(sym);
                }
            }
            result = sb.ToString();
            after.Text = result;
            notifications.Text = "Готово!";
            return result;

        }
    }
}
