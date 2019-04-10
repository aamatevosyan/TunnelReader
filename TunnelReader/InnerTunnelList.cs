using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TunnelReader
{
    /// <summary>
    /// Класс для работы со списком Внутренных тоннелей
    /// </summary>
    class InnerTunnelList
    {
        /// <summary>
        /// Список внутренных тоннелей
        /// </summary>
        private readonly List<InnerTunnel> innerTunnels = new List<InnerTunnel>();
        /// <summary>
        /// Класс для генерирования ID
        /// </summary>
        public IDGenerator idGenerator = new IDGenerator();

        /// <summary>
        /// Возвращает внутренный тоннель с задаными значениями
        /// </summary>
        public InnerTunnel Get(int global_id, string value)
        {
            idGenerator.AddID(global_id);
            InnerTunnel innerTunnel = new InnerTunnel(global_id, value);
            InnerTunnel result = innerTunnels.Find((a) => a.Equals(innerTunnel));
            if (result != null)
                return result;
            else
            {
                innerTunnels.Add(innerTunnel);
                return innerTunnel;
            }
        }
    }
}
