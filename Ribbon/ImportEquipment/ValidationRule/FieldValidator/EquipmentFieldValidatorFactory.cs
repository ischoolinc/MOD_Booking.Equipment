using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Campus.DocumentValidator;

namespace Ischool.Booking.Equipment
{
    class EquipmentFieldValidatorFactory : IFieldValidatorFactory
    {
        IFieldValidator IFieldValidatorFactory.CreateFieldValidator(string typeName, XmlElement validatorDescription)
        {
            switch (typeName.ToUpper())
            {
                case "CHECKUNITINISCHOOL":
                    return new CheckUnitInIschool();
                case "CHECKSTRING":
                    return new CheckString();
                case "INTPARSE":
                    return new CheckInt();
                default:
                    return null;
            }
        }
    }
}
