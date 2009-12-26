/*	Copyright (C) 2009-2010 Daniel Lo Nigro
 *	This program is free software: you can redistribute it and/or modify it 
 *	under the terms of the GNU General Public License as published by 
 *	the Free Software Foundation, either version 3 of the License, or 
 *	(at your option) any later version. This program is distributed in the 
 *	hope that it will be useful, but WITHOUT ANY WARRANTY; without 
 *	even the implied warranty of MERCHANTABILITY or FITNESS FOR 
 *	A PARTICULAR PURPOSE. See the GNU General Public License 
 *	for more details. You should have received a copy of the GNU General 
 *	Public License along with this program. If not, see <http://www.gnu.org/licenses/>
*/
using System.Collections.Generic;

namespace Common.Concurrency
{
	/// <summary>
	/// A thread-safe queue
	/// </summary>
	/// <typeparam name="T">Type to store in the queue</typeparam>
	public class Channel<T> {
		private readonly Semaphore _Semaphore = new Semaphore(0);
		private readonly Queue<T> _Queue = new Queue<T>();

		/// <summary>
		/// Put an item into the channel
		/// </summary>
		/// <param name="item">What to put into the channel</param>
		public void Put(T item) {
			lock (this) {
				_Queue.Enqueue(item);
			}
			_Semaphore.Release();
		}

		/// <summary>
		/// Take an item out of the channel
		/// </summary>
		/// <returns>The item</returns>
		public T Take() {
			_Semaphore.Acquire();
			lock (this) {
				return _Queue.Dequeue();
			}
		}

		/// <summary>
		/// Try to take an item from the channel. Stops trying after a set
		/// period of time
		/// </summary>
		/// <param name="ms">Milliseconds to try for</param>
		/// <returns>The item</returns>
		public T TryTake(int ms) {
			// Try to acquire a semaphore
			if (!_Semaphore.TryAcquire(ms))
				// TODO: Will this work in all situations? What about for eg. ints?
				return default(T);

			lock (this) {
				return _Queue.Dequeue();
			}
		}
	}
}