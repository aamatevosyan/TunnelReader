using System.Collections.Generic;

namespace TunnelReader
{
    /// <summary>
    /// Класс для работы со списком Районов
    /// </summary>
    internal class AreaList
    {
        /// <summary>
        /// Список районов
        /// </summary>
        private readonly List<Area> areas = new List<Area>();

        /// <summary>
        /// Возвращает район с задаными значениями
        /// </summary>
        public Area Get(string admArea, string district)
        {
            var area = new Area(admArea, district);
            var result = areas.Find(a => a.Equals(area));
            if (result != null) return result;

            areas.Add(area);
            return area;
        }
    }
}