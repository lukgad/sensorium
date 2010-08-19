namespace Sensorium.Core {
	public interface IVisitor<T> {
		void Visit(T t);
	}
}
