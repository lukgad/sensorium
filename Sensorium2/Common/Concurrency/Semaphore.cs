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
using System;
using System.Threading;

namespace Common.Concurrency
{
	/// <summary>
	/// A counting semaphore. TODO: Change this summary
	/// </summary>
	public class Semaphore {
		protected int _Tokens;

		public Semaphore(int tokensAvailable) {
			_Tokens = tokensAvailable;
		}

		/*public void Acquire()
		{
			lock (this)
			{
				while (_Tokens <= 0)
					Monitor.Wait(this);

				_Tokens--;
			}
		}*/

		/// <summary>
		/// Acquire a token from the semaphore.
		/// </summary>
		public void Acquire() {
			TryAcquire(-1);
		}

		/// <summary>
		/// Try to acquire a token from the semaphore. If a token is not acquired within the time limit, it just gives up 
		/// </summary>
		/// <param name="msTimeout">Maximum number of milliseconds to wait for a token</param>
		/// <returns>True if a token was acquired within the time limit, false otherwise.</returns>
		public bool TryAcquire(int msTimeout) {
			lock (this) {
				// Store the time that we have to end
				DateTime endTime = DateTime.Now.AddMilliseconds(msTimeout);

				while (true) {
					try {
						// Do we have a token? If so, we're done now
						if (_Tokens > 0) {
							_Tokens--;
							return true;
						}

						// Do we actually have a timeout?
						if (msTimeout != -1) {
							// Calculate how many milliseconds we have left to wait
							msTimeout = Convert.ToInt32((endTime - DateTime.Now).TotalMilliseconds);
							// Is our time up?
							if (msTimeout <= 0)
								return false;
						}

						Monitor.Wait(this, msTimeout);
					}
						// Were we interrupted?!
					catch (ThreadInterruptedException) {
						// Better let someone else wake up, just in case there's a
						// token available and we missed it.
						Monitor.Pulse(this);
						throw;
					}
				}
			}
		}

		/*public virtual void Release()
		{
			lock (this)
			{
				_Tokens++;
				Monitor.PulseAll(this);
			}
		}*/

		/// <summary>
		/// Release a token into the semaphore
		/// </summary>
		public virtual void Release() {
			Release(1);
		}

		/// <summary>
		/// Release a number of tokens into the semaphore
		/// </summary>
		/// <param name="howMany">How many tokens to release</param>
		public virtual void Release(int howMany) {
			lock (this) {
				_Tokens += howMany;
				//Monitor.PulseAll(this);
				// TODO: Is this faster than PulseAll? Need to benchmark
				for (int i = 0; i < howMany; i++)
					Monitor.Pulse(this);
			}
		}

		/// <summary>
		/// Force the release of a token into the semaphore. This will release
		/// a token even if interrupted.
		/// </summary>
		public virtual void ForceRelease() {
			bool wasInterrupted = false;

			while (true) {
				// Try to release. If we get interrupted, try again.
				try {
					Release();
					break;
				} catch (ThreadInterruptedException) {
					// Better remember this for when we're done
					wasInterrupted = true;
					// Be nice and yield to other threads
					Thread.Sleep(0);
				}
			}
			// Were we interrupted?
			if (wasInterrupted)
				Thread.CurrentThread.Interrupt();
		}
	}
}