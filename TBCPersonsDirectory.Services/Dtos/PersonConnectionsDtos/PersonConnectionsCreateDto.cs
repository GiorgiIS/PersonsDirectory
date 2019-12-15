using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace TBCPersonsDirectory.Services.Dtos.PersonConnectionsDtos
{
    public class PersonConnectionsCreateDto
    {
        [Required]
        public int ConnectionTypeId { get; set; }
    
        [Required]
        public int TargetPersonId { get; set; }
    }
}
