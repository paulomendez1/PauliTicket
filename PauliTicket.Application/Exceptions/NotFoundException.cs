using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace PauliTicket.Application.Exceptions
{
    public class NotFoundException : Exception
    {
        private readonly ILogger<NotFoundException> _logger;
        public NotFoundException(string name, object key, ILogger<NotFoundException> logger) : base($"{name} ({key}) is not found")
        {
            logger.LogError($"{name} ({key}) is not found");
        }
    }
}
