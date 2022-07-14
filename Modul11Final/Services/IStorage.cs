using Modul11Final.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Modul11Final.Services
{
    public interface IStorage
    {
        /// <summary>
        /// Получение сессии пользователя по идентификатору
        /// </summary>
        Session GetSession(long chatId);
    }
}
