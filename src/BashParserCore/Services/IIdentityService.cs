using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using BashParserCore.Models;

namespace BashParserCore.Services
{
    public interface IIdentityService
    {
        void createModerator();
    }
}
