using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Cipher.Tests
{
    [TestClass]
    public class ChiperTests
    {
        [TestMethod]
        public void Vigenere_приветandпока_яяувфб()
        {
            // arrange
            string a = "привет";
            string b = "пока";
            string expected = "яяувфб";

            // act

            MainWindow mainWindow = new MainWindow();
            string actual = mainWindow.Vigenere(a, b, true);

            // assert
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void Vigenere_приветandempty_empty()
        {
            // arrange
            string a = "привет";
            string b = "";
            string expected = "";

            // act
            MainWindow mainWindow = new MainWindow();
            string actual = mainWindow.Vigenere(a, b, true);

            // assert
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void Vigenere_englishandпока_english()
        {
            // arrange
            string a = "english";
            string b = "пока";
            string expected = "english";

            // act
            MainWindow mainWindow = new MainWindow();
            string actual = mainWindow.Vigenere(a, b, true);

            // assert
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void Vigenere_приветhiandпока_яяувфбhi()
        {
            // arrange
            string a = "приветhi";
            string b = "пока";
            string expected = "яяувфбhi";

            // act
            MainWindow mainWindow = new MainWindow();
            string actual = mainWindow.Vigenere(a, b, true);

            // assert
            Assert.AreEqual(expected, actual);
        }
    }
}
