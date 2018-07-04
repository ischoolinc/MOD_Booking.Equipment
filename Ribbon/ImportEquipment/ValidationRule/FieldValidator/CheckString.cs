using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Campus.DocumentValidator;

namespace Ischool.Booking.Equipment
{
    class CheckString : IFieldValidator
    {
        public string Correct(string Value)
        {
            return string.Empty;
        }

        public string ToString(string template)
        {
            return template;
        }

        /// <summary>
        /// 回傳true、false
        /// </summary>
        /// <param name="Value"></param>
        /// <returns></returns>
        public bool Validate(string Value)
        {
            bool result = false;

            if (Value.Trim() == "")
            {
                result = false;
            }
            else
            {
                result = true;
            }

            return result;
        }
    }
}
