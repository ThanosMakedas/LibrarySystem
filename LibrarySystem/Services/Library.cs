using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LibrarySystem.Core.Models;

namespace LibrarySystem.Core.Services;

public class Library
{
    public BookCatalog BookCatalog { get; }
    public MemberRegistry MemberRegistry { get; }
    public LoanManager LoanManager { get; }

    public Library()
    {
        BookCatalog = new BookCatalog();
        MemberRegistry = new MemberRegistry();
        LoanManager = new LoanManager();
    }
}

