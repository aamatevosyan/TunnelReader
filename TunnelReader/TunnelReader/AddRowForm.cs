using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TunnelReader
{
    /// <summary>
    /// Форма для создания новой записи тоннеля
    /// </summary>
    public partial class AddRowForm : Form
    {
        /// <summary>
        /// Список тоннелей
        /// </summary>
        internal TunnelList tunnelList;
        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public AddRowForm()
        {
            InitializeComponent();

        }

        /// <summary>
        /// Инициализирует форму
        /// </summary>
        public void SetupForm()
        {
            string[] headings = tunnelList.headings;
            label1.Text = headings[1];
            label2.Text = headings[2] + " global_id";
            label3.Text = headings[2] + " value";
            label4.Text = headings[3];
            label5.Text = headings[4];
            label6.Text = headings[5];
            label7.Text = headings[6];
            label8.Text = headings[7];

            textBox2.Text = tunnelList.innerTunnelList.idGenerator.NextID().ToString();
            textBox8.Text = tunnelList.idGenerator.NextID().ToString();
            textBox2.Enabled = false;
            textBox8.Enabled = false;
        }

        /// <summary>
        /// Добавляет данные в таблицу
        /// </summary>
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string[] fields = new string[]
                {
                    "", textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text, textBox6.Text,
                    textBox7.Text, textBox8.Text
                };
                tunnelList.AddTunnel(fields);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Проверьте корректность введенных данных.", "Произошла ощибка при создании новой записи Тоннеля", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
