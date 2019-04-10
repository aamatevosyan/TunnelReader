using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TunnelReader
{
    /// <summary>
    /// Внутренный тоннель
    /// </summary>
    class InnerTunnel
    {
        /// <summary>
        /// ID Внутренного тоннеля
        /// </summary>
        public int global_id;
        /// <summary>
        /// Значение внутреннего тоннеля
        /// </summary>
        public string value;

        /// <summary>
        /// Конструктор
        /// </summary>
        public InnerTunnel(int global_id, string value)
        {
            this.global_id = global_id;
            this.value = value;
        }

        /// <summary>
        /// Декодирует данные из строки CSV
        /// </summary>
        public static void DecodeTunnelData(string encoded, out string strID, out string strValue)
        {
            const string _id = "\"\"global_id\"\": ";
            const string _idDel = ", ";

            const string _value = "\"\"value\"\": \"\"";
            const string _valueDel = "\"\" }";

            int startForID = encoded.IndexOf(_id) + _id.Length;
            int endForID = encoded.IndexOf(_idDel);
            strID = encoded.Substring(startForID, endForID - startForID);


            int startForValue = encoded.IndexOf(_value) + _value.Length;
            int endForValue = encoded.IndexOf(_valueDel);
            strValue = encoded.Substring(startForValue, endForValue - startForValue);
        }

        /// <summary>
        /// Кодирует данные
        /// </summary>
        public static string EncodeTunnelData(string strID, string strValue)
        {
            return $"{{ \"\"global_id\"\": {strID}, \"\"value\"\": \"\"{strValue}\"\" }}";
        }

        /// <summary>
        /// Кодирует данные 
        /// </summary>
        public static string EncodeTunnelData(InnerTunnel innerTunnel)
        {
            return EncodeTunnelData(innerTunnel.global_id.ToString(), innerTunnel.value);
        }

        /// <summary>
        /// Переопределенный метод для сравнения по значению
        /// </summary>
        public override bool Equals(object obj)
        {
            InnerTunnel innerTunnel = obj as InnerTunnel;
            return (this.global_id == innerTunnel.global_id) && (this.value == innerTunnel.value);
        }
    }
}
