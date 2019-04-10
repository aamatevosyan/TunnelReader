using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TunnelReader
{
    /// <summary>
    /// Класс для генерации ID
    /// </summary>
    class IDGenerator
    {
        /// <summary>
        /// Максимальный ID
        /// </summary>
        private int maxId = 1;

        /// <summary>
        /// Добавляет ID
        /// </summary>
        public void AddID(int id)
        {
            if (id > maxId)
                maxId = id;
        }

        /// <summary>
        /// Возвращает следующий ID
        /// </summary>
        public int NextID()
        {
            return maxId++;
        }
    }
}
