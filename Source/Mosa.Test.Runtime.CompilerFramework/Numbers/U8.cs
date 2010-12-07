﻿/*
* (c) 2008 MOSA - The Managed Operating System Alliance
*
* Licensed under the terms of the New BSD License.
*
* Authors:
*  Phil Garcia (tgiphil) <phil@thinkedge.com>
*/

using System;
using System.Collections.Generic;
using System.Text;

using MbUnit.Framework;

namespace Mosa.Test.Runtime.CompilerFramework.Numbers
{
	public class U8
	{
		private static IList<ulong> samples = null;
		public static IList<ulong> SampleData { get { if (samples == null) samples = GetSamples(); return samples; } }

		public static IEnumerable<ulong> Samples
		{
			get
			{
				foreach (ulong value in SampleData)
					yield return value;
			}
		}

		public static IList<ulong> GetSamples()
		{
			List<ulong> list = new List<ulong>();

			list.Add(0);
			list.Add(1);
			list.Add(2);
			list.Add(byte.MinValue);
			list.Add(byte.MaxValue);
			list.Add(byte.MinValue + 1);
			list.Add(byte.MaxValue - 1);
			list.Add(ushort.MinValue);
			list.Add(ushort.MaxValue);
			list.Add(ushort.MinValue + 1);
			list.Add(ushort.MaxValue - 1);
			list.Add(uint.MinValue);
			list.Add(uint.MaxValue);
			list.Add(uint.MinValue + 1);
			list.Add(uint.MaxValue - 1);
			list.Add(ulong.MinValue);
			list.Add(ulong.MaxValue);
			list.Add(ulong.MinValue + 1);
			list.Add(ulong.MaxValue - 1);

			list.Sort();

			return list;
		}

	}
}
