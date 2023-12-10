namespace orleansSample.grain;
public interface IDeviceGrain : IGrainWithStringKey
{
	Task<string> SayHi();
}
