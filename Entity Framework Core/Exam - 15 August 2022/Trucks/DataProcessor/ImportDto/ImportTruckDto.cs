﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Trucks.Common;

namespace Trucks.DataProcessor.ImportDto
{
    [XmlType("Truck")]
    public class ImportTruckDto
    {
        [XmlElement("RegistrationNumber")]
        [MinLength(ValidationConstants.TruckRegistrationNumberMaxLength)]
        [MaxLength(ValidationConstants.TruckRegistrationNumberMaxLength)]
        [RegularExpression(ValidationConstants.TruckRegistrationNumberRegEx)]
        public string? RegistrationNumber { get; set; }

        [XmlElement("VinNumber")]
        [Required]
        [MinLength(ValidationConstants.TruckVinNumberMaxLength)]
        [MaxLength(ValidationConstants.TruckVinNumberMaxLength)]
        public string VinNumber { get; set; } = null!;

        [XmlElement("TankCapacity")]
        [Range(ValidationConstants.TruckTankCapacityMinValue,
            ValidationConstants.TruckTankCapacityMaxValue)]
        public int TankCapacity { get; set; }

        [XmlElement("CargoCapacity")]
        [Range(ValidationConstants.TruckCargoCapacityMinValue,
            ValidationConstants.TruckCargoCapacityMaxValue)]
        public int CargoCapacity { get; set; }

        [XmlElement("CategoryType")]
        [Range(ValidationConstants.TruckCategoryTypeMinValue,
            ValidationConstants.TruckCategoryTypeMaxValue)]
        public int CategoryType { get; set; }

        [XmlElement("MakeType")]
        [Range(ValidationConstants.TruckMakeTypeMinValue,
            ValidationConstants.TruckMakeTypeMaxValue)]
        public int MakeType { get; set; }
    }
}
