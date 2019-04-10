using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace TunnelReader
{
    /// <summary>
    /// Класс для работы с данными
    /// </summary>
    internal class TunnelList
    {
        /// <summary>
        /// Список тоннелей
        /// </summary>
        public List<Tunnel> tunnels;
        /// <summary>
        /// Тип Файла
        /// </summary>
        public CSVType fileType;
        /// <summary>
        /// Список Районов
        /// </summary>
        public AreaList areaList;

        /// <summary>
        /// Список внутренных тоннелей
        /// </summary>
        public InnerTunnelList innerTunnelList;
        /// <summary>
        /// Таблица данных
        /// </summary>
        public DataTable dataTable;
        /// <summary>
        /// Число столбцов в таблице
        /// </summary>
        private const int fieldsCount = 9;
        /// <summary>
        /// Заголовки
        /// </summary>
        public string[] headings;
        /// <summary>
        /// Путь к файлу
        /// </summary>
        public string filePath;
        /// <summary>
        /// Класс для генерации ID
        /// </summary>
        public IDGenerator idGenerator;

        /// <summary>
        /// Создает новый обьект из файла
        /// </summary>
        public static TunnelList NewFromFile(string pathToFile, CSVType fileType, int from, int count)
        {
            TunnelList tunnelList = new TunnelList();
            tunnelList.areaList = new AreaList();
            tunnelList.innerTunnelList = new InnerTunnelList();
            tunnelList.tunnels = new List<Tunnel>();
            tunnelList.filePath = pathToFile;
            tunnelList.idGenerator = new IDGenerator();
            tunnelList.fileType = fileType;

            var fileStream = new FileStream(pathToFile, FileMode.Open);
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
            {
                if (CSVType.WithHeadings == fileType)
                    tunnelList.headings = streamReader.ReadLine()
                        .Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                else
                    tunnelList.headings = new string[]
                        {"Index", "Name", "Тunnel", "Admin. Area", "District", "Longitude", "Latitude", "ID"};
                tunnelList.SetupDatatable();

                string line;
                Tunnel tunnel;
                for (var index = 1; index < from; index++)
                    streamReader.ReadLine();
                for (var index = 0; index != count && (line = streamReader.ReadLine()) != null && line != ""; index++)
                {
                    tunnel = tunnelList.GetTunnel(line);
                    tunnelList.tunnels.Add(tunnel);
                    tunnelList.idGenerator.AddID(tunnel.ID);
                    tunnelList.dataTable.Rows.Add(tunnelList.GetFields(tunnel));
                }

                tunnelList.UpdateRowIndexes();
            }

            return tunnelList;
        }

        /// <summary>
        /// Сохраняет обьект в файл
        /// </summary>

        public void SaveToFile(string filePath)
        {
            var fileStream = new FileStream(filePath, FileMode.Create);
            using (var streamWriter = new StreamWriter(fileStream))
            {
                streamWriter.WriteLine(GetHeadings());
                for (var i = 0; i < tunnels.Count; i++)
                    streamWriter.WriteLine(GetCSVLine(tunnels[i], i + 1));
                streamWriter.Flush();
            }
        }

        /// <summary>
        /// Добавляет тоннель к списку
        /// </summary>
        public void AddTunnel(string[] fields)
        {
            var tunnel = GetTunnel(fields);
            dataTable.Rows.Add(fields);
            tunnels.Add(tunnel);
        }

        /// <summary>
        /// Редактирует заданный тоннель
        /// </summary>
        public void EditTunnel(string[] fields, int index)
        {
            var tunnel = GetTunnel(fields);
            dataTable.Rows[index].ItemArray = fields;
            tunnels[index] = tunnel;
        }

        /// <summary>
        /// Удаляет заданный тоннель
        /// </summary>
        public void DeleteTunnel(int index)
        {
            tunnels.RemoveAt(index);
            dataTable.Rows.RemoveAt(index);
            UpdateRowIndexes();
        }

        /// <summary>
        /// Возвращает тоннель с задаными данными
        /// </summary>
        public Tunnel GetTunnel(string csvLine)
        {
            var fields = csvLine.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            for (var i = 0; i < fields.Count; i++) fields[i] = fields[i].Trim('"');

            string strID, strValue;

            InnerTunnel.DecodeTunnelData(fields[2], out strID, out strValue);
            fields.Insert(2, strID);
            fields[3] = strValue;

            return GetTunnel(fields.ToArray());
        }

        /// <summary>
        /// Возвращает тоннель с задаными данными
        /// </summary>
        public Tunnel GetTunnel(string[] fields)
        {
            for (var i = 1; i < fields.Length; i++)
                if (fields[i] == "")
                    throw new ArgumentException("Некоторые из данных пусты!!!");

            var tunnel = new Tunnel();
            tunnel.Name = fields[1];

            tunnel.Latitude = double.Parse(fields[6]);
            tunnel.Longitude = double.Parse(fields[7]);
            tunnel.ID = int.Parse(fields[8]);

            tunnel.area = areaList.Get(fields[4], fields[5]);
            tunnel.innerTunnel = innerTunnelList.Get(int.Parse(fields[2]), fields[3]);
            return tunnel;
        }

        /// <summary>
        /// Возвращает значения для столбцов
        /// </summary>
        public string[] GetFields(Tunnel tunnel)
        {
            return new[]
            {
                "", tunnel.Name, tunnel.innerTunnel.global_id.ToString(), tunnel.innerTunnel.value, tunnel.area.AdmArea,
                tunnel.area.District, tunnel.Longitude.ToString(), tunnel.Latitude.ToString(), tunnel.ID.ToString()
            };
        }

        /// <summary>
        /// Обновляет индексы строк
        /// </summary>
        public void UpdateRowIndexes()
        {
            for (var i = 0; i < dataTable.Rows.Count; i++)
                dataTable.Rows[i][0] = (i + 1).ToString();
        }

        /// <summary>
        /// Иницилизирует таблицу
        /// </summary>
        public void SetupDatatable()
        {
            dataTable = new DataTable();
            dataTable.Columns.Add(headings[0]);
            dataTable.Columns.Add(headings[1]);
            dataTable.Columns.Add(headings[2] + " global_id");
            dataTable.Columns.Add(headings[2] + " value");
            dataTable.Columns.Add(headings[3]);
            dataTable.Columns.Add(headings[4]);
            dataTable.Columns.Add(headings[5]);
            dataTable.Columns.Add(headings[6]);
            dataTable.Columns.Add(headings[7]);

        }

        /// <summary>
        /// Возвращает заголовки
        /// </summary>
        public string GetHeadings()
        {
            return string.Join(";", headings);
        }

        /// <summary>
        /// Возвращает строку данных CSV
        /// </summary>
        public string GetCSVLine(Tunnel tunnel, int index)
        {
            var fields = new List<string>();
            fields.Add(index.ToString());
            fields.Add(tunnel.Name);
            fields.Add(InnerTunnel.EncodeTunnelData(tunnel.innerTunnel));
            fields.Add(tunnel.area.AdmArea);
            fields.Add(tunnel.area.District);
            fields.Add(tunnel.Longitude.ToString());
            fields.Add(tunnel.Latitude.ToString());
            fields.Add(tunnel.ID.ToString());

            for (var i = 0; i < fields.Count; i++) fields[i] = "\"" + fields[i] + "\"";
            return string.Join(";", fields);
        }

        /// <summary>
        /// Обновляет таблицу
        /// </summary>
        public void UpdateTable()
        {
            dataTable.Rows.Clear();
            idGenerator = new IDGenerator();
            for (var i = 0; i < tunnels.Count; i++)
            {
                dataTable.Rows.Add(GetFields(tunnels[i]));
                idGenerator.AddID(tunnels[i].ID);
            }

            UpdateRowIndexes();
        }
    }

    /// <summary>
    /// Показывает нужно ли окрыть файл CSV с заголовкой или без
    /// </summary>
    internal enum CSVType
    {
        WithHeadings,
        WithoutHeading
    }
}