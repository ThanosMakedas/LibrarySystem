using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LibrarySystem.Core.Models;

namespace LibrarySystem.Core.Services;

public class MemberRegistry
{
    private readonly List<Member> _members = new();

    public IReadOnlyList<Member> Members => _members;

    public void AddMember(Member member)
    {
        _members.Add(member);
    }

    public Member? GetById(string memberId)
    {
        return _members.FirstOrDefault(m => m.MemberId == memberId);
    }

    public Member? GetMostActiveBorrower()
    {
        return _members
            .OrderByDescending(m => m.BorrowedBooks.Count)
            .FirstOrDefault();
    }
}