using AutoMapper;
using BusinessLogic.Services.Interfaces;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class PersonService : IPersonService
    {
        private readonly ClassNTutorContext _context;
        private readonly IS3StorageService _s3storageService;
        private readonly IMapper _mapper;

        public PersonService(ClassNTutorContext context,
            IS3StorageService s3storageService, IMapper mapper)
        {
            _context = context;
            _s3storageService = s3storageService;
            _mapper = mapper;
        }


    }
}
