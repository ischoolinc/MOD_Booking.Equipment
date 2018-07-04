using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Campus.DocumentValidator;

namespace Ischool.Booking.Equipment
{
    class EquipmentRowValidatorFactory : IRowValidatorFactory
    {
        public IRowVaildator CreateRowValidator(string typeName, XmlElement validatorDescription)
        {
            switch (typeName.ToUpper())
            {
                default:
                    return null;
            }
        }
    }
}
