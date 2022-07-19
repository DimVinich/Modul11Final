using System;
using System.Collections.Generic;
using System.Text;

namespace Modul11Final.Services
{

    /// <summary>
    /// В данном классе реализованы "Задания" из финального задания к 11 модулю
    /// 1. Подсчёт кол-ва символов. Сразу с фомированиям строки для вывода
    /// 2. Подсчёт суммы введённых чисел, так же с формированим строки для вывода
    /// </summary>
    /// 
    public class StringServices : IStringServices
    {

        /// 1. Подсчёт кол-ва символов. Сразу с фомированиям строки для вывода
        public string CountString(string aString)
        {
            return $"Длина сообщения: {aString.Length} знаков";
        }

        /// 2. Подсчёт суммы введённых чисел, так же с формированим строки для вывода
        public string CountSum(string aString)
        {
            double ldh = 0.0;
            double ldSum = 0.0;
            string lsError = "Введённая информация не может быть суммирована";

            try
            {
                string[] arrString = aString.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                if (arrString.Length < 1)
                {
                    return lsError;
                }

                foreach (string lsh in arrString)
                {
                    if (lsh.Length > 0)
                    {
                        if (double.TryParse(lsh, out ldh))
                        {
                            ldSum += ldh;
                        }
                        else
                        {
                            return lsError;
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                return lsError + "\n" + ex.Message;
            }

            return $"Сумма введённых чисел: {ldSum}";

        }
    }
}
