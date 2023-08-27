using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.Domain.Base
{
    /// <summary>
    /// Базовый контракт.
    /// </summary>
    public class BaseEntity
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public Guid Id { get; set; }
    }
}
