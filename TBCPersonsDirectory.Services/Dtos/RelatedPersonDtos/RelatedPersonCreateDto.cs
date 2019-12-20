using System;
using System.Collections.Generic;
using System.Text;

namespace TBCPersonsDirectory.Services.Dtos.RelatedPersonDtos
{
    public class RelatedPersonCreateDto
    {
        public int RelatedPersonId { get; set; }
        public int RelationshipId { get; set; }
    }
}
