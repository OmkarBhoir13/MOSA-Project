// Copyright (c) MOSA Project. Licensed under the New BSD License.

// This code was generated by an automated template.

using Mosa.Compiler.Framework;

namespace Mosa.Platform.ARMv6.Instructions
{
	/// <summary>
	/// Bl32 - Call a subroutine
	/// </summary>
	/// <seealso cref="Mosa.Platform.ARMv6.ARMv6Instruction" />
	public sealed class Bl32 : ARMv6Instruction
	{
		public override int ID { get { return 705; } }

		internal Bl32()
			: base(1, 3)
		{
		}
	}
}
