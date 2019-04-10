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
    /// Форма для редактирования записи тоннеля
    /// </summary>
    public partial class EditRowForm : Form
    {
        /// <summary>
        /// Список тоннелей
        /// </summary>
        internal TunnelList tunnelList;
        /// <summary>
        /// Выбранный индекс списка
        /// </summary>
        internal int index;
        /// <summary>
        /// Выбраный тоннель
        /// </summary>
        internal Tunnel tunnel;
        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public EditRowForm()
        {
            InitializeComponent();

        }

        /// <summary>
        /// Инициализирует форму
        /// </summary>
        public void SetupForm()
        {
            string[] headings = tunnelList.headings;
            tunnel = tunnelList.tunnels[index];
            label1.Text = headings[1];
            label2.Text = headings[2] + " global_id";
            label3.Text = headings[2] + " value";
            label4.Text = headings[3];
            label5.Text = headings[4];
            label6.Text = headings[5];
            label7.Text = headings[6];
            label8.Text = headings[7];

            string[] fields = tunnelList.GetFields(tunnel);
            textBox1.Text = fields[1];
            textBox2.Text = fields[2];
            textBox3.Text = fields[3];
            textBox4.Text = fields[4];
            textBox5.Text = fields[5];
            textBox6.Text = fields[6];
            textBox7.Text = fields[7];
            textBox8.Text = fields[8];


            //textBox2.Text = tunnelList.innerTunnelList.idGenerator.NextID().ToString();
            //textBox8.Text = tunnelList.idGenerator.NextID().ToString();
            textBox2.Enabled = false;
            textBox8.Enabled = false;

        }

        /// <summary>
        /// Редактирует данные из таблицы
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
                tunnelList.EditTunnel(fields, index);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Проверьте корректность введенных данных.", "Произошла ощибка при отредактировании записи Тоннеля", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
