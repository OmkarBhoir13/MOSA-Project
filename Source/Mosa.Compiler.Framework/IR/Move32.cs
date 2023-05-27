// Copyright (c) MOSA Project. Licensed under the New BSD License.

// This code was generated by an automated template.

namespace Mosa.Compiler.Framework.IR;

/// <summary>
/// Move32
/// </summary>
/// <seealso cref="Mosa.Compiler.Framework.IR.BaseIRInstruction" />
public sealed class Move32 : BaseIRInstruction
{
	public Move32()
		: base(1, 1)
	{
	}

	public override bool IsIRMoveInstruction => true;
}
