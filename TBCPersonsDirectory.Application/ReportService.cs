using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TBCPersonsDirectory.Repository.Interfaces;
using TBCPersonsDirectory.Services.Interfaces;
using TBCPersonsDirectory.Services.Models;

namespace TBCPersonsDirectory.Application
{
    public class ReportService : IReportService
    {
        private readonly IPersonsRepository _personsRepository;
        private readonly IPersonConnectionsRepository _personConnectionsRepository;

        public ReportService(IPersonsRepository personsRepository, IPersonConnectionsRepository personConnectionsRepository)
        {
            _personsRepository = personsRepository;
            _personConnectionsRepository = personConnectionsRepository;
        }

        public PersonReportResponseModel GetPersonReport()
        {
            var allConnections = _personConnectionsRepository.GetAll()
                .Include(c => c.FirstPerson)
                .Include(c => c.ConnectionType).Select(c =>
                new ConnectionReportModel
                {
                    PersonId = c.FirstPersonId.Value,
                    FullName = $"{c.FirstPerson.FirstName} {c.FirstPerson.LastName}",
                    PrivateNumber = c.FirstPerson.PrivateNumber,
                    ConnectionTypeId = c.ConnectionType.Id,
                    TargetPersonId = c.SecondPersonId.Value
                });

            PersonReportRowModel[] rows = new PersonReportRowModel[allConnections.Count()];

            var connectionTypes = _personConnectionsRepository.GetConnectionTypes();

            var connectionTypesDictionary = new Dictionary<int, string>();

            foreach (var c in connectionTypes)
            {
                connectionTypesDictionary.Add(c.Id, c.Name);
            }

            foreach (var ac in allConnections)
            {
                if (rows[ac.PersonId] == null)
                {
                    rows[ac.PersonId] = new PersonReportRowModel
                    {
                        Id = ac.PersonId,
                        FullName = ac.FullName,
                        PrivateNumber = ac.PrivateNumber
                    };
                }

                if (rows[ac.PersonId].ConnectedPersons.ContainsKey(ac.ConnectionTypeId))
                {
                    rows[ac.PersonId].ConnectedPersons[ac.ConnectionTypeId] = rows[ac.PersonId].ConnectedPersons[ac.ConnectionTypeId] + 1;
                }
                else
                {
                    rows[ac.PersonId].ConnectedPersons[ac.ConnectionTypeId] = 1;
                }
            }

            var resp = rows.Where(c => c != null).Select(c => new PersonReportRowResponseModel
            {
                Id = c.Id,
                FullName = c.FullName,
                PrivateNumber = c.PrivateNumber,
                ConnectedPersons = c.ConnectedPersons.Select(x => new ConnectedPersonResponseModel
                {
                    ConnectionTypeId = x.Key,
                    ConnectedPersonCount = x.Value,
                    ConnectionTypeName = connectionTypesDictionary[x.Key]
                }).ToList()
            });



            return new PersonReportResponseModel { Rows = resp.ToList() };
        }
    }
}
