
using AutoMapper;
using AutoMapper.QueryableExtensions;
using EmployeeManagementMVC.Repositories;

namespace EmployeeManagementMVC.Services
{
    public class GenericService<TEntity, TDto> : IGenericService<TDto>
        where TEntity : class
    {
        private readonly IGenericRepository<TEntity> _repository;
        private readonly IMapper _mapper;

        public GenericService(IGenericRepository<TEntity> repository, IMapper mapper)
        {
                _repository = repository;
                _mapper = mapper;
        }
        public async Task AddAsync(TDto dto)
        {
            var entity = _mapper.Map<TEntity>(dto);
            await _repository.AddAsync(entity);
            await _repository.SaveAsync();
        }

        public async Task UpdateAsync(TDto dto)
        {
            var entity = _mapper.Map<TEntity>(dto);
            await _repository.UpdateAsync(entity);
            await _repository.SaveAsync();
        }
        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
            await _repository.SaveAsync();
        }

        public IQueryable<TDto> GetAll()
        {
            return _repository
                .GetAll()
                .ProjectTo<TDto>(_mapper.ConfigurationProvider);
        }

        public async Task<TDto?> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return _mapper.Map<TDto>(entity);
        }

    }
}
