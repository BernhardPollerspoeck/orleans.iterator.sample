using Orleans.Runtime;

using orleansSample.grain;

namespace orleansSample.server;

public class DeviceGrain : Grain, IDeviceGrain
{
	private readonly IPersistentState<int> _state;

	public DeviceGrain(
		[PersistentState("Device", "STORE_NAME")]
		IPersistentState<int> state)
	{
		_state = state;
	}

	public async Task<string> SayHi()
	{
		_state.State++;
		await _state.WriteStateAsync();
		//await _state.ClearStateAsync();
		return $"Hi {this.GetPrimaryKeyString()} {_state.State}";
	}
}
