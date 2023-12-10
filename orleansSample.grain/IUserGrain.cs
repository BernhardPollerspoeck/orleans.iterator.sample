using Orleans.Runtime;

namespace orleansSample.grain;
public interface IUserGrain : IGrainWithStringKey
{

	Task<List<GrainId>> GetDevices();
	Task AddDevice(GrainId grainId);

	Task HiAll();
}
