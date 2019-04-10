using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace TunnelReader
{
    /// <summary>
    /// Тоннель
    /// </summary>
    class Tunnel
    {
        /// <summary>
        /// Долгота
        /// </summary>
        /// <value>Долгота</value>
        public double Longitude { get; set; }
        /// <summary>
        /// Широта
        /// </summary>
        /// <value>Широта</value>
        public double Latitude { get; set; }
        /// <summary>
        /// ID тоннеля
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Внутренный тоннель
        /// </summary>
        public InnerTunnel innerTunnel;
        /// <summary>
        /// Имя тоннеля
        /// </summary>
        /// <value>Имя тоннеля</value>
        public string Name { get; set; }
        /// <summary>
        /// Район тоннеля
        /// </summary>
        public Area area;

        /// <summary>
        /// Возвращает ID внутреннего тоннеля
        /// </summary>
        public int GetGlobalID()
        {
            return innerTunnel.global_id;
        }
    }


}
