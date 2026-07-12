using AutoMapper;
using Flight_API.Dto;
using Flight_API.Contracts;
using Flight_API.Entities;


namespace FlightAPI.Services
{
    public class FlightService : IFlightService
    {
        private readonly IFlightRepository _repository;
        private readonly IMapper _mapper;

        public FlightService(IFlightRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<int> AddAsync(FlightDto dto)
        {
            var entity = _mapper.Map<FlightMaster>(dto);
            return await _repository.AddAsync(entity);
        }

        public async Task<IEnumerable<FlightDto>> GetAllAsync()
        {
            var items = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<FlightDto>>(items);
        }

        public async Task<FlightDto?> GetByIdAsync(int id)
        {
            var item = await _repository.GetByIdAsync(id);
            return item == null ? null : _mapper.Map<FlightDto>(item);
        }

        public async Task<bool> UpdateAsync(FlightDto dto)
        {
            var entity = _mapper.Map<FlightMaster>(dto);
            return await _repository.UpdateAsync(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<PagedResultDto<FlightDto>> GetAllPagedAsync(
            string? flightNumber,
            string? airlineName,
            string? sourceAirport,
            string? destinationAirport,
            bool? isActive,
            int pageNumber,
            int pageSize)
        {
            var result = await _repository.GetAllPagedAsync(
                flightNumber,
                airlineName,
                sourceAirport,
                destinationAirport,
                isActive,
                pageNumber,
                pageSize);

            return new PagedResultDto<FlightDto>
            {
                Data = _mapper.Map<IEnumerable<FlightDto>>(result.Data),
                TotalRecords = result.TotalRecords
            };
        }

        
    }
}