namespace Sensorium.Common
{
	public interface IVisitor<T>
	{
		void Visit(T t);
	}
}
