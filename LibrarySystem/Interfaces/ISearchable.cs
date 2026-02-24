using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Core.Interfaces;

public interface ISearchable
{
    bool Matches(string searchTerm);
}
