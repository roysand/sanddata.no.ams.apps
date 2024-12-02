using Application.Interface;
using DataLayer.Domain.Entities;
using DataLayer.Infrastructure.Persistence;

namespace Infrastructure.Repositories;

public class DetailRepository : GenericRepository<Detail> , IDetailRepository<Detail>
{
    public DetailRepository(ApplicationDbContext context) : base(context)
    {
    }
}