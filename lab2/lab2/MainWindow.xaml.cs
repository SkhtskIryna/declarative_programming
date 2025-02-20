using System;
using System.Collections.Generic;
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
using Microsoft.Win32;

namespace lab2
{
    /// <summary>
    /// Логіка взаємодії для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //Створення прив'язки та приєднання обробників
            CommandBinding saveCommand = new CommandBinding(ApplicationCommands.Save, execute_Save, canExecute_Save);
            CommandBinding openCommand = new CommandBinding(ApplicationCommands.Open, execute_Open, canExecute_Open);
            CommandBinding clearCommand = new CommandBinding(ApplicationCommands.Cut, execute_Clear, canExecute_Clear);
            
            //Реєстрація прив'язки
            CommandBindings.Add(saveCommand);
            CommandBindings.Add(openCommand);
            CommandBindings.Add(clearCommand);
        }

        /// <summary>
        /// Перевірка введеного тексту
        /// </summary>
        void canExecute_Save(object sender, CanExecuteRoutedEventArgs e)
        {
            if (inputTextBox.Text.Trim().Length > 0) {
                e.CanExecute = true;
            }
            else {
                e.CanExecute = false;
            }
        }

        /// <summary>
        /// Збереження тексту у файл saveFile.txt
        /// </summary>
        void execute_Save(object sender, ExecutedRoutedEventArgs e)
        {
            System.IO.File.WriteAllText("C:\\Users\\IrA\\Desktop\\Декларат. прогр\\labs\\lab2\\lab2\\saveFile.txt", inputTextBox.Text);
            MessageBox.Show("Файл збережено!");
        }

        /// <summary>
        /// Перевірка доступності відкриття файлу
        /// </summary>
        void canExecute_Open(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = inputTextBox != null;
        }

        /// <summary>
        /// Відкриття файла та вставка його вмісту у TextBox
        /// </summary>
        void execute_Open(object sender, ExecutedRoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "Виберіть файл"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    string fileContent = System.IO.File.ReadAllText(openFileDialog.FileName);
                    inputTextBox.Text = fileContent; // Вставка вмісту файла у TextBox
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Помилка відкриття файлу: {ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        /// <summary>
        /// Перевірка можливості очищення (чи існує текст)
        /// </summary>
        void canExecute_Clear(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = inputTextBox != null && !string.IsNullOrWhiteSpace(inputTextBox.Text);
        }

        /// <summary>
        /// Очищення вмісту TextBox
        /// </summary>
        void execute_Clear(object sender, ExecutedRoutedEventArgs e)
        {
            inputTextBox.Clear();
        }
    }
}
