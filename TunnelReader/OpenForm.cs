using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TunnelReader
{
    /// <summary>
    /// Форма для открытия файла
    /// </summary>
    public partial class OpenForm : Form
    {
        /// <summary>
        /// Список тоннелей
        /// </summary>
        internal TunnelList tunnelList;
        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public OpenForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Проверяет является ли строка числом
        /// </summary>
        private bool IsValidNumber(string str)
        {
            int id;
            return int.TryParse(str, out id) && id > 0;
        }

        /// <summary>
        /// Возвращает путь к выбранному файлу
        /// </summary>
        private void browseButton_Click(object sender, EventArgs e)
        {
            openFileDialog.ShowDialog();
            pathTextBox.Text = openFileDialog.FileName;
            openButton.Enabled = true;
        }

        /// <summary>
        /// Возвращает количество строк файла
        /// </summary>
        private int GetLinesCount(string pathToFile)
        {
            int count = 0;
            FileStream fileStream = new FileStream(pathToFile, FileMode.Open);
            using (StreamReader streamReader = new StreamReader(fileStream))
            {
                while (streamReader.ReadLine() != null)
                    count++;
                streamReader.Close();
            }
            return count;
        }

        /// <summary>
        /// Открывает выбранный файл
        /// </summary>
        private void openButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsValidNumber(indexTextBox.Text))
                    throw new ArgumentException("Неправильно введен начальный индекс.");
                if (!IsValidNumber(countTextBox.Text) ||
                    int.Parse(countTextBox.Text) > GetLinesCount(pathTextBox.Text) - int.Parse(indexTextBox.Text) + 1 + ((headingsCheckBox.Checked) ? -1 : 0))
                    throw new ArgumentException("Неправильно введено количество отображаемых элементов.");


                tunnelList = TunnelList.NewFromFile(pathTextBox.Text,
                    headingsCheckBox.Checked ? CSVType.WithHeadings : CSVType.WithoutHeading,
                    int.Parse(indexTextBox.Text), int.Parse(countTextBox.Text));
                this.Close();
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show("Невозможно открыть файл так как он не допустен для пользователья", "Произошла ощибка при открытии файла.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (IOException ex)
            {
                MessageBox.Show("Проверьте существует ли выбранный файл, используется ли в других приложениях.", "Произошла ощибка при открытии файла.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Проверьте корректность файла.", "Произошла ощибка при открытии файла.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
