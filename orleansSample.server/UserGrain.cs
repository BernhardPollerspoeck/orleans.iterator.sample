using Orleans.Runtime;

using orleansSample.grain;

namespace orleansSample.server;

public class UserGrain : Grain, IUserGrain
{
	private readonly IPersistentState<List<IDeviceGrain>> _state;

	public UserGrain(
		[PersistentState("User", "STORE_NAME")]
		IPersistentState<List<IDeviceGrain>> state)
	{
		_state = state;
	}

	public Task AddDevice(GrainId grainId)
	{
		_state.State.Add(GrainFactory.GetGrain<IDeviceGrain>(grainId));
		return _state.WriteStateAsync();
	}

	public Task<List<GrainId>> GetDevices()
	{
		return Task.FromResult(_state.State.Select(s => s.GetGrainId()).ToList());
	}

	public async Task HiAll()
	{
		foreach (var item in _state.State)
		{
			var msg = await item.SayHi();
			Console.WriteLine(msg);
		}
	}
}