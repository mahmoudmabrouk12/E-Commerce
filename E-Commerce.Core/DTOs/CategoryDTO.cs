﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.DTOs
{
   
    public record CategoryDTO(string Name , string Description);
    public record UpdateCatgoryDTO(int Id , string Name, string Description);




}
