﻿using System.Collections.Generic;
using worksheet2.Model;
using worksheet2.Model.Request;
using worksheet2.Model.Response;

namespace worksheet2.Services
{
    public interface IAccountDetailsService
    {
        IEnumerable<AccountDetailsResponse> getAccountDetailsResponsesById(User user);
    }
}