using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TunnelReader
{
    /// <summary>
    /// Класс Район
    /// </summary>
    class Area
    {
        /// <summary>
        /// Административный округ
        /// </summary>
        public string AdmArea { get; set; }
        /// <summary>
        /// Район округа
        /// </summary>
        public string District { get; set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        public Area(string admArea, string district)
        {
            AdmArea = admArea;
            District = district;
        }

        /// <summary>
        /// Переопределенный метод для сравнения по значению
        /// </summary>
        public override bool Equals(object obj)
        {
            Area area = obj as Area;
            return (area.AdmArea == this.AdmArea) && (area.District == this.District);
        }
    }
}
