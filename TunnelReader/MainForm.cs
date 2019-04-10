using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace TunnelReader
{
    /// <summary>
    ///     Главная форма программы
    /// </summary>
    public partial class MainForm : Form
    {
        /// <summary>
        ///     Значение тоннелей до соверщенного действия
        /// </summary>
        private List<Tunnel> oldTunnels = new List<Tunnel>();

        /// <summary>
        ///     Список тоннелей
        /// </summary>
        private TunnelList tunnelList;

        /// <summary>
        ///     Конструктор по умолчанию
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
        }

        /// <summary>
        ///     Добавляет новое значение в таблицу
        /// </summary>
        private void addButton_Click(object sender, EventArgs e)
        {
            if (tunnelList != null)
            {
                BackupResult();
                var addRowForm = new AddRowForm();
                addRowForm.tunnelList = tunnelList;
                addRowForm.SetupForm();
                addRowForm.ShowDialog();
                tunnelList.UpdateRowIndexes();
            }
        }

        /// <summary>
        ///     Показывает окно для последуешого открытия файла
        /// </summary>
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var openForm = new OpenForm();
            openForm.ShowDialog();
            if (openForm.tunnelList != null)
            {
                tunnelList = openForm.tunnelList;
                dataGridView.DataSource = tunnelList.dataTable;

                sortFieldComboBox.ComboBox.Items.Clear();
                sortFieldComboBox.ComboBox.Items.Add(tunnelList.headings[1]);
                sortFieldComboBox.ComboBox.Items.Add(tunnelList.headings[3]);
                sortFieldComboBox.ComboBox.SelectedIndex = 0;

                filterFieldComboBox.ComboBox.Items.Clear();
                filterFieldComboBox.ComboBox.Items.Add(tunnelList.headings[2] + " global_id");
                filterFieldComboBox.ComboBox.Items.Add(tunnelList.headings[3]);
                filterFieldComboBox.ComboBox.SelectedIndex = 0;

                foreach (DataGridViewColumn column in dataGridView.Columns)
                    column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        /// <summary>
        ///     Пересохраняет данные
        /// </summary>
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                tunnelList.SaveToFile(tunnelList.filePath);
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show("Невозможно сохранить файл так как он не доступен для пользователья",
                    "Произошла ощибка при сохранении файла.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (IOException ex)
            {
                MessageBox.Show("Проверьте существует ли выбранный файл, используется ли в других приложениях.",
                    "Произошла ощибка при сохранении файла.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Проверьте корректность файла.", "Произошла ощибка при сохранении файла.",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        ///     Сохраняет данные в заданый файл
        /// </summary>
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                saveFileDialog.ShowDialog();
                if (saveFileDialog.FileName != "") tunnelList.SaveToFile(saveFileDialog.FileName);
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show("Невозможно сохранить файл так как он не доступен для пользователья",
                    "Произошла ощибка при сохранении файла.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (IOException ex)
            {
                MessageBox.Show("Проверьте существует ли выбранный файл, используется ли в других приложениях.",
                    "Произошла ощибка при сохранении файла.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Проверьте корректность файла.", "Произошла ощибка при сохранении файла.",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        ///     Сортирует данные в алфавитном порядке
        /// </summary>
        private void sortAZButton_Click(object sender, EventArgs e)
        {
            if (tunnelList != null)
            {
                BackupResult();
                if (sortFieldComboBox.SelectedIndex == 0)
                    tunnelList.tunnels.Sort((t1, t2) => t1.Name.CompareTo(t2.Name));
                else
                    tunnelList.tunnels.Sort((t1, t2) =>
                    {
                        var res = t1.area.AdmArea.CompareTo(t2.area.AdmArea);
                        if (res == 0 && t1.Name != t2.Name)
                            return t1.Name.CompareTo(t2.Name);
                        return res;
                    });

                tunnelList.UpdateTable();
            }
        }

        /// <summary>
        ///     Сортирует данные по количеству округов
        /// </summary>
        private void sortNumButton_Click(object sender, EventArgs e)
        {
            if (tunnelList != null && tunnelList.tunnels.Count > 0)
            {
                BackupResult();
                var districtsDictionary = new Dictionary<string, int>();
                for (var i = 0; i < tunnelList.tunnels.Count; i++)
                {
                    var data = districtsDictionary.ContainsKey(tunnelList.tunnels[i].area.AdmArea)
                        ? districtsDictionary[tunnelList.tunnels[i].area.AdmArea] + 1
                        : 1;
                    districtsDictionary[tunnelList.tunnels[i].area.AdmArea] = data;
                }

                tunnelList.tunnels.Sort((t1, t2) =>
                {
                    var res = districtsDictionary[t1.area.AdmArea].CompareTo(districtsDictionary[t2.area.AdmArea]);
                    if (res == 0 && t1.area.AdmArea != t2.area.AdmArea)
                        return t1.area.AdmArea.CompareTo(t2.area.AdmArea);
                    return res;
                });
                tunnelList.UpdateTable();
            }
        }

        /// <summary>
        ///     Фильтрирует данные
        /// </summary>
        private void filterButton_Click(object sender, EventArgs e)
        {
            if (tunnelList != null && tunnelList.tunnels.Count > 0)
            {
                BackupResult();
                if (filterFieldComboBox.SelectedIndex == 0)
                    tunnelList.tunnels = tunnelList.tunnels
                        .Where(t => t.GetGlobalID().ToString().IndexOf(filterTextBox.Text) == 0)
                        .ToList();
                else
                    tunnelList.tunnels = tunnelList.tunnels
                        .Where(t => t.area.AdmArea.IndexOf(filterTextBox.Text) == 0)
                        .ToList();

                tunnelList.UpdateTable();
            }
        }

        /// <summary>
        ///     Востанавливает данные до совершенного действия
        /// </summary>
        private void undoButton_Click(object sender, EventArgs e)
        {
            if (oldTunnels != null && oldTunnels.Count != 0)
            {
                tunnelList.tunnels = oldTunnels;
                tunnelList.UpdateTable();
                oldTunnels = null;
            }
        }

        /// <summary>
        ///     Удаляет значения из таблицы
        /// </summary>
        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (tunnelList != null && tunnelList.tunnels.Count > 0)
            {
                BackupResult();
                var index = dataGridView.SelectedRows[0].Index;
                tunnelList.DeleteTunnel(index);
            }
        }

        /// <summary>
        ///     Редактирует значение из таблицы
        /// </summary>
        private void editButton_Click(object sender, EventArgs e)
        {
            if (tunnelList != null && tunnelList.tunnels.Count > 0)
            {
                BackupResult();
                var index = dataGridView.SelectedRows[0].Index;
                var editRowForm = new EditRowForm();
                editRowForm.tunnelList = tunnelList;
                editRowForm.index = index;
                editRowForm.SetupForm();
                editRowForm.ShowDialog();
                tunnelList.UpdateRowIndexes();
            }
        }

        /// <summary>
        ///     Сохраняет список тоннелей для последуешего востановления
        /// </summary>
        public void BackupResult()
        {
            oldTunnels = new List<Tunnel>(tunnelList.tunnels);
        }

        /// <summary>
        /// Завершает программу
        /// </summary>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}