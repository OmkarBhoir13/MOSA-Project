// Copyright (c) MOSA Project. Licensed under the New BSD License.

// This code was generated by an automated template.

using Mosa.Compiler.Framework;

namespace Mosa.Platform.ARMv6.Instructions
{
	/// <summary>
	/// Ldrsh32 - Load 16-bit signed halfword
	/// </summary>
	/// <seealso cref="Mosa.Platform.ARMv6.ARMv6Instruction" />
	public sealed class Ldrsh32 : ARMv6Instruction
	{
		public override int ID { get { return 721; } }

		internal Ldrsh32()
			: base(1, 3)
		{
		}
	}
}
